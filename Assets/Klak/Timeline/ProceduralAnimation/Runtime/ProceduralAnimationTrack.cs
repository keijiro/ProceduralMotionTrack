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

            var component = go.GetComponent(template.componentName);
            if (component == null) return;

            driver.AddFromName(component.GetType(), go, template.fieldName);
        }
    }
}
