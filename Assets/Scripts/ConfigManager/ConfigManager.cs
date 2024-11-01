using UnityEngine;

public class ConfigManager : MonoBehaviour, IConfigManager
{
    public GameSettings GameSettings => _gameSettings;


    [SerializeField] private GameSettings _gameSettings;
}
