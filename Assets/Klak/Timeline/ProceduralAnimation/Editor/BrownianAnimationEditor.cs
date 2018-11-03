// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Timeline
{
    [CustomEditor(typeof(BrownianAnimation)), CanEditMultipleObjects]
    class BrownianAnimationEditor : Editor
    {
        SerializedProperty _frequency;
        SerializedProperty _octaves;
        SerializedProperty _randomSeed;

        void OnEnable()
        {
            _frequency = serializedObject.FindProperty("template.frequency");
            _octaves = serializedObject.FindProperty("template.octaves");
            _randomSeed = serializedObject.FindProperty("template.randomSeed");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_frequency);
            EditorGUILayout.IntSlider(_octaves, 1, 9);
            EditorGUILayout.PropertyField(_randomSeed);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
