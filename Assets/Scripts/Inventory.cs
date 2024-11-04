using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot SelectSlot => _selectSlot;
    public InventoryType Type => _type;

    [SerializeField] private GameObject _slotRoot;
    [SerializeField] private InventorySlot _inventorySlot;
    [SerializeField] private InventoryType _type;
    [SerializeField] private GameObject _interactiveOff;

    InventorySlot _selectSlot;
    LinkedList<InventorySlot> _inventorySlots = new LinkedList<InventorySlot>();

    internal void Init(int capacity)
    {
        _slotRoot.transform.DetachChildren();

        for (int i = 0; i < capacity; i++)
        {
            InventorySlot inventorySlot = Instantiate(_inventorySlot, _slotRoot.transform);
            inventorySlot.Init(i);
            _inventorySlots.AddLast(inventorySlot);
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

    public void RemoveItem(int index)
    {
        InventorySlot slot = _inventorySlots.ElementAt(index);

        if(slot.HasItem)
        {
            slot.Clear();
        }
    }

    public void Select(bool isSelect)
    {
        _interactiveOff.SetActive(!isSelect);

        if (_selectSlot == null)
        {
            _selectSlot = _inventorySlots.First.Value;
        }

        _selectSlot.Select(isSelect);
    }

    public void Next(bool next)
    {
        _selectSlot.Select(false);

        LinkedListNode<InventorySlot> node = _inventorySlots.Find(_selectSlot);
        
        _selectSlot = next ? 
            node.Next != null ? 
                node.Next.Value : 
                _inventorySlots.First.Value : 
            node.Previous != null ? 
                node.Previous.Value : 
                _inventorySlots.Last.Value;

        _selectSlot.Select(true);
    }
}
