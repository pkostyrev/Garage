using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] private TextMeshProUGUI textInfo;
    [SerializeField] private GameObject textInfoRoot;
    [SerializeField] private GameObject inventoryHelpInfo;
    [SerializeField] private Inventory[] inventories;

    Dictionary<InventoryType, Inventory> _inventories = new Dictionary<InventoryType, Inventory>();

    InventoryType _targetInventoryTipe;

    public void Init()
    {
        foreach (Inventory inventory in inventories)
        {
            _inventories.Add(inventory.Type, inventory);
        }

        _targetInventoryTipe = InventoryType.Main;
    }

    public void ShowInfo(string text)
    {
        textInfo.text = text;
        textInfoRoot.SetActive(true);
    }

    public void HideInfo()
    {
        textInfoRoot.SetActive(false);
    }

    public Inventory GetInventory(InventoryType type)
    {
        return _inventories[type];
    }

    public void InitInventory(InventoryType type, int capacity)
    {
        _inventories[type].Init(capacity);

        if(type == _targetInventoryTipe)
        {
            SelectIventory(type);
        }
    }

    public void ShowInventory(InventoryType type) => _inventories[type].gameObject.SetActive(true);

    public void ShowSelectInventory(InventoryType type)
    {
        SelectIventory(type);
        ShowInventory(type);
    }

    public void HideInventory(InventoryType type)
    {
        _inventories[type].gameObject.SetActive(false);
        SelectIventory(InventoryType.Main);
    }

    public void SwitchToInventory(InventoryType type) => SelectIventory(type);

    public void ChangeVisibilityInventoryHelpInfo(bool visibel)
    {
        inventoryHelpInfo.SetActive(visibel);
    }

    public void SelectItem(bool next)
    {
        _inventories[_targetInventoryTipe].Next(next);
    }

    public int GetSelectSlotIndex()
    {
        return _inventories[_targetInventoryTipe].SelectSlot.Index;
    }

    private void SelectIventory(InventoryType type)
    {
        _inventories[_targetInventoryTipe].Select(false);
        _targetInventoryTipe = type;
        _inventories[_targetInventoryTipe].Select(true);
    }
}
