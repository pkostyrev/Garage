using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool HasItem => _icon.enabled;

    [SerializeField] private Image _icon;
    [SerializeField] private Image _background;
    [SerializeField] private Color _selectColor;
    [SerializeField] private Color _defaultColor;

    private void Awake()
    {
        _icon.enabled = false;
    }

    public void SetItem(string itemKey)
    {
        _icon.sprite = Root.ViewsManager.ItemViewManger.GetItemIcon(itemKey);
        _icon.enabled = true;
    }

    private void Clear()
    {
        _icon.enabled = false;
    }

    internal void SetSelect(bool isSelect)
    {
        _background.color = isSelect ? _selectColor : _defaultColor;
    }
}
