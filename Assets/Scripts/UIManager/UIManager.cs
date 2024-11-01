using System;
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

    LinkedList<Inventory> _inventories = new LinkedList<Inventory>();
    Inventory _targetInventory;

    public void Init(IViewsManager viewsManager, IUserController userController)
    {
        _viewsManager = viewsManager;

        foreach (Inventory inventory in inventories)
        {
            if (inventory.Type == InventoryType.Main)
            {
                _inventories.AddLast(inventory);
                SelectIventory(inventory);
            }

            inventory.Init(_viewsManager.GetInventoryCapacity(inventory.Type));
        }

        Refresh();

        userController.OnInteractiveTypeChanged += ShowInfo;
        userController.OnNothingInteractive += HideInfo;
    }

    private void ShowInfo(InteractiveType type)
    {
        textInfo.text = _viewsManager.GetInteractiveText(type);
        textInfoRoot.SetActive(true);
    }

    public void HideInfo()
    {
        textInfoRoot.SetActive(false);
    }

    public void OpenInventory(InventoryType inventoryType)
    {
        Inventory inventory = inventories.Single(i => i.Type == inventoryType);
        _inventories.AddLast(inventory);
        SelectIventory(inventory);
        Refresh();
    }

    public void CloseInventory(InventoryType inventoryType)
    {
        Inventory inventory = inventories.Single(i => i.Type == inventoryType);
        _inventories.Remove(inventory);
        SelectIventory(inventories.Single(i => i.Type == InventoryType.Main));
        Refresh();
    }

    public void ChangeInventory()
    {
        if (_inventories.Last.Value == _targetInventory)
        {
            SelectIventory(_inventories.First.Value);
        }
        else
        {
            SelectIventory(_inventories.Find(_targetInventory).Next.Value);
        }
    }

    private void SelectIventory(Inventory selectInventory)
    {
        _targetInventory = selectInventory;

        foreach (Inventory inventory in _inventories)
        {
            inventory.Select(inventory == selectInventory);
        }
    }

    private void Refresh()
    {
        swappedInventoryInfo.SetActive(_inventories.Count(a => a == true) > 1);
    }
}
