// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Timeline
{
    [System.Serializable]
    public class ProceduralMotionMixer : PlayableBehaviour
    {
        public Vector3 InitialPosition { private get; set; }
        public Quaternion InitialRotation { private get; set; }

        #region PlayableBehaviour overrides

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            var target = info.output.GetUserData() as Transform;
            if (target != null)
            {
                // Reset the target transform.
                // It'll be modified in clip playables.
                target.localPosition = InitialPosition;
                target.localRotation = InitialRotation;
            }
        }

        #endregion
    }
}
