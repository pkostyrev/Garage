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
                instance.inputManager.Init(ConfigManager, UIManager, UserController);
            }
            return instance.inputManager;
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
                instance.userController.Init(InputManager);
            }
            return instance.userController;
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
                instance.uiManager.Init(ViewsManager, UserController);
            }
            return instance.uiManager;
        }
    }
}
