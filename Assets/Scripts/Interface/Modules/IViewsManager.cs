public interface IViewsManager
{
    void Init(IConfigManager configManager);

    string GetInteractiveText(InteractiveType type);
    int GetInventoryCapacity(InventoryType type);

    IItemViewManger ItemViewManger { get; }
}
