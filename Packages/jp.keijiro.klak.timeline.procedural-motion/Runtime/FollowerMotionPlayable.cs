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

    bool _valid;
    float3 _p, _vp;
    float4 _r, _vr;

    float4 Q2F4(quaternion q) => q.value;

    #endregion

    #region PlayableBehaviour overrides

    public override void OnBehaviourPlay(Playable playable, FrameData info)
      => _valid = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var self = playerData as Transform;
        if (self == null || Target == null) return;

        if (!_valid)
        {
            (_p, _vp) = (self.position, 0);
            (_r, _vr) = (Q2F4(self.rotation), 0);
            _valid = true;
        }

        var dt = math.abs((float)(playable.GetTime() - playable.GetPreviousTime()));
        var w = info.weight;

        if (positionSpeed > 0)
        {
            var pt = (float3)Target.position;
            var sp = positionSpeed;

            if (interpolator == Interpolator.Exponential)
            {
                _p = math.lerp(pt, _p, math.exp(sp * -dt));
            }
            else if (interpolator == Interpolator.Spring)
            {
                _vp *= math.exp((1 + sp * 0.5f) * -dt);
                _vp += (pt - _p) * (sp * 0.1f);
                _p += _vp * dt;
            }
            else // interpolator == Interpolator.DampedSpring
            {
                var n1 = _vp - (_p - pt) * (sp * sp * dt);
                var n2 = 1 + sp * dt;
                _vp = n1 / (n2 * n2);
                _p += _vp * dt;
            }

            self.position = math.lerp(self.position, _p, w);
        }

        if (rotationSpeed > 0)
        {
            var rt = Q2F4(Target.rotation);
            var sp = rotationSpeed;

            if (math.dot(_r, rt) < 0) rt = -rt;

            if (interpolator == Interpolator.Exponential)
            {
                _r = math.lerp(rt, _r, math.exp(sp * -dt));
            }
            else if (interpolator == Interpolator.Spring)
            {
                _vr *= math.exp((1 + sp * 0.5f) * -dt);
                _vr += (rt - _r) * (sp * 0.1f);
                _r += _vr * dt;
            }
            else // interpolator == Interpolator.DampedSpring
            {
                var n1 = _vr - (_r - rt) * (sp * sp * dt);
                var n2 = 1 + sp * dt;
                _vr = n1 / (n2 * n2);
                _r += _vr * dt;
            }

            var r = math.lerp(Q2F4(self.rotation), _r, w);
            self.rotation = math.normalize(math.quaternion(r));
        }
    }

    #endregion
}

} // namespace Klak.Timeline
