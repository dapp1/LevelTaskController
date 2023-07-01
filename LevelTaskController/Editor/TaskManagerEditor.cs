using UnityEditor;
using UnityEngine;
using Task;

[CustomEditor(typeof(TaskController))]
public class TaskManagerEditor : Editor
{
    private SerializedProperty taskSelectProperty;
    private SerializedProperty activePickTaskProperty;
    private SerializedProperty activeKillTaskProperty;
    private SerializedProperty activeWTFTaskProperty;
    private SerializedProperty killsProperty;
    private SerializedProperty itemsProperty;
    private SerializedProperty wtfProperty;

    private void OnEnable()
    {
        taskSelectProperty = serializedObject.FindProperty("taskSelect");
        activePickTaskProperty = serializedObject.FindProperty("activePickTask");
        activeKillTaskProperty = serializedObject.FindProperty("activeKillTask");
        activeWTFTaskProperty = serializedObject.FindProperty("activeWTFTask");
        killsProperty = serializedObject.FindProperty("kills");
        itemsProperty = serializedObject.FindProperty("items");
        wtfProperty = serializedObject.FindProperty("wtf");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(taskSelectProperty);

        TaskController.TaskSelect selectedFunction = (TaskController.TaskSelect)taskSelectProperty.enumValueIndex;

        switch (selectedFunction)
        {
            case TaskController.TaskSelect.KillEnemy:
                EditorGUILayout.PropertyField(activeKillTaskProperty);
                if (activeKillTaskProperty.boolValue)
                {
                    EditorGUILayout.PropertyField(killsProperty);
                }
                break;

            case TaskController.TaskSelect.SoberiItems:
                EditorGUILayout.PropertyField(activePickTaskProperty);
                if (activePickTaskProperty.boolValue)
                {
                    EditorGUILayout.PropertyField(itemsProperty);
                }
                break;

            case TaskController.TaskSelect.RunBarryRun:
                EditorGUILayout.PropertyField(activeWTFTaskProperty);
                if (activeWTFTaskProperty.boolValue)
                {
                    EditorGUILayout.PropertyField(wtfProperty);
                }
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
