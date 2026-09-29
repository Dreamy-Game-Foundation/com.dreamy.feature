using Dreamy.UI;
using UnityEngine;

namespace Dreamy.Feature
{
    public class FeaturePanel : UIPanel
    {
        [SerializeField] private string featureId = "feature.base";
        [SerializeField] private bool canBack = true;

        public override bool CanBack => canBack;
        public FeatureId FeatureId => new(featureId);
    }
}
