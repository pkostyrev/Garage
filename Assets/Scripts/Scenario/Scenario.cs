using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scenario : MonoBehaviour, IScenario
{
    const InventoryType MAIN_INVENTORY_TYPE = InventoryType.Main;

    private IViewsManager _viewsManager;
    private IUserController _userController;
    private IInputManager _inputManager;
    private IUIManager _uIManager;

    private bool _hasInteractiveElement;
    private IInteractiveElement _targetInteractiveElement;

    InventoryType _targetShownInventoryType;

    Dictionary<string, InventoryType> _inventoryNames = new Dictionary<string, InventoryType>();
    Dictionary<InventoryType, bool> _shownInventories = new Dictionary<InventoryType, bool>();
    Dictionary<InventoryType, List<string>> _itemsInInventories = new Dictionary<InventoryType, List<string>>();
    Dictionary<string, int> _indexOfItems = new Dictionary<string, int>();

    public void Init(IViewsManager viewsManager, IUserController userController, IInputManager inputManager, IUIManager uIManager)
    {
        _viewsManager = viewsManager;
        _userController = userController;
        _inputManager = inputManager;
        _uIManager = uIManager;

        _inputManager.OnInteractive += OnInteractiveInput;
        _userController.OnSelectInteractive += OnSelectInteractive;
        _userController.OnNothingInteractive += OnNothingInteractive;

        InitInentories();
    }

    private void OnSelectInteractive(IInteractiveElement element)
    {
        if (_hasInteractiveElement
             && _inventoryNames.ContainsKey(_targetInteractiveElement.Name)
             && _shownInventories.TryGetValue(_inventoryNames[_targetInteractiveElement.Name], out bool shown)
             && shown)
        {
            HideInventory(_inventoryNames[_targetInteractiveElement.Name]);
        }

        if (_inventoryNames.ContainsKey(element.Name)
            && _shownInventories.TryGetValue(_inventoryNames[element.Name], out bool shownElememt)
            && !shownElememt)
        {
            ShowInventory(_inventoryNames[element.Name]);
        }

        _uIManager.ShowInfo(_viewsManager.GetInteractiveText(element.InteractiveType));
        _targetInteractiveElement = element;
        _hasInteractiveElement = true;
    }

    private void OnInteractiveInput(InteractiveType type)
    {
        if (_hasInteractiveElement)
        {
            switch (type)
            {
                case InteractiveType.Interact:

                    if (_inventoryNames.ContainsKey(_targetInteractiveElement.Name))
                    {
                        InteractInventory();
                    }

                    if (_targetInteractiveElement.InteractiveType == type)
                    {
                        _targetInteractiveElement.Interact();
                    }

                    break;

                case InteractiveType.Pickup:

                    if (_targetInteractiveElement.InteractiveType == type && TryPickup())
                    {
                        _targetInteractiveElement.Interact();
                    }

                    break;

                case InteractiveType.Put:

                    TryPutItem();

                    break;

                case InteractiveType.ChangeInventory:

                    if (_shownInventories.Count(i => i.Value) > 1)
                    {
                        ChangeInventory();
                    }

                    break;
            }
        }

        if (type is InteractiveType.PreviousItem)
        {
            _uIManager.SelectItem(false);
        }

        if (type is InteractiveType.NextItem)
        {
            _uIManager.SelectItem(true);
        }

        if (type is InteractiveType.Exit)
        {
            Root.ExitGame();
        }
    }

    private void TryPutItem()
    {
        if (_shownInventories.Count(i => i.Value) > 1)
        {
            int selectSlotIndex = _uIManager.GetSelectSlotIndex();
            List<string> items = new List<string>(_itemsInInventories[_targetShownInventoryType]);
            foreach (string itemInInventory in items)
            {
                if (_indexOfItems[itemInInventory] == selectSlotIndex)
                {
                    InventoryType to = _targetShownInventoryType;
                    InventoryType from = to == MAIN_INVENTORY_TYPE ?
                        _shownInventories.First(i => i.Key != MAIN_INVENTORY_TYPE && i.Value).Key :
                        MAIN_INVENTORY_TYPE;

                    Sprite targetItemIcon = _viewsManager.ItemViewManger.GetItemIcon(itemInInventory);
                    int indexItem = _uIManager.GetInventory(from).TryAddItem(targetItemIcon);

                    if (indexItem < 0)
                    {
                        return;
                    }

                    _itemsInInventories[from].Add(itemInInventory);
                    _indexOfItems[itemInInventory] = indexItem;

                    _itemsInInventories[to].Remove(itemInInventory);
                    _uIManager.GetInventory(to).RemoveItem(selectSlotIndex);
                }
            }
        }
    }

    private bool TryPickup()
    {
        string interactiveKey = _targetInteractiveElement.Name;

        if (_viewsManager.ItemViewManger.IsItem(interactiveKey))
        {
            Sprite targetItemIcon = _viewsManager.ItemViewManger.GetItemIcon(_targetInteractiveElement.Name);
            int indexItem = _uIManager.GetInventory(MAIN_INVENTORY_TYPE).TryAddItem(targetItemIcon);

            if (indexItem < 0)
            {
                return false;
            }

            _itemsInInventories[MAIN_INVENTORY_TYPE].Add(interactiveKey);
            _indexOfItems.Add(interactiveKey, indexItem);
            return true;
        }

        return false;
    }

    private void InteractInventory()
    {
        InventoryType targetInventoryType = _inventoryNames[_targetInteractiveElement.Name];

        if (_shownInventories.ContainsKey(targetInventoryType))
        {
            CloseInventory(targetInventoryType);
        }
        else
        {
            OpenInventory(targetInventoryType);
        }
    }

    private void ChangeInventory()
    {
        var shownInventories = _shownInventories.Where(i => i.Value).Select(i => i.Key);

        if (shownInventories.Count() > 1)
        {
            if (_targetShownInventoryType != MAIN_INVENTORY_TYPE)
            {
                if (shownInventories.Last().Equals(_targetShownInventoryType))
                {
                    _targetShownInventoryType = MAIN_INVENTORY_TYPE;
                }
                else
                {
                    _targetShownInventoryType = shownInventories.SkipWhile(t => t != _targetShownInventoryType).ElementAt(1);
                }
            }
            else
            {
                _targetShownInventoryType = shownInventories.First(t => t != MAIN_INVENTORY_TYPE);
            }

            _uIManager.SwitchToInventory(_targetShownInventoryType);
        }
    }

    private void OnNothingInteractive()
    {
        _hasInteractiveElement = false;
        _uIManager.HideInfo();

        foreach (InventoryType type in _shownInventories.Keys.ToArray())
        {
            if (type != MAIN_INVENTORY_TYPE && _shownInventories[type])
            {
                HideInventory(type);
            }
        }
    }

    private void InitInentories()
    {
        foreach (InventoryType type in Enum.GetValues(typeof(InventoryType)))
        {
            _inventoryNames.Add(type.ToString(), type);
            _itemsInInventories.Add(type, new List<string>());
            _uIManager.InitInventory(type, _viewsManager.GetInventoryCapacity(type));
        }

        _targetShownInventoryType = MAIN_INVENTORY_TYPE;
        _shownInventories.Add(MAIN_INVENTORY_TYPE, true);
    }

    private void OpenInventory(InventoryType type)
    {
        if (!_shownInventories.ContainsKey(type))
        {
            _shownInventories.Add(type, true);
            _targetShownInventoryType = type;
            _uIManager.ShowSelectInventory(type);
            _uIManager.ChangeVisibilityInventoryHelpInfo(_shownInventories.Values.Count(v => v) > 1);
        }
        else
        {
            Debug.LogError($"Attempt to open opened inventory. Inventory name: {type}");
        }
    }

    private void ShowInventory(InventoryType type)
    {
        if (_shownInventories.ContainsKey(type) && !_shownInventories[type])
        {
            _shownInventories[type] = true;
            _uIManager.ShowInventory(type);
            _uIManager.ChangeVisibilityInventoryHelpInfo(_shownInventories.Values.Count(v => v) > 1);
        }
    }

    private void CloseInventory(InventoryType type)
    {
        if (_shownInventories.ContainsKey(type))
        {
            HideInventory(type);
            _shownInventories.Remove(type);
        }
        else
        {
            Debug.LogError($"Attempt to close closed inventory. Inventory name: {type}");
        }
    }

    private void HideInventory(InventoryType type)
    {
        if (_shownInventories.ContainsKey(type) && _shownInventories[type])
        {
            _shownInventories[type] = false;
            _uIManager.HideInventory(type);
            _uIManager.ChangeVisibilityInventoryHelpInfo(_shownInventories.Values.Count(v => v) > 1);
            _targetShownInventoryType = MAIN_INVENTORY_TYPE;
        }
        else
        {
            Debug.LogError($"Attempt to hide closed inventory. Inventory name: {type}");
        }
    }
}
