using UnityEngine; // for debug.log
using UnityEditor;


[CustomEditor(typeof(Shooting_Arrow_Device_Behavior))]
public class Custom_Shooting_Device_Editor : Editor
{


    SerializedProperty repeatRate;
    SerializedProperty delay;
    SerializedProperty burstDelay;
    SerializedProperty firingMode;

    public void OnEnable()
    {
        
        repeatRate = serializedObject.FindProperty("repeatRate");
        delay = serializedObject.FindProperty("delay");
        burstDelay = serializedObject.FindProperty("burstDelay");
        firingMode = serializedObject.FindProperty("firingMode");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        base.OnInspectorGUI();
        
        Shooting_Arrow_Device_Behavior myScript = target as Shooting_Arrow_Device_Behavior;

        string[] firingModeLabels = new string[] { "None", "Automatic", "Trigger", "Burst", "Random", "Homing", "Trigger Homing" };  
        firingMode.enumValueIndex = EditorGUILayout.Popup("Firing Mode", (int)myScript.PropFiringMode, firingModeLabels);


        //Display group when choosen
        int mode = firingMode.enumValueIndex;

        if (mode == 1 || mode == 3 || mode == 5 || mode == 6)
        {
            using (var group = new EditorGUILayout.FadeGroupScope(1f))
            {
                EditorGUI.indentLevel++;
                repeatRate.floatValue = EditorGUILayout.Slider("Delay between shots", repeatRate.floatValue, 1, 0);
                delay.floatValue = EditorGUILayout.Slider("Initial Delay", delay.floatValue, 0, 10);
                if(mode == 3) burstDelay.floatValue = EditorGUILayout.Slider("Burst Delay", burstDelay.floatValue, 0, 10);
            }
            //Values doesn't change during gameplay;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
