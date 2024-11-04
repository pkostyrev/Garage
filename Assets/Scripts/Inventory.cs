using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryType Type => _type;

    [SerializeField] private GameObject _slotRoot;
    [SerializeField] private InventorySlot _inventorySlot;
    [SerializeField] private InventoryType _type;
    [SerializeField] private GameObject _interactiveOff;

    List<InventorySlot> _inventorySlots = new List<InventorySlot>();

    internal void Init(int capacity)
    {
        _slotRoot.transform.DetachChildren();

        for (int i = 0; i < capacity; i++)
        {
            InventorySlot inventorySlot = Instantiate(_inventorySlot, _slotRoot.transform);
            inventorySlot.Init(i);
            _inventorySlots.Add(inventorySlot);
        }
    }

    public int TryAddItem(Sprite icon)
    {
        foreach (InventorySlot inventorySlot in _inventorySlots)
        {
            if (!inventorySlot.HasItem)
            {
                inventorySlot.SetItem(icon);
                return inventorySlot.Index;
            }
        }

        return -1;
    }

    public void Select(bool isSelect)
    {
        _interactiveOff.SetActive(!isSelect);
    }

    public void SelectItem(int index)
    {
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            _inventorySlots[index].SetSelect(i == index);
        }
    }
}
