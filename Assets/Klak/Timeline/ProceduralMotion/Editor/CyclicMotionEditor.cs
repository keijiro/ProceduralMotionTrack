// Custom timeline track for procedural motion
// https://github.com/keijiro/ProceduralMotionTrack

using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Klak.Timeline
{
    [CustomEditor(typeof(CyclicMotion)), CanEditMultipleObjects]
    class CyclicMotionEditor : Editor
    {
        SerializedProperty _positionAmount;
        SerializedProperty _positionFrequency;
        SerializedProperty _rotationAmount;
        SerializedProperty _rotationFrequency;

        static class Styles
        {
            public static readonly GUIContent amount = new GUIContent("Amount");
            public static readonly GUIContent frequency = new GUIContent("Frequency");
        }

        void OnEnable()
        {
            _positionAmount = serializedObject.FindProperty("template.positionAmount");
            _positionFrequency = serializedObject.FindProperty("template.positionFrequency");
            _rotationAmount = serializedObject.FindProperty("template.rotationAmount");
            _rotationFrequency = serializedObject.FindProperty("template.rotationFrequency");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Position");
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_positionAmount, Styles.amount);
            EditorGUILayout.PropertyField(_positionFrequency, Styles.frequency);
            EditorGUI.indentLevel--;

            EditorGUILayout.LabelField("Rotation");
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_rotationAmount, Styles.amount);
            EditorGUILayout.PropertyField(_rotationFrequency, Styles.frequency);
            EditorGUI.indentLevel--;

            serializedObject.ApplyModifiedProperties();
        }
    }
}
