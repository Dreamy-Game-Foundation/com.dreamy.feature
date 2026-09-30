# Dreamy Feature

`com.dreamy.feature` is the thin shared foundation for game features. It contains feature identity and state primitives, view and presenter contracts, and two reusable UI base prefabs.

## Ownership

- `FeatureId`, `FeatureMetadata`, `FeatureState`, `IFeatureView<TState>`, and `IFeaturePresenter` are package-neutral contracts.
- `Runtime/Prefabs/FeaturePanel.prefab` and `Runtime/Prefabs/FeatureItem.prefab` are visual bases for feature-specific prefab variants.

This package has no feature-specific MonoBehaviour, Daily Reward, Shop, Spin, currency balance, configuration, save, or audio logic. Each feature package owns its own domain behavior and each host game owns art, prefab variants, and composition.
