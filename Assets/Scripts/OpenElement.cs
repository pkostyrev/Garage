using System.Linq;
using UnityEngine;

public class OpenElement : MonoBehaviour, IInteractiveElement
{
    private const string IS_CHANGE = "Change";
    private const string DIRECTION = "Direction";

    public InteractiveType InteractiveType => _interactiveType;

    [SerializeField] private Animator _animator;
    [SerializeField] private InteractiveType _interactiveType;
    [SerializeField] private DirectionType _direction;

    void Start()
    {
        if (_animator.parameters.Any(p => p.name == DIRECTION))
        {
            _animator.SetInteger(DIRECTION, (int)_direction);
        }
    }

    public void Interact()
    {
        _animator.SetTrigger(IS_CHANGE);
    }
}
