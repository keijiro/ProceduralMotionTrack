// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;
using Unity.Mathematics;

namespace Klak.Timeline
{
    [System.Serializable]
    public class JitterMotionPlayable : PlayableBehaviour
    {
        #region Serialized variables

        public Vector3 positionAmount = Vector3.one * 0.1f;
        public Vector3 rotationAmount = Vector3.one * 3;
        public uint randomSeed;

        #endregion

        #region PlayableBehaviour overrides

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var target = playerData as Transform;
            if (target == null) return;

            var t = (float)playable.GetTime();
            var id = (uint)(t * 1e+6f % 0xfffffff);
            var rand = new Unity.Mathematics.Random((randomSeed + 1) ^ id);

            var p = rand.NextFloat3();
            var r = rand.NextFloat3();

            p = (p * 2 - 1) * positionAmount * info.weight;
            r = (r * 2 - 1) * rotationAmount * info.weight;

            target.localPosition += (Vector3)p;
            target.localRotation = Quaternion.Euler(r) * target.localRotation;
        }

        #endregion
    }
}
