using System.Collections.Generic;
using UnityEngine;

public class ViewsManager : MonoBehaviour, IViewsManager
{
    public IItemViewManger ItemViewManger => _itemViewManager;

    [SerializeField] ItemViewManager _itemViewManager;

    private IConfigManager _configManager;

    private Dictionary<InventoryType, int> inventories = new Dictionary<InventoryType, int>();
    private Dictionary<InteractiveType, string> _interactiveTexts = new Dictionary<InteractiveType, string>();

    public void Init(IConfigManager configManager)
    {
        _configManager = configManager;

        _itemViewManager.Init(configManager);

        foreach (GameSettings.InteractiveText interactiveText in _configManager.GameSettings.InteractiveTexts)
        {
            _interactiveTexts.Add(interactiveText.type, interactiveText.text);
        }

        foreach (GameSettings.InventoryConfig inventoryConfig in _configManager.GameSettings.Inventories)
        {
            inventories.Add(inventoryConfig.type, inventoryConfig.capacity);
        }
    }

    public string GetInteractiveText(InteractiveType type)
    {
        return _interactiveTexts[type];
    }

    public int GetInventoryCapacity(InventoryType type)
    {
        return inventories[type];
    }
}
