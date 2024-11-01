using UnityEngine;

[CreateAssetMenu(fileName = nameof(ItemConfig), menuName = "Configs" + "/" + nameof(ItemConfig))]
public class ItemConfig : ScriptableObject
{
    public GameObject ItemPrefab => _itemPrefab;
    public Sprite Icon => _icon;

    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Sprite _icon;
}
