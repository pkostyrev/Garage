using System;

public interface IUserController
{
    event Action<IInteractiveElement> OnSelectInteractive;
    event Action OnNothingInteractive;
}
