using UnityEngine;
using UnityEngine.Playables;
using Klak.Math;

namespace Klak.Timeline
{
    [System.Serializable]
    public class OffsetPlayable : PlayableBehaviour
    {
        #region Serialized variables

        public Vector3 position = Vector3.zero;
        public Vector3 rotation = Vector3.zero;

        public float noiseFrequency = 0.1f;
        public int noiseOctaveCount = 0;

        public float amplitude = 1;
        public AnimationCurve envelope = AnimationCurve.Linear(0, 1, 1, 1);

        public int randomSeed;

        #endregion

        #region Private members

        const float kNoiseNormalize = 1 / 0.75f;

        #endregion

        #region Public functions

        public Vector3 CalculatePosition(float time)
        {
            var v = position;

            if (noiseOctaveCount > 0)
            {
                var hash = new XXHash(randomSeed);
                var t = time * noiseFrequency;
                var n = new Vector3(
                    Perlin.Fbm(hash.Range(-1e3f, 1e3f, 0) + t, noiseOctaveCount),
                    Perlin.Fbm(hash.Range(-1e3f, 1e3f, 1) + t, noiseOctaveCount),
                    Perlin.Fbm(hash.Range(-1e3f, 1e3f, 2) + t, noiseOctaveCount)
                );
                v += Vector3.Scale(n, v) * kNoiseNormalize;
            }

            return v * amplitude;
        }

        public Vector3 CalculateRotation(float time)
        {
            var v = rotation;

            if (noiseOctaveCount > 0)
            {
                var hash = new XXHash(randomSeed);
                var t = time * noiseFrequency;
                var n = new Vector3(
                    Perlin.Fbm(hash.Range(-1e3f, 1e3f, 0) + t, noiseOctaveCount),
                    Perlin.Fbm(hash.Range(-1e3f, 1e3f, 1) + t, noiseOctaveCount),
                    Perlin.Fbm(hash.Range(-1e3f, 1e3f, 2) + t, noiseOctaveCount)
                );
                v += Vector3.Scale(n, v) * kNoiseNormalize;
            }

            return v * amplitude;
        }

        #endregion
    }
}
