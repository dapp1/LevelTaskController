using UnityEngine;
using Task;

public class TaskKillEnemy : MonoBehaviour
{
    private TaskController _taskController;
    private int _itemIndex; // Индекс элемента в списке

    public void Initialize(TaskController taskController, int itemIndex)
    {
        _taskController = taskController;
        _itemIndex = itemIndex;
    }

    private void OnDisable()
    {
        if (_taskController != null && _taskController.kills.Count > _itemIndex)
        {
            _taskController.kills[_itemIndex].counts++;
            Debug.Log(_taskController.kills[_itemIndex].counts);
        }
    }
}
