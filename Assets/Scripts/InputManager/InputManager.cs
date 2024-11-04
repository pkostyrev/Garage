using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager
{
    public event Action<InteractiveType> OnInteractive;

    Dictionary<KeyCode, InteractiveType> _interatives = new Dictionary<KeyCode, InteractiveType>();

    public void Init(IConfigManager configManager)
    {
        foreach (GameSettings.InteractiveButton interactiveButton in configManager.GameSettings.InteractiveButtons)
        {
            _interatives.Add(interactiveButton.key, interactiveButton.type);
        }
    }

    private void Update()
    {
        foreach ((KeyCode key, InteractiveType type) in _interatives)
        {
            if (Input.GetKeyDown(key))
            {
                OnInteractive?.Invoke(type);
            }
        }

        float mw = Input.GetAxis("Mouse ScrollWheel");

        if (mw > 0.1)
        {
            OnInteractive?.Invoke(InteractiveType.PreviousItem);
        }

        if (mw < -0.1)
        {
            OnInteractive?.Invoke(InteractiveType.NextItem);
        }
    }
}
