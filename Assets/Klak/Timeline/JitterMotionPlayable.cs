// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;
using Klak.Math;

namespace Klak.Timeline
{
    [System.Serializable]
    public class JitterMotionPlayable : PlayableBehaviour
    {
        #region Serialized variables

        public Vector3 positionAmount = Vector3.one * 0.1f;
        public Vector3 rotationAmount = Vector3.one * 3;
        public int randomSeed;

        #endregion

        #region PlayableBehaviour overrides

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var target = playerData as Transform;
            if (target == null) return;

            var t = (float)playable.GetTime();
            var id = (int)(t * 1e+6f % 0xfffffff);
            var hash = new XXHash(randomSeed ^ id);

            var p = new Vector3(hash.Value01(0), hash.Value01(1), hash.Value01(2));
            var r = new Vector3(hash.Value01(3), hash.Value01(4), hash.Value01(5));

            p = Vector3.Scale(p * 2 - Vector3.one, positionAmount) * info.weight;
            r = Vector3.Scale(r * 2 - Vector3.one, rotationAmount) * info.weight;

            target.localPosition += p;
            target.localRotation = Quaternion.Euler(r) * target.localRotation;
        }

        #endregion
    }
}
