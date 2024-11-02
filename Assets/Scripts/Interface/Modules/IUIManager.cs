using System.Globalization;

public interface IUIManager
{
    void Init(IViewsManager viewsManager);

    void ChangeInventory();

    void ShowInfo(InteractiveType type);
    void HideInfo();

    bool IsOpenedInventory(InventoryType inventoryType);
    void OpenInventory(InventoryType inventoryType);
    void CloseInventory(InventoryType inventoryType);
    void ChangeOpennessInventory(InventoryType inventoryType);
    void ShowInventory(InventoryType inventoryType);
    void HideInventory(InventoryType inventoryType);
    void HideOtherInventory();

}
