// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline
{
    [TrackColor(0.4f, 0.4f, 0.4f)]
    [TrackClipType(typeof(BrownianMotion))]
    [TrackClipType(typeof(ConstantMotion))]
    [TrackBindingType(typeof(Transform))]
    public class ProceduralMotionTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<ProceduralMotionMixer>.Create(graph, inputCount);
        }

        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
            var transform = director.GetGenericBinding(this) as Transform;
            if (transform == null) return;

            var go = transform.gameObject;
            driver.AddFromName<Transform>(go, "m_LocalPosition.x");
            driver.AddFromName<Transform>(go, "m_LocalPosition.y");
            driver.AddFromName<Transform>(go, "m_LocalPosition.z");
            driver.AddFromName<Transform>(go, "m_LocalRotation.x");
            driver.AddFromName<Transform>(go, "m_LocalRotation.y");
            driver.AddFromName<Transform>(go, "m_LocalRotation.z");
        }
    }
}
