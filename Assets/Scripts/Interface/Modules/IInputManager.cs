using System;

public interface IInputManager
{
    event Action<InteractiveType> OnInteractive;
    void Init(IConfigManager configManager, IUIManager uiManager, IUserController userController);
}
