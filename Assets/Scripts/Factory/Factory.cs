using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Transform _instancesParent;
    [SerializeField] private GameObject _configManager;
    [SerializeField] private GameObject _eventSystemControl; 
    [SerializeField] private GameObject _viewsManager;
    [SerializeField] private GameObject _inputManager;
    [SerializeField] private GameObject _userController;
    [SerializeField] private GameObject _UIManager;
    [SerializeField] private GameObject _scenario;

    public IConfigManager CreateConfigManager() => Instantiate(_configManager, _instancesParent).GetComponent<IConfigManager>();
    public IEventSystemControl CreateEventSystemControl() => Instantiate(_eventSystemControl, _instancesParent).GetComponent<IEventSystemControl>();
    public IViewsManager CreateViewsManager() => Instantiate(_viewsManager, _instancesParent).GetComponent<IViewsManager>();
    public IInputManager CreateInputManager() => Instantiate(_inputManager, _instancesParent).GetComponent<IInputManager>();
    public IUserController CreateUserController() => Instantiate(_userController, _instancesParent).GetComponent<IUserController>();
    public IUIManager CreateUIManager() => Instantiate(_UIManager, _instancesParent).GetComponent<IUIManager>(); 
    public IScenario CreateScenario() => Instantiate(_scenario, _instancesParent).GetComponent<IScenario>();

}

