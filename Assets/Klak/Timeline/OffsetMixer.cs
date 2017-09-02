using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline
{
    [System.Serializable]
    class OffsetMixer : PlayableBehaviour
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
                var weight = playable.GetInputWeight(i);
                if (Mathf.Approximately(weight, 0)) continue;

                var input = ((ScriptPlayable<OffsetPlayable>)playable.GetInput(i)).GetBehaviour();
                position += input.currentPosition * weight;
                rotation *= Quaternion.Euler(input.currentRotation * weight);
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
