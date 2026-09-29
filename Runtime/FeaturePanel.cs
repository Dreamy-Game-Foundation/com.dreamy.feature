using Dreamy.UI;

namespace Dreamy.Feature
{
    public abstract class FeaturePanel : UIPanel
    {
        public abstract FeatureId FeatureId { get; }
    }
}
