using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    Item _item;
    public bool IsEmpty { get; private set; }
    public void Fill(Item item)
    {
        _item = item;
        IsEmpty = false;
    }
    public Item Empty()
    {
        Item item = _item;
        _item = null;
        IsEmpty = true;
        return _item;
    }
    public GameObject GetObject() => _item?.Model;
    public EItemName GetItemName() => _item.Name;
    public ECookingState GetCookingState() => _item.CookingState;
}