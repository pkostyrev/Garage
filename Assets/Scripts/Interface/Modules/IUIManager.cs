using System.Globalization;

public interface IUIManager
{
    void Init(IViewsManager viewsManager);

    void ShowInfo(string text);
    void HideInfo();

    Inventory GetInventory(InventoryType type);

    void InitInventory(InventoryType type, int capacity);
    void ShowInventory(InventoryType type);
    void ShowSelectInventory(InventoryType type);
    void HideInventory(InventoryType type);
    void SwitchToInventory(InventoryType type);
    void ChangeVisibilityInventoryHelpInfo(bool visibel);

}
