public interface IInteractiveElement
{
    string Name { get; }
    InteractiveType InteractiveType { get; }
    void Interact();
}
