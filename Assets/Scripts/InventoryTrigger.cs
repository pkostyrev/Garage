using UnityEngine;

public class InventoryTrigger : OpenElement
{
    public new string Name => type.ToString();

    [SerializeField] private InventoryType type;
}
