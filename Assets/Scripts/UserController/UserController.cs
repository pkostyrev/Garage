using System;
using UnityEngine;

public class UserController : MonoBehaviour, IUserController
{
    public event Action<InteractiveType> OnInteractiveTypeChanged;
    public event Action OnNothingInteractive;

    [SerializeField] private Transform _cameraTransform;

    RaycastHit _targetHit;
    IInteractiveElement _interactiveElement;

    public void Init(IInputManager inputManager)
    {
        inputManager.OnInteractive += OnInteractive;
    }

    void Update()
    {
        if(Physics.Raycast(transform.position, _cameraTransform.transform.forward,out RaycastHit hit, 3f))
        {
            if (_targetHit.collider != null && _targetHit.collider.Equals(hit))
            {
                return;
            }
            else if (hit.collider.TryGetComponent(out IInteractiveElement element))
            {
                _targetHit = hit;
                _interactiveElement = element;
                OnInteractiveTypeChanged?.Invoke(element.InteractiveType);
            }
            else if (_interactiveElement != null)
            {
                _targetHit = new RaycastHit();
                _interactiveElement = null;
                OnNothingInteractive?.Invoke();
            }
        }
        else if(_interactiveElement != null)
        {
            _targetHit = new RaycastHit();
            _interactiveElement = null;
            OnNothingInteractive?.Invoke();
        }
    }

    private void OnInteractive(InteractiveType type)
    {
        if (_interactiveElement != null && _interactiveElement.InteractiveType == type)
        {
            _interactiveElement.Interact();
        }
    }
}
