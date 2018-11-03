// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;
using Unity.Mathematics;

namespace Klak.Timeline
{
    [System.Serializable]
    public class BrownianAnimationPlayable : PlayableBehaviour
    {
        #region Serialized variables

        public float frequency = 1;
        public int octaves = 2;
        public uint randomSeed;

        #endregion

        #region Public property

        public float CurrentValue { get; private set; }

        #endregion

        #region PlayableBehaviour overrides

        float _noiseOffset;

        public override void OnPlayableCreate(Playable playable)
        {
            var rand = new Unity.Mathematics.Random(randomSeed + 1);
            _noiseOffset = rand.NextFloat(-1e3f, 1e3f);
        }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            var t = (float)playable.GetTime() * frequency;
            CurrentValue = Fbm(_noiseOffset, t, octaves);
        }

        #endregion

        #region Fractal brownian motion

        static float Fbm(float x, float y, int octave)
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

    }
}
