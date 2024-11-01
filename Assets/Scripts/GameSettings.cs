using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Configs" + "/" + nameof(GameSettings))]
public class GameSettings : ScriptableObject
{
    [Serializable]
    public class InteractiveText
    {
        public InteractiveType type;
        public string text;
    }


    [Serializable]
    public class InteractiveButton
    {
        public InteractiveType type;
        public KeyCode key;
    }

    [Serializable]
    public class InventoryConfig
    {
        public InventoryType type;
        public int capacity;
    }

    public ItemConfig[] ItemConfigs => itemConfigs;
    public InventoryConfig[] Inventories => inventories;
    public InteractiveText[] InteractiveTexts => interactiveTexts;
    public InteractiveButton[] InteractiveButtons => interactiveButton;

    [SerializeField] private ItemConfig[] itemConfigs;
    [SerializeField] private InventoryConfig[] inventories;
    [SerializeField] private InteractiveText[] interactiveTexts;
    [SerializeField] private InteractiveButton[] interactiveButton;
}
