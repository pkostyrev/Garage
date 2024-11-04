using System.Globalization;

public interface IUIManager
{
    void Init();

    void ShowInfo(string text);
    void HideInfo();

    Inventory GetInventory(InventoryType type);

    void InitInventory(InventoryType type, int capacity);
    void ShowInventory(InventoryType type);
    void ShowSelectInventory(InventoryType type);
    void HideInventory(InventoryType type);
    void SwitchToInventory(InventoryType type);
    void ChangeVisibilityInventoryHelpInfo(bool visibel);

    int GetSelectSlotIndex();
    void SelectItem(bool next);
}
