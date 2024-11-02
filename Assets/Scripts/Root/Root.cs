using System.Collections;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Factory factory;

    private static Root instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private IEnumerator Start()
    {
        var foo1 = Root.EventSystemControl;

        yield return null;

        var foo2 = Root.UIManager;

        yield return null;

        var foo3 = Root.UserController;

        yield return null;

        var foo4 = Root.InputManager;

        yield return null;

        var foo5 = Root.Scenario;

        foo4.OnInteractive += (type) =>
        {
            if (type == InteractiveType.Exit)
                ExitGame();
        };
    }

    internal static void Die()
    {
        if (instance != null)
        {
            DestroyImmediate(instance.gameObject);
        }

        instance = null;
    }

    public static void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    private IEventSystemControl eventSystemControl;
    public static IEventSystemControl EventSystemControl
    {
        get
        {
            if (instance.eventSystemControl == null)
            {
                instance.eventSystemControl = instance.factory.CreateEventSystemControl();
            }
            return instance.eventSystemControl;
        }
    }

    private IConfigManager configManager;
    public static IConfigManager ConfigManager
    {
        get
        {
            if (instance.configManager == null)
            {
                instance.configManager = instance.factory.CreateConfigManager();
            }
            return instance.configManager;
        }
    }

    private IUserController userController;
    public static IUserController UserController
    {
        get
        {
            if (instance.userController == null)
            {
                instance.userController = instance.factory.CreateUserController();
            }
            return instance.userController;
        }
    }

    private IViewsManager viewsManager;
    public static IViewsManager ViewsManager
    {
        get
        {
            if (instance.viewsManager == null)
            {
                instance.viewsManager = instance.factory.CreateViewsManager();
                instance.viewsManager.Init(ConfigManager);
            }
            return instance.viewsManager;
        }
    }

    private IInputManager inputManager;
    public static IInputManager InputManager
    {
        get
        {
            if (instance.inputManager == null)
            {
                instance.inputManager = instance.factory.CreateInputManager();
                instance.inputManager.Init(ConfigManager);
            }
            return instance.inputManager;
        }
    }

    private IUIManager uiManager;
    public static IUIManager UIManager
    {
        get
        {
            if (instance.uiManager == null)
            {
                instance.uiManager = instance.factory.CreateUIManager();
                instance.uiManager.Init(ViewsManager);
            }
            return instance.uiManager;
        }
    }

    private IScenario scenario;
    public static IScenario Scenario
    {
        get
        {
            if (instance.scenario == null)
            {
                instance.scenario = instance.factory.CreateScenario();
                instance.scenario.Init(ViewsManager, UserController, InputManager, UIManager);
            }
            return instance.scenario;
        }
    }
}
