using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemViewManager : MonoBehaviour, IItemViewManger
{
    private Dictionary<string, ItemConfig> _itemsConfig = new Dictionary<string, ItemConfig>();

    private IConfigManager _configManager;

    internal void Init(IConfigManager configManager)
    {
        _configManager = configManager;

        foreach (ItemConfig itemConfig in _configManager.GameSettings.ItemConfigs)
        {
            _itemsConfig.Add(itemConfig.name, itemConfig);
        }
    }

    public Sprite GetItemIcon(string itemName)
    {
        if (_itemsConfig.TryGetValue(itemName, out ItemConfig itemConfig))
        {
            return itemConfig.Icon;
        }

        Debug.LogError($"Missing sprite for item, name: {itemName}");
        return null;
    }
}
