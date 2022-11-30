using UnityEditor;
using UnityEngine;

namespace KartGame.KartSystems
{
    /// <summary>
    /// Editor for the KartMovement MonoBehaviour.
    /// </summary>
    [CustomEditor(typeof(KartMovement))]
    public class KartMovementEditor : Editor
    {
        private SerializedProperty m_ScriptProp;
        private SerializedProperty m_KartProp;
        private SerializedProperty m_DriverProp;
        private SerializedProperty m_AirborneModifierProp;
        private SerializedProperty m_InputProp;
        private SerializedProperty m_FrontLeftGroundRaycastProp;
        private SerializedProperty m_FrontRightGroundRaycastProp;
        private SerializedProperty m_RearLeftGroundRaycastProp;
        private SerializedProperty m_RearRightGroundRaycastProp;
        private SerializedProperty m_DefaultStatsProp;
        private SerializedProperty m_GroundLayersProp;
        private SerializedProperty m_AllCollidingLayersProp;
        private SerializedProperty m_AirborneOrientationSpeedProp;
        private SerializedProperty m_MinDriftingSteeringProp;
        private SerializedProperty m_RotationCorrectionSpeedProp;
        private SerializedProperty m_MinDriftStartAngleProp;
        private SerializedProperty m_MaxDriftStartAngleProp;
        private SerializedProperty m_KartToKartBumpProp;
        private SerializedProperty m_OnBecomeAirborneProp;
        private SerializedProperty m_OnBecomeGroundedProp;
        private SerializedProperty m_OnHopProp;
        private SerializedProperty m_OnDriftStartedProp;
        private SerializedProperty m_OnDriftStoppedProp;
        private SerializedProperty m_OnKartCollisionProp;

        private GUIContent m_RaycastPositionsContent = new GUIContent("Raycast Positions");
        private GUIContent m_OtherSettingsContent = new GUIContent("Other Settings", "These settings control how the kart behaves but are not part of its stat block.  It is recommended you don't adjust these without careful understanding of what they do.");
        private GUIContent m_EventsContent = new GUIContent("Events");

        private void OnEnable()
        {
            m_ScriptProp = serializedObject.FindProperty("m_Script");
            m_KartProp = serializedObject.FindProperty("kart");
            m_DriverProp = serializedObject.FindProperty("driver");
            m_AirborneModifierProp = serializedObject.FindProperty("airborneModifier");
            m_InputProp = serializedObject.FindProperty("input");
            m_FrontLeftGroundRaycastProp = serializedObject.FindProperty("frontGroundRaycast");
            m_FrontRightGroundRaycastProp = serializedObject.FindProperty("rightGroundRaycast");
            m_RearLeftGroundRaycastProp = serializedObject.FindProperty("leftGroundRaycast");
            m_RearRightGroundRaycastProp = serializedObject.FindProperty("rearGroundRaycast");
            m_DefaultStatsProp = serializedObject.FindProperty("defaultStats");
            m_GroundLayersProp = serializedObject.FindProperty("groundLayers");
            m_AllCollidingLayersProp = serializedObject.FindProperty("allCollidingLayers");
            m_AirborneOrientationSpeedProp = serializedObject.FindProperty("airborneOrientationSpeed");
            m_MinDriftingSteeringProp = serializedObject.FindProperty("minDriftingSteering");
            m_RotationCorrectionSpeedProp = serializedObject.FindProperty("rotationCorrectionSpeed");
            m_MinDriftStartAngleProp = serializedObject.FindProperty("minDriftStartAngle");
            m_MaxDriftStartAngleProp = serializedObject.FindProperty("maxDriftStartAngle");
            m_KartToKartBumpProp = serializedObject.FindProperty("kartToKartBump");
            m_OnBecomeAirborneProp = serializedObject.FindProperty("OnBecomeAirborne");
            m_OnBecomeGroundedProp = serializedObject.FindProperty("OnBecomeGrounded");
            m_OnHopProp = serializedObject.FindProperty("OnHop");
            m_OnDriftStartedProp = serializedObject.FindProperty("OnDriftStarted");
            m_OnDriftStoppedProp = serializedObject.FindProperty("OnDriftStopped");
            m_OnKartCollisionProp = serializedObject.FindProperty("OnKartCollision");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.PropertyField(m_ScriptProp);
            GUI.enabled = true;

            EditorGUILayout.PropertyField(m_KartProp);
            EditorGUILayout.PropertyField(m_DriverProp);
            EditorGUILayout.PropertyField(m_AirborneModifierProp);
            EditorGUILayout.PropertyField(m_InputProp);

            m_FrontLeftGroundRaycastProp.isExpanded = EditorGUILayout.Foldout(m_FrontLeftGroundRaycastProp.isExpanded, m_RaycastPositionsContent);

            if (m_FrontLeftGroundRaycastProp.isExpanded)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_FrontLeftGroundRaycastProp);
                EditorGUILayout.PropertyField(m_FrontRightGroundRaycastProp);
                EditorGUILayout.PropertyField(m_RearLeftGroundRaycastProp);
                EditorGUILayout.PropertyField(m_RearRightGroundRaycastProp);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(m_DefaultStatsProp, true);

            EditorGUILayout.PropertyField(m_GroundLayersProp);
            EditorGUILayout.PropertyField(m_AllCollidingLayersProp);

            m_AirborneOrientationSpeedProp.isExpanded = EditorGUILayout.Foldout(m_AirborneOrientationSpeedProp.isExpanded, m_OtherSettingsContent);

            if (m_AirborneOrientationSpeedProp.isExpanded)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_AirborneOrientationSpeedProp);
                EditorGUILayout.PropertyField(m_MinDriftingSteeringProp);
                EditorGUILayout.PropertyField(m_RotationCorrectionSpeedProp);
                EditorGUILayout.PropertyField(m_MinDriftStartAngleProp);
                EditorGUILayout.PropertyField(m_MaxDriftStartAngleProp);
                EditorGUILayout.PropertyField(m_KartToKartBumpProp);
                EditorGUI.indentLevel--;
            }

            m_OnBecomeAirborneProp.isExpanded = EditorGUILayout.Foldout(m_OnBecomeAirborneProp.isExpanded, m_EventsContent);

            if (m_OnBecomeAirborneProp.isExpanded)
            {
                EditorGUILayout.PropertyField(m_OnBecomeAirborneProp);
                EditorGUILayout.PropertyField(m_OnBecomeGroundedProp);
                EditorGUILayout.PropertyField(m_OnHopProp);
                EditorGUILayout.PropertyField(m_OnDriftStartedProp);
                EditorGUILayout.PropertyField(m_OnDriftStoppedProp);
                EditorGUILayout.PropertyField(m_OnKartCollisionProp);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}