using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool _inventoryActivated = false;

    [SerializeField] private GameObject _go_InventoryBase;

    [SerializeField] private GameObject _go_SlotsParent;

    private Slot[] _slots;

    // Start is called before the first frame update
    private void Start()
    {
        _slots = _go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    private void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _inventoryActivated = !_inventoryActivated;
            if (_inventoryActivated)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    private void OpenInventory()
    {
        _go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        _go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count=1)
    {
        if (Item.ItemType.Equipment != _item._ItemType)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i]._item != null)
                {
                    if (_slots[i]._item._itemName.Equals(_item._itemName))
                    {
                        _slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i]._item == null)
            {
                _slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}