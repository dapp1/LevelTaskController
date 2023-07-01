using UnityEditor;
using UnityEngine;

public class ScriptAutoAdder : UnityEditor.AssetModificationProcessor
{
    private static void OnWillCreateAsset(string assetName)
    {
        // ���������, ��� ��������� ������
        if (assetName.EndsWith(".cs"))
        {
            // �������� ������ ���� ������������ �������
            string scriptPath = assetName.Replace(".meta", "");

            // ���������, ��� ��� ������ ������ (����� "ScriptA" - ��� �������, ������� ������ ������������� ��������� ������ ������)
            if (scriptPath.EndsWith("TaskController.cs"))
            {
                // �������� ���� � ������� �������, ������� ����� �������� (����� "ScriptB" - ��� �������, ������� ����� �����������)
                string otherScriptPath = scriptPath.Replace("TaskController.cs", "ItemTrigger.cs");

                // ������� ������ �������-��������� ��� ���������� ������� �������
                MonoScript otherScript = AssetDatabase.LoadAssetAtPath<MonoScript>(otherScriptPath);
                if (otherScript != null)
                {
                    // ������� ��������� �������-���������
                    var editorInstance = ScriptableObject.CreateInstance(otherScript.GetClass());

                    // �������� ����� OnEnable() �������-��������� (���� �� ����)
                    var method = otherScript.GetClass().GetMethod("OnEnable");
                    if (method != null)
                    {
                        method.Invoke(editorInstance, null);
                    }

                    // ��������� ��������� ������-��������
                    AssetDatabase.AddObjectToAsset(editorInstance, scriptPath);
                    AssetDatabase.ImportAsset(scriptPath);

                    // ������� �����, ����� ����� �� ��������� �� ��������� �������
                    EditorGUIUtility.PingObject(editorInstance);
                }
            }
        }
    }
}
