// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;
using Unity.Mathematics;

namespace Klak.Timeline
{
    [System.Serializable]
    public class BrownianMotionPlayable : PlayableBehaviour
    {
        #region Serialized variables

        public Vector3 positionAmount = Vector3.one;
        public Vector3 rotationAmount = Vector3.one * 10;
        public float frequency = 1;
        public int octaves = 2;
        public uint randomSeed;

        #endregion

        #region Private functions

        float Fbm(float x, float y, int octave)
        {
            var p = math.float2(x, y);
            var f = 0.0f;
            var w = 0.5f;
            for (var i = 0; i < octave; i++)
            {
                f += w * noise.snoise(p);
                p *= 2.0f;
                w *= 0.5f;
            }
            return f;
        }

        #endregion

        #region PlayableBehaviour overrides

        float3 _positionOffset;
        float3 _rotationOffset;

        public override void OnPlayableCreate(Playable playable)
        {
            var rand = new Unity.Mathematics.Random(randomSeed + 1);
            _positionOffset = rand.NextFloat3(-1e3f, 1e3f);
            _rotationOffset = rand.NextFloat3(-1e3f, 1e3f);
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var target = playerData as Transform;
            if (target == null) return;

            var t = (float)playable.GetTime() * frequency;
            var w = info.weight / 0.75f; // normalized weight

            var np = math.float3(
                Fbm(_positionOffset.x, t, octaves),
                Fbm(_positionOffset.y, t, octaves),
                Fbm(_positionOffset.z, t, octaves)
            );

            var nr = math.float3(
                Fbm(_rotationOffset.x, t, octaves),
                Fbm(_rotationOffset.y, t, octaves),
                Fbm(_rotationOffset.z, t, octaves)
            );

            np = np * positionAmount * w;
            nr = nr * rotationAmount * w;

            target.localPosition += (Vector3)np;
            target.localRotation = Quaternion.Euler(nr) * target.localRotation;
        }

        #endregion
    }
}
