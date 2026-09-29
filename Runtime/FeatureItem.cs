using UnityEngine;

namespace Dreamy.Feature
{
    public class FeatureItem : MonoBehaviour
    {
        public virtual void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}
