using System.Globalization;

public interface IUIManager
{
    void Init(IViewsManager viewsManager, IUserController UserController);

    void ChangeInventory();

    void OpenInventory(InventoryType inventoryType);
    void CloseInventory(InventoryType inventoryType);
}
