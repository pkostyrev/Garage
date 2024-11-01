using UnityEngine;

public class Item : MonoBehaviour, IInteractiveElement
{
    public InteractiveType InteractiveType => _interactiveType;

    [SerializeField] private InteractiveType _interactiveType;
    [SerializeField] private Collider _collider;

    public void Interact()
    {
        gameObject.SetActive(false);
        _collider.enabled = false;
    }
}
