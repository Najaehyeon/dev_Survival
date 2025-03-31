using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "NPC/Item Data", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField] private string description;
    public string Description { get { return description; } }

    [SerializeField] private int price;
    public int Price { get { return price; } }

    [SerializeField] private int stress;
    public int Stress { get { return stress; } }
}
