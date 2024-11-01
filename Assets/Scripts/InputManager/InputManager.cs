using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager
{
    public event Action<InteractiveType> OnInteractive;

    IUIManager _uiManager;
    IUserController _userController;

    Dictionary<KeyCode, InteractiveType> _interatives = new Dictionary<KeyCode, InteractiveType>();

    public void Init(IConfigManager configManager ,IUIManager uiManager, IUserController userController)
    {
        _uiManager = uiManager;
        _userController = userController;

        foreach (GameSettings.InteractiveButton interactiveButton in configManager.GameSettings.InteractiveButtons)
        {
            _interatives.Add(interactiveButton.key, interactiveButton.type);
        }
    }

    private void Update()
    {
        foreach ((KeyCode key, InteractiveType type) in _interatives)
        {
            if(Input.GetKeyDown(key))
            {
                OnInteractive?.Invoke(type);
            }
        }
    }
}
