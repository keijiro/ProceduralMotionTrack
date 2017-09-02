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
        public int noiseOctaves = 0;

        public float amplitude = 1;
        public AnimationCurve envelope = AnimationCurve.Linear(0, 1, 1, 1);

        public int randomSeed;

        #endregion

        #region Public properties

        public Vector3 currentPosition { private set; get; }
        public Vector3 currentRotation { private set; get; }

        #endregion

        #region PlayableBehaviour overrides

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var time = (float)playable.GetTime();
            var normalizedTime = time / (float)playable.GetDuration();

            // Calculate amplitude. Break if it's nearly zero.
            var amp = amplitude * envelope.Evaluate(normalizedTime);
            if (Mathf.Approximately(amp, 0))
            {
                currentPosition = currentRotation = Vector3.zero;
                return;
            }

            // Noise variables.
            var hash = new XXHash(randomSeed);
            var nt = time * noiseFrequency;
            var norm = 1 / 0.75f;

            // Calculate position offset.
            currentPosition = position;

            if (noiseOctaves > 0)
            {
                var n = new Vector3(
                    Perlin.Fbm(hash.Range(-1e3f, 1e3f, 0) + nt, noiseOctaves),
                    Perlin.Fbm(hash.Range(-1e3f, 1e3f, 1) + nt, noiseOctaves),
                    Perlin.Fbm(hash.Range(-1e3f, 1e3f, 2) + nt, noiseOctaves)
                );
                currentPosition = Vector3.Scale(n, currentPosition) * norm;
            }

            currentPosition *= amp;

            // Calculate rotation offset.
            currentRotation = rotation;

            if (noiseOctaves > 0)
            {
                var n = new Vector3(
                    Perlin.Fbm(hash.Range(-1e3f, 1e3f, 3) + nt, noiseOctaves),
                    Perlin.Fbm(hash.Range(-1e3f, 1e3f, 4) + nt, noiseOctaves),
                    Perlin.Fbm(hash.Range(-1e3f, 1e3f, 5) + nt, noiseOctaves)
                );
                currentRotation = Vector3.Scale(n, currentRotation) * norm;
            }

            currentRotation *= amp;
        }

        #endregion
    }
}
