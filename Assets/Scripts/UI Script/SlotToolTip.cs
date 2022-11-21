using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField] private GameObject _go_Base;

    [SerializeField] private Text _text_ItemName;
    [SerializeField] private Text _text_ItemDesc;
    [SerializeField] private Text _text_ItemHowToUsed;

    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        _go_Base.SetActive(true);
        _pos += new Vector3(_go_Base.GetComponent<RectTransform>().rect.width * 0.8f,-_go_Base.GetComponent<RectTransform>().rect.height * 0.7f, 0f);
        _go_Base.transform.position = _pos;

        _text_ItemName.text = _item._itemName;
        _text_ItemDesc.text = _item._itemDesc;

        if (_item._ItemType == Item.ItemType.Equipment)
        {
            _text_ItemHowToUsed.text = "우클릭 - 장착";
        }
        else if (_item._ItemType == Item.ItemType.Used)
        {
            _text_ItemHowToUsed.text = "우클릭 - 먹기";
        }
        else
        {
            _text_ItemHowToUsed.text = "";
        }
    }

    public void HideToolTip()
    {
        _go_Base.SetActive(false);
    }
}
