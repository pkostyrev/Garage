using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int Index => _index;
    public bool HasItem => _icon.enabled;

    [SerializeField] private Image _icon;
    [SerializeField] private Image _background;
    [SerializeField] private Color _selectColor;
    [SerializeField] private Color _defaultColor;

    private int _index;

    private void Awake()
    {
        _icon.enabled = false;
    }

    public void Init(int index)
    {
        _index = index;
        _icon.enabled = false;
    }

    public void SetItem(Sprite icon)
    {
        _icon.sprite = icon;
        _icon.enabled = true;
    }

    public void Clear()
    {
        _icon.enabled = false;
    }

    internal void Select(bool isSelect)
    {
        _background.color = isSelect ? _selectColor : _defaultColor;
    }
}
