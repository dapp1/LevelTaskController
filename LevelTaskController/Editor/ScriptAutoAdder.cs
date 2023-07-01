using UnityEditor;
using UnityEngine;

public class ScriptAutoAdder : UnityEditor.AssetModificationProcessor
{
    private static void OnWillCreateAsset(string assetName)
    {
        // Проверяем, что создается скрипт
        if (assetName.EndsWith(".cs"))
        {
            // Получаем полный путь создаваемого скрипта
            string scriptPath = assetName.Replace(".meta", "");

            // Проверяем, что это нужный скрипт (здесь "ScriptA" - имя скрипта, который должен автоматически добавлять другой скрипт)
            if (scriptPath.EndsWith("TaskController.cs"))
            {
                // Получаем путь к другому скрипту, который нужно добавить (здесь "ScriptB" - имя скрипта, который будет добавляться)
                string otherScriptPath = scriptPath.Replace("TaskController.cs", "ItemTrigger.cs");

                // Создаем объект скрипта-редактора для добавления другого скрипта
                MonoScript otherScript = AssetDatabase.LoadAssetAtPath<MonoScript>(otherScriptPath);
                if (otherScript != null)
                {
                    // Создаем экземпляр скрипта-редактора
                    var editorInstance = ScriptableObject.CreateInstance(otherScript.GetClass());

                    // Вызываем метод OnEnable() скрипта-редактора (если он есть)
                    var method = otherScript.GetClass().GetMethod("OnEnable");
                    if (method != null)
                    {
                        method.Invoke(editorInstance, null);
                    }

                    // Сохраняем созданный скрипт-редактор
                    AssetDatabase.AddObjectToAsset(editorInstance, scriptPath);
                    AssetDatabase.ImportAsset(scriptPath);

                    // Очищаем фокус, чтобы фокус не оставался на созданном скрипте
                    EditorGUIUtility.PingObject(editorInstance);
                }
            }
        }
    }
}
