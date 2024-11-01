using System;

public interface IUserController
{
    event Action<InteractiveType> OnInteractiveTypeChanged;
    event Action OnNothingInteractive;

    void Init(IInputManager inputManager);
}
