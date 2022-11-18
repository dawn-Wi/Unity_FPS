using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float _range; //습득 가능한 최대거리

    private bool _pickupActivated = false;


    private RaycastHit _hitInfo;

    [SerializeField] private LayerMask _layerMask; //아이템 레이어에만 반응하도록 

    [SerializeField] private Text _actionText;
    [SerializeField] private Inventory _theInventory;

    // Update is called once per frame
    private void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if (_pickupActivated)
        {
            if (_hitInfo.transform != null)
            {
                Debug.Log(_hitInfo.transform.GetComponent<ItemPickup>()._Item._itemName + "획득했습니다.");
                _theInventory.AcquireItem(_hitInfo.transform.GetComponent<ItemPickup>()._Item);
                Destroy(_hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }    
    }
    
    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hitInfo, _range, _layerMask))
        {
            if (_hitInfo.transform.CompareTag("Item"))
            {
                ItemInfoAppear();
            }
            else
            {
                InfoDisappear();
            }
        }
    }

    private void ItemInfoAppear()
    {
        _pickupActivated = true;
        _actionText.gameObject.SetActive(true);
        _actionText.text = _hitInfo.transform.GetComponent<ItemPickup>()._Item._itemName + " 획득 " + "<color=yellow>" + "(E)" + "</color>";
    }

    private void InfoDisappear()
    {
        _pickupActivated = false;
        _actionText.gameObject.SetActive(false);
    }
}