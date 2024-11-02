using System;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour, IScenario
{
    private IViewsManager _viewsManager;
    private IUserController _userController;
    private IInputManager _inputManager;
    private IUIManager _uIManager;

    private bool _hasInteractiveElement;
    private IInteractiveElement _targetInteractiveElement;

    Dictionary<string, InventoryType> _inventoryNames = new Dictionary<string, InventoryType>();

    private void Start()
    {
        foreach (InventoryType type in Enum.GetValues(typeof(InventoryType)))
        {
            _inventoryNames.Add(type.ToString(), type);
        }
    }

    public void Init(IViewsManager viewsManager, IUserController userController, IInputManager inputManager, IUIManager uIManager)
    {
        _viewsManager = viewsManager;
        _userController = userController;
        _inputManager = inputManager;
        _uIManager = uIManager;

        _inputManager.OnInteractive += OnInteractiveInput;
        _userController.OnSelectInteractive += OnSelectInteractive;
        _userController.OnNothingInteractive += OnNothingInteractive;
    }

    private void OnSelectInteractive(IInteractiveElement element)
    {
        if (_hasInteractiveElement 
            && _inventoryNames.ContainsKey(_targetInteractiveElement.Name) 
            && _uIManager.IsOpenedInventory(_inventoryNames[_targetInteractiveElement.Name]))
        {
            _uIManager.HideInventory(_inventoryNames[_targetInteractiveElement.Name]);
        }

        if (_inventoryNames.ContainsKey(element.Name) && _uIManager.IsOpenedInventory(_inventoryNames[element.Name]))
        {
            _uIManager.ShowInventory(_inventoryNames[element.Name]);
        }

        _uIManager.ShowInfo(element.InteractiveType);
        _targetInteractiveElement = element;
        _hasInteractiveElement = true;
    }

    private void OnInteractiveInput(InteractiveType type)
    {
        Debug.Log("OnInteractiveInput");
        if (_hasInteractiveElement && _targetInteractiveElement.InteractiveType == type)
        {
            _targetInteractiveElement.Interact();

            if (_inventoryNames.ContainsKey(_targetInteractiveElement.Name))
            {
                _uIManager.ChangeOpennessInventory(_inventoryNames[_targetInteractiveElement.Name]);
            }
        }
    }

    private void OnNothingInteractive()
    {
        _hasInteractiveElement = false;
        _uIManager.HideInfo();
        _uIManager.HideOtherInventory();
    }
}
