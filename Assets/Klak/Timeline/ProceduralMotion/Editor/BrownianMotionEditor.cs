// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Timeline
{
    [CustomEditor(typeof(BrownianMotion)), CanEditMultipleObjects]
    class BrownianMotionEditor : Editor
    {
        SerializedProperty _positionAmount;
        SerializedProperty _rotationAmount;
        SerializedProperty _frequency;
        SerializedProperty _octaves;
        SerializedProperty _randomSeed;

        static class Styles
        {
            public static readonly GUIContent position = new GUIContent("Position");
            public static readonly GUIContent rotation = new GUIContent("Rotation");
        }

        void OnEnable()
        {
            _positionAmount = serializedObject.FindProperty("template.positionAmount");
            _rotationAmount = serializedObject.FindProperty("template.rotationAmount");
            _frequency = serializedObject.FindProperty("template.frequency");
            _octaves = serializedObject.FindProperty("template.octaves");
            _randomSeed = serializedObject.FindProperty("template.randomSeed");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Noise Amount");
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_positionAmount, Styles.position);
            EditorGUILayout.PropertyField(_rotationAmount, Styles.rotation);
            EditorGUI.indentLevel--;

            EditorGUILayout.PropertyField(_frequency);
            EditorGUILayout.IntSlider(_octaves, 1, 9);
            EditorGUILayout.PropertyField(_randomSeed);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
