using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    
    public Item _item;

    public int _itemCount;

    public Image _itemImage;


    [SerializeField] private Text _text_Count;
    [SerializeField] private GameObject _go_CountImage;
    private WeaponManager _theWeaponManager;

    private void Start()
    {
        _theWeaponManager = FindObjectOfType<WeaponManager>();
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (_item != null)
            {
                if (_item._ItemType == Item.ItemType.Equipment)
                {
                    StartCoroutine(_theWeaponManager.ChangeWeaponCoroutine(_item._weaponType, _item._itemName));
                }
                else
                {
                    Debug.Log(_item._itemName+"을 사용했습니다.");
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_item != null)
        {
            DragSlot._instance._dragSlot = this;
            DragSlot._instance.DragSetImage(_itemImage);
            DragSlot._instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_item != null)
        {
            DragSlot._instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot._instance.SetColor(0);
        DragSlot._instance._dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot._instance._dragSlot != null)
        {
            ChangeSlot();
        }
    }

    private void ChangeSlot()
    {
        Item _tempItem = _item;
        int _tempItemCount = _itemCount;
        
        AddItem(DragSlot._instance._dragSlot._item, DragSlot._instance._dragSlot._itemCount);

        if (_tempItem != null)
        {
            DragSlot._instance._dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot._instance._dragSlot.ClearSlot();
        }
    }
}
