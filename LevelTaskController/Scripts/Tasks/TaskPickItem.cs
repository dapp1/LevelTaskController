using UnityEngine;
using Task;

public class TaskPickItem : MonoBehaviour
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
        if (_taskController != null && _taskController.items.Count > _itemIndex)
        {
            _taskController.items[_itemIndex].collectedItems++;
        }
    }
}
