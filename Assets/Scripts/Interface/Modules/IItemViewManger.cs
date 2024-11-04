using UnityEngine;

public interface IItemViewManger
{
    bool IsItem(string key);
    Sprite GetItemIcon(string itemName);
}
