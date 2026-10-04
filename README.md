# Dreamy Feature

Package thuộc Dreamy Game Studio. Hướng dẫn dưới đây mô tả cấu trúc, cách cài vào project và tích hợp ở root/scene.

## Cài package

Dùng Unity 6000.0 trở lên. Sandbox đã tham chiếu package bằng `file:../LocalPackages/com.dreamy.feature`. Project khác dùng Package Manager > + > Install package from disk và chọn package.json, hoặc Git URL của repository nội bộ. Cài cả dependency Dreamy/Git vào manifest của game; version dependency không tự cấu hình registry riêng.

Dependency trực tiếp theo package.json:

Không khai báo dependency trực tiếp; kiểm tra reference asmdef và prefab bên dưới.

## Cấu trúc và asmdef

| Assembly | Reference | Phạm vi |
| --- | --- | --- |
| `Dreamy.Feature.Runtime` | Dreamy.DataConfig.Runtime (GUID) | Runtime |

Trong asmdef của game, thêm assembly chứa API trực tiếp sử dụng. Code bootstrap reference thêm Core/DataConfig/Datasave/Economy theo nhu cầu; code async reference UniTask. Code gọi type sample reference assembly sample. Giữ Editor reference trong asmdef Editor-only.

## Cấu trúc và vai trò

Runtime/FeatureId là ID chuỗi không trống; FeatureState gồm Locked/Available/Active/Completed; FeatureMetadata ghép ID và state. IFeatureView<TState> cung cấp Render; IFeaturePresenter cung cấp Show/Refresh. Game quyết định trạng thái và persistence.

```csharp
using Dreamy.Feature;
var metadata = new FeatureMetadata(
    new FeatureId("shop"), FeatureState.Available);
```

Runtime/Prefabs/BaseFeaturePanel.prefab và BaseFeatureItem.prefab là nền để tạo Prefab Variant trong game. Thêm UIPanel subclass trên root variant panel, gán nội dung/button/container; item dùng variant riêng. Giữ .meta và reference prefab nền.

Package không có installer/service hoặc sample riêng. Cài service của feature cụ thể ở GameInstaller: config/save/wallet trước, service nghiệp vụ sau, rồi presenter/panel. Runtime asmdef hiện reference DataConfig qua GUID dù package.json chưa khai báo; cài DataConfig. Prefab nền cần Dreamy UI và dependency của UI.

Game nên tổ chức Scripts/Bootstrap/GameInstaller.cs, Scripts/UI/PanelAddress.cs, Scripts/Features/<Feature> và Prefabs/Panel. Service giữ nghiệp vụ; presenter nối sự kiện; panel chỉ hiển thị. Các feature có thể dùng contract riêng như IShopView. Xem README Shop để có ví dụ root cụ thể.
## Sample

Manifest hiện không khai báo sample để import qua Package Manager.

## Addressables Group và class address

1. Lưu prefab/variant của game tại Assets/_Project/Prefabs/Panel/HomePanel.prefab. Với UIPanel, root phải có subclass tương ứng.
2. Mở Window > Asset Management > Addressables > Groups; tạo settings nếu chưa có.
3. Tạo group UI Panels và kéo prefab vào group.
4. Đặt cột Address thành Panel/HomePanel.prefab.
5. Tạo class dùng chung trong game:

```csharp
public static class PanelAddress
{
    public const string Home = "Panel/HomePanel.prefab";
    public const string Current = "Panel/HomePanel.prefab";
}
```

Đường dẫn asset trên disk và address là hai giá trị riêng. Address do bạn đặt, constant phải khớp chính xác cột Address. Tên group không phải key tải. HomePanel là ví dụ subclass do game tự tạo.

Scene cần Canvas có PanelManager và EventSystem/input module. Chờ root cài service xong. Các lệnh sau nằm trong method async UniTask; asmdef reference Dreamy.UI.Runtime, UniTask và assembly chứa type panel.

```csharp
var panel = await PanelManager.Instance.Create<HomePanel>(PanelAddress.Current);
await panel.Show();
// Đóng từ code game:
await PanelManager.Instance.Close<HomePanel>();
```

Host giữ một presenter cho mỗi panel instance, dispose lúc teardown, bind/render lại khi mở panel cache. Không chạy đồng thời controller sample và presenter khác trên cùng panel. PanelManager không tự cài service feature.

Build Addressables content cho target trước khi thử player. AssetLoader cache prefab; đóng panel không tự unload cache. Chỉ unload sau khi mọi instance/consumer đã kết thúc.
