using System;
using UnityEngine;

public class UserController : MonoBehaviour, IUserController
{
    public event Action<IInteractiveElement> OnSelectInteractive;
    public event Action OnNothingInteractive;

    [SerializeField] private Transform _cameraTransform;

    Collider _targetColider;

    private void Update()
    {
        if (Physics.Raycast(transform.position, _cameraTransform.transform.forward, out RaycastHit hit, 3f))
        {
            if (_targetColider != null && _targetColider.Equals(hit.collider))
            {
                return;
            }
            else if (hit.collider.TryGetComponent(out IInteractiveElement element))
            {
                _targetColider = hit.collider;
                OnSelectInteractive?.Invoke(element);
            }
            else if (_targetColider != null)
            {
                _targetColider = null;
                OnNothingInteractive?.Invoke();
            }
        }
        else if (_targetColider != null)
        {
            _targetColider = null;
            OnNothingInteractive?.Invoke();
        }
    }
}
