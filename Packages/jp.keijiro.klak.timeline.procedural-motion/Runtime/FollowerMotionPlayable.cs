using UnityEngine;
using UnityEngine.Playables;
using Unity.Mathematics;

namespace Klak.Timeline {

[System.Serializable]
public sealed class FollowerMotionPlayable : PlayableBehaviour
{
    #region Serialized attributes

    public enum Interpolator { Exponential, Spring, DampedSpring }
    public Interpolator interpolator = Interpolator.DampedSpring;
    public float positionSpeed = 2;
    public float rotationSpeed = 2;

    #endregion

    #region Runtime attributes

    public Transform Target { get; set; }

    #endregion

    #region Private members

    (float3 p, quaternion r) _origin;
    (float3 p, float4 r) _current;
    (float3 p, float4 r) _velocity;
    float _prevTime;

    static float4 Q2F4(quaternion q) => q.value;

    float CalculateDeltaTime(Playable playable)
    {
        var time = (float)playable.GetTime();
        var dt = math.abs(time - _prevTime);
        _prevTime = time;
        return dt;
    }

    #endregion

    #region PlayableBehaviour overrides

    // Clip initialization
    public override void OnBehaviourPlay(Playable playable, FrameData info)
      => _prevTime = -1;

    // Per-frame initialization
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        var self = info.output.GetUserData() as Transform;
        if (self == null || Target == null) return;

        // Original transform
        _origin = (self.position, Q2F4(self.rotation));
    }

    // Per-frame process
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var self = playerData as Transform;
        if (self == null || Target == null) return;

        if (_prevTime < 0)
        {
            // State initialization
            _current = (Target.position, Q2F4(Target.rotation));
            _velocity = (0, 0);
            _prevTime = (float)playable.GetTime();
        }

        // Parameters (delta time and clip weight)
        var dt = CalculateDeltaTime(playable);
        var w = info.weight;

        // Position interpolation
        if (positionSpeed > 0)
        {
            var target = (float3)Target.position;
            var speed = positionSpeed;

            if (interpolator == Interpolator.Exponential)
            {
                _current.p = math.lerp(target, _current.p, math.exp(speed * -dt));
            }
            else if (interpolator == Interpolator.Spring)
            {
                _velocity.p *= math.exp((1 + speed * 0.5f) * -dt);
                _velocity.p += (target - _current.p) * (speed * 0.1f);
                _current.p += _velocity.p * dt;
            }
            else // interpolator == Interpolator.DampedSpring
            {
                var n1 = _velocity.p - (_current.p - target) * (speed * speed * dt);
                var n2 = 1 + speed * dt;
                _velocity.p = n1 / (n2 * n2);
                _current.p += _velocity.p * dt;
            }

            self.position += (Vector3)((_current.p - _origin.p) * w);
        }

        // Rotation interpolation
        if (rotationSpeed > 0)
        {
            var target = Q2F4(Target.rotation);
            var speed = rotationSpeed;

            if (math.dot(_current.r, target) < 0) target = -target;

            if (interpolator == Interpolator.Exponential)
            {
                _current.r = math.lerp(target, _current.r, math.exp(speed * -dt));
            }
            else if (interpolator == Interpolator.Spring)
            {
                _velocity.r *= math.exp((1 + speed * 0.5f) * -dt);
                _velocity.r += (target - _current.r) * (speed * 0.1f);
                _current.r += _velocity.r * dt;
            }
            else // interpolator == Interpolator.DampedSpring
            {
                var n1 = _velocity.r - (_current.r - target) * (speed * speed * dt);
                var n2 = 1 + speed * dt;
                _velocity.r = n1 / (n2 * n2);
                _current.r += _velocity.r * dt;
            }

            // self += (current - origin) * w;
            var r = math.normalize(math.quaternion(_current.r));
            r = math.mul(math.inverse(_origin.r), r);
            r = math.nlerp(quaternion.identity, r, w);
            self.rotation = math.mul(r, self.rotation);
        }
    }

    #endregion
}

} // namespace Klak.Timeline
