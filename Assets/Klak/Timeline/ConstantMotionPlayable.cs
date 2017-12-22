// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Timeline
{
    [System.Serializable]
    public class ConstantMotionPlayable : PlayableBehaviour
    {
        #region Serialized variables

        public Vector3 position = Vector3.zero;
        public Vector3 rotation = Vector3.zero;

        #endregion

        #region PlayableBehaviour overrides

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var target = playerData as Transform;
            if (target != null)
            {
                target.localPosition += position * info.weight;
                target.localRotation = Quaternion.Slerp(target.localRotation, Quaternion.Euler(rotation), info.weight);
            }
        } 

        #endregion
    }
}
