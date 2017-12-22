// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Timeline
{
    [System.Serializable]
    public class ProceduralMotionMixer : PlayableBehaviour
    {
        #region PlayableBehaviour overrides

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var target = playerData as Transform;
            if (target != null)
            {
                // Reset the target transform.
                // It'll be modified in clip playables.
                target.localPosition = Vector3.zero;
                target.localRotation = Quaternion.identity;
            }
        }

        #endregion
    }
}
