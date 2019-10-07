// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Timeline
{
    [System.Serializable]
    public class CyclicMotionPlayable : PlayableBehaviour
    {
        #region Serialized variables

        public Vector3 positionAmount = Vector3.one;
        public Vector3 positionFrequency = Vector3.one;
        public Vector3 rotationAmount = Vector3.one * 10;
        public Vector3 rotationFrequency = Vector3.one;

        #endregion

        #region PlayableBehaviour overrides

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var target = playerData as Transform;
            if (target == null) return;

            var t = (float)playable.GetTime();
            var w = info.weight;

            var p = new Vector3(
                Mathf.Sin(positionFrequency.x * t),
                Mathf.Sin(positionFrequency.y * t),
                Mathf.Sin(positionFrequency.z * t)
            );

            var r = new Vector3(
                Mathf.Sin(rotationFrequency.x * t),
                Mathf.Sin(rotationFrequency.y * t),
                Mathf.Sin(rotationFrequency.z * t)
            );

            p = Vector3.Scale(p, positionAmount) * w;
            r = Vector3.Scale(r, rotationAmount) * w;

            target.localPosition += p;
            target.localRotation = Quaternion.Euler(r) * target.localRotation;
        }

        #endregion
    }
}
