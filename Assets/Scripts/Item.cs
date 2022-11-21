using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string _itemName;
    [TextArea]
    public string _itemDesc;
    public ItemType _ItemType;
    public Sprite _itemImage;
    public GameObject _itemPrefab;

    public string _weaponType;
    
    public enum ItemType
    {
        Equipment,
        Used,
        Ingrdient,
        ETC
    }
    
}
