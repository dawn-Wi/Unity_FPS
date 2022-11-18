using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item _item;

    public int _itemCount;

    public Image _itemImage;


    [SerializeField] private Text _text_Count;
    [SerializeField] private GameObject _go_CountImage;

    private void SetColor(float _alpha)
    {
        Color _color = _itemImage.color;
        _color.a = _alpha;
        _itemImage.color = _color;
    }
    
    public void AddItem(Item item, int _count=1)
    {
        _item = item;
        _itemCount = _count;
        _itemImage.sprite = item._itemImage;

        if (_item._ItemType != Item.ItemType.Equipment)
        {
            _go_CountImage.SetActive(true);
            _text_Count.text = _itemCount.ToString();
        }
        else
        {
            _text_Count.text = "0";
            _go_CountImage.SetActive(false);
        }
        
        SetColor(1);
    }

    public void SetSlotCount(int _count)
    {
        _itemCount += _count;
        _text_Count.text = _itemCount.ToString();

        if (_itemCount <=0)
        {
            ClearSlot();
        }
    }

    private void ClearSlot()
    {
        _item = null;
        _itemCount = 0;
        _itemImage.sprite = null;
        SetColor(0);
        
        _text_Count.text = "0";
        _go_CountImage.SetActive(false);
    }
}
