using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IUIManager
{
    public Inventory TargetInventory => _targetInventory;

    [SerializeField] private TextMeshProUGUI textInfo;
    [SerializeField] private GameObject textInfoRoot;
    [SerializeField] private GameObject swappedInventoryInfo;
    [SerializeField] private Inventory[] inventories;

    IViewsManager _viewsManager;

    LinkedList<Inventory> _openedInventories = new LinkedList<Inventory>();
    Inventory _mainIventories;
    Inventory _targetInventory;
    int _shownInventoryesCount = 1;

    public void Init(IViewsManager viewsManager)
    {
        _viewsManager = viewsManager;

        foreach (Inventory inventory in inventories)
        {
            if (inventory.Type == InventoryType.Main)
            {
                _mainIventories = inventory;
                _openedInventories.AddLast(inventory);
                SelectIventory(inventory);
            }

            inventory.Init(_viewsManager.GetInventoryCapacity(inventory.Type));
        }

        Refresh();
    }

    public void ShowInfo(InteractiveType type)
    {
        textInfo.text = _viewsManager.GetInteractiveText(type);
        textInfoRoot.SetActive(true);
    }

    public void HideInfo()
    {
        textInfoRoot.SetActive(false);
    }

    public bool IsOpenedInventory(InventoryType inventoryType)
    {
        Inventory inventory = inventories.Single(i => i.Type == inventoryType);
        return _openedInventories.Contains(inventory);
    }

    public void OpenInventory(InventoryType inventoryType)
    {
        Inventory inventory = inventories.Single(i => i.Type == inventoryType);

        if (!_openedInventories.Contains(inventory))
        {
            _shownInventoryesCount++;
            _openedInventories.AddLast(inventory);
            SelectIventory(inventory);
            Refresh();
            inventory.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError($"Attempt to open opened inventory. Inventory name: {inventoryType}");
        }
    }

    public void CloseInventory(InventoryType inventoryType)
    {
        Inventory inventory = inventories.Single(i => i.Type == inventoryType);

        if (_openedInventories.Contains(inventory))
        {
            _shownInventoryesCount--;
            inventory.gameObject.SetActive(false);
            _openedInventories.Remove(inventory);
            SelectIventory(_mainIventories);
            Refresh();
        }
        else
        {
            Debug.LogError($"Attempt to close closed inventory. Inventory name: {inventoryType}");
        }
    }

    public void ChangeOpennessInventory(InventoryType inventoryType)
    {
        Inventory inventory = inventories.Single(i => i.Type == inventoryType);

        if (_openedInventories.Contains(inventory))
        {
            _shownInventoryesCount--;
            inventory.gameObject.SetActive(false);
            _openedInventories.Remove(inventory);
            inventory = inventories.Single(i => i.Type == InventoryType.Main);
        }
        else
        {
            _shownInventoryesCount++;
            _openedInventories.AddLast(inventory);
            inventory.gameObject.SetActive(true);
        }

        SelectIventory(inventory);
        Refresh();
    }

    public void ShowInventory(InventoryType inventoryType)
    {
        Inventory inventory = inventories.Single(i => i.Type == inventoryType);

        if (_openedInventories.Contains(inventory))
        {
            _shownInventoryesCount++;
            Refresh();
            inventory.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError($"Atempt to show shown inventory. Inventory name: {inventoryType}");
        }
    }

    public void HideInventory(InventoryType inventoryType)
    {
        Inventory inventory = inventories.Single(i => i.Type == inventoryType);

        if (_openedInventories.Contains(inventory))
        {
            _shownInventoryesCount--;
            inventory.gameObject.SetActive(false);
            SelectIventory(_mainIventories);
            Refresh();
        }
        else
        {
            Debug.LogError($"Attempt to close closed inventory. Inventory name: {inventoryType}");
        }
    }

    public void HideOtherInventory()
    {
        foreach (Inventory inventory in _openedInventories)
        {
            if (inventory != _mainIventories)
            {
                inventory.gameObject.SetActive(false);
            }
        }

        _shownInventoryesCount = 1;
        SelectIventory(_mainIventories);
        Refresh();
    }

    public void ChangeInventory()
    {
        if (_openedInventories.Last.Value == _targetInventory)
        {
            SelectIventory(_openedInventories.First.Value);
        }
        else
        {
            SelectIventory(_openedInventories.Find(_targetInventory).Next.Value);
        }
    }

    private void SelectIventory(Inventory selectInventory)
    {
        _targetInventory = selectInventory;

        foreach (Inventory inventory in _openedInventories)
        {
            inventory.Select(inventory == selectInventory);
        }
    }

    private void Refresh()
    {
        swappedInventoryInfo.SetActive(_shownInventoryesCount > 1);
    }
}
