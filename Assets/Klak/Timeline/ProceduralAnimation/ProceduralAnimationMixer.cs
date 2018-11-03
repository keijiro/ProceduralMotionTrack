// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;
using System.Reflection;

namespace Klak.Timeline
{
    [System.Serializable]
    public class ProceduralAnimationMixer : PlayableBehaviour
    {
        #region Serialized variables

        public string componentName;
        public string propertyName;
        public string fieldName;

        #endregion

        #region Private variables

        System.Reflection.PropertyInfo _targetProperty;

        #endregion

        #region PlayableBehaviour overrides

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var acc = 0.0f;

            for (var i = 0; i < playable.GetInputCount(); i++)
            {
                var p = playable.GetInput(i);
                var b = (ScriptPlayable<BrownianAnimationPlayable>)p;
                acc += playable.GetInputWeight(i) * b.GetBehaviour().CurrentValue;
            }

            var target = playerData as GameObject;
            if (target == null) return;

            var component = target.GetComponent(componentName);
            if (component == null) return;

            if (_targetProperty == null)
                _targetProperty = component.GetType().GetProperty(propertyName);

            if (_targetProperty != null)
                _targetProperty.SetValue(component, acc, null);
        }

        #endregion
    }
}
