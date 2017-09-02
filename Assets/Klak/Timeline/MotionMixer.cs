using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline
{
    [System.Serializable]
    class MotionMixer : PlayableBehaviour
    {
        #region Private state

        Transform _target;
        Vector3 _initialPosition;
        Quaternion _initialRotation;
        bool _initialized;

        #endregion

        #region PlayableBehaviour overrides

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            _target = playerData as Transform;
            if (_target == null) return;

            if (!_initialized)
            {
                _initialPosition = _target.localPosition;
                _initialRotation = _target.localRotation;
                _initialized = true;
            }

            var position = _initialPosition;
            var rotation = _initialRotation;

            var inputCount = playable.GetInputCount();
            for (var i = 0; i < inputCount; i++)
            {
                var input = (ScriptPlayable<MotionPlayable>)playable.GetInput(i);
                var motion = input.GetBehaviour();
                var weight = playable.GetInputWeight(i);
                var time = (float)input.GetTime();
                var normalizedTime = time / (float)input.GetDuration();

                weight *= motion.envelope.Evaluate(normalizedTime);
                if (weight < 0.001f) continue;

                var mpos = motion.CalculatePosition(time);
                var mrot = motion.CalculateRotation(time);

                position += mpos * weight;
                rotation *= Quaternion.Euler(mrot * weight);
            }

            _target.localPosition = position;
            _target.localRotation = rotation;
        }

        public override void OnGraphStop(Playable playable)
        {
            if (_target != null)
            {
                _target.localPosition = _initialPosition;
                _target.localRotation = _initialRotation;
            }

            _initialized = false;
        }

        #endregion
    }
}
