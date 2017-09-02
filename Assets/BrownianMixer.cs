using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Motion
{
    [System.Serializable]
    class BrownianMixer : PlayableBehaviour
    {
        Transform _target;

        Vector3 _initialPosition;
        Quaternion _initialRotation;

        bool _initialized;

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

            var inputCount = playable.GetInputCount();

            var position = _initialPosition;
            var rotation = _initialRotation;


            for (var i = 0; i < inputCount; i++)
            {
                var input = (ScriptPlayable<BrownianPlayable>)playable.GetInput(i);
                var brownian = input.GetBehaviour();
                var weight = playable.GetInputWeight(i);
                var time = (float)input.GetTime();

                position += brownian.CalculatePosition(time) * weight;
                rotation *= Quaternion.Euler(brownian.CalculateRotation(time) * weight);
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
    }
}
