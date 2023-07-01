using System;
using System.Collections.Generic;
using UnityEngine;

namespace Task
{
    public class TaskController : MonoBehaviour
    {
        public TaskSelect taskSelect;

        public bool activePickTask;
        public bool activeKillTask;
        public bool activeWTFTask;

        public List<KillsEnemy> kills = new List<KillsEnemy>();
        public List<PickItem> items = new List<PickItem>();
        public List<WhatDo> wtf = new List<WhatDo>();

        private void Update()
        {
            TaskComplete();
        }

        public void TaskComplete()
        {
            if (activeKillTask)
            {
                for (int i = 0; i < kills.Count; i++)
                {
                    if (kills[i].killsComplete)
                        continue;

                    if (kills[i].enemyCount <= kills[i].counts)
                    {

                        kills[i].actionKills.Invoke();
                        Debug.Log("ok");
                        kills[i].killsComplete = true;
                    }
                }
            }

            if (activePickTask)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (!items[i].pickComplete)
                        continue;

                    AddScriptToObjectsWithTag<TaskPickItem>(items[i].items, i);

                    if (items[i].collectedItems >= items[i].items.Count)
                    {
                        items[i].actionPicks.Invoke();
                        items[i].pickComplete = true;
                    }
                }
            }
        }

        public static void SubscribeEnemy<T>(T enemy, GameObject targetObject) where T : Enum
        {
            if (FindObjectOfType<TaskController>() != null)
            {
                int itemIndex = Convert.ToInt32(enemy);
                FindObjectOfType<TaskController>().AddScriptToObjectsWithTag<TaskKillEnemy>(itemIndex, enemy.ToString(), targetObject);
            }
        }

        void AddScriptToObjectsWithTag<T>(int itemIndex, string name, GameObject targetObject) where T : MonoBehaviour
        {
            for (int i = 0; i < kills.Count; i++)
            {
                if (kills[i].nameTag == name)
                {
                    itemIndex = i;
                }
            }

            // Проверяем, что скрипт еще не добавлен на объект
            if (targetObject.GetComponent<T>() == null)
            {
                T script = targetObject.AddComponent<T>();
                if (script is TaskKillEnemy taskKillEnemy)
                {
                    taskKillEnemy.Initialize(this, itemIndex);
                }
            }
        }

        void AddScriptToObjectsWithTag<T>(List<GameObject> list, int itemIndex) where T : MonoBehaviour
        {
            if (list != null && list.Count > 0)
            {
                foreach (GameObject obj in list)
                {
                    // Проверяем, что скрипт еще не добавлен на объект
                    if (obj.GetComponent<T>() == null)
                    {
                        // Добавляем скрипт на объект
                        T script = obj.AddComponent<T>();
                        if (script is TaskPickItem taskPickItem)
                        {
                            taskPickItem.Initialize(this, itemIndex);
                        }
                    }
                }
            }
        }

        public enum TaskSelect
        {
            KillEnemy,
            SoberiItems,
            RunBarryRun
        }
    }
}
