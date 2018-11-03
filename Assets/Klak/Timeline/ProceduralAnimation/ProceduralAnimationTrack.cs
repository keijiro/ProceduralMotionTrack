// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline
{
    [TrackColor(0.4f, 0.4f, 0.4f)]
    [TrackClipType(typeof(BrownianAnimation))]
    [TrackBindingType(typeof(GameObject))]
    public class ProceduralAnimationTrack : TrackAsset
    {
        public ProceduralAnimationMixer template = new ProceduralAnimationMixer();

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<ProceduralAnimationMixer>.Create(graph, template, inputCount);
        }

        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
            var go = director.GetGenericBinding(this) as GameObject;
            if (go == null) return;

            var componentType = go.GetComponent(template.componentName).GetType();

            foreach (var m in driver.GetType().GetMethods())
            {
                if (m.Name != "AddFromName") continue;
                if (!m.IsGenericMethod) continue;

                var args = m.GetParameters();
                if (args.Length != 2) continue;
                if (args[0].ParameterType != typeof(GameObject)) continue;
                if (args[1].ParameterType != typeof(string)) continue;

                var m2 = m.MakeGenericMethod(componentType);
                m2.Invoke(driver, new object [] {go, template.fieldName});
            }
        }
    }
}
