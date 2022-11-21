using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string _itemName;
    [Tooltip(" HP, SP, DP, HUNGRY, THIRSTY, SATISFY만 가능합니다.")]
    public string[] _part;
    public int[] _num;
}

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] _itemEffects;

    [SerializeField]
    private StatusController _thePlayerStatus;
    
    [SerializeField]
    private WeaponManager _theWeaponManager;

    [SerializeField] private SlotToolTip _theSlotToolTip;
    
    private const string HP = "HP", SP = "SP", DP = "DP", HUNGRY = "HUNGRY", THIRSTY = "THIRSTY", SATISFY = "SATISFY";

    public void ShowToolTip(Item _item,Vector3 _pos)
    {
        _theSlotToolTip.ShowToolTip(_item,_pos);
    }

    public void HideToolTip()
    {
        _theSlotToolTip.HideToolTip();
    }

    public void UseItem(Item _item)
    {
        
        if (_item._ItemType == Item.ItemType.Equipment)
        {
            StartCoroutine(_theWeaponManager.ChangeWeaponCoroutine(_item._weaponType, _item._itemName));
        }
        else if (_item._ItemType == Item.ItemType.Used)
        {
            for (int i = 0; i < _itemEffects.Length; i++)
            {
                if (_itemEffects[i]._itemName == _item._itemName)
                {
                    for (int j = 0; j < _itemEffects[i]._part.Length; j++)
                    {
                        switch (_itemEffects[i]._part[j])
                        {
                            case HP:
                                _thePlayerStatus.IncreaseHP(_itemEffects[i]._num[j]);
                                break;
                            case SP:
                                _thePlayerStatus.IncreaseSP(_itemEffects[i]._num[j]);
                                break;
                            case DP:
                                _thePlayerStatus.IncreaseDP(_itemEffects[i]._num[j]);
                                break;
                            case HUNGRY:
                                _thePlayerStatus.IncreaseHungry(_itemEffects[i]._num[j]);
                                break;
                            case THIRSTY:
                                _thePlayerStatus.IncreaseThirsty(_itemEffects[i]._num[j]);
                                break;
                            case SATISFY:
                                break;
                            default:
                                Debug.Log("잘못된 Status 부위. HP, SP, DP, HUNGRY, THIRSTY, SATISFY만 가능합니다.");
                                break;
                        }
                        
                        Debug.Log(_item._itemName + "을 사용했습니다.");
                    }
                    return;
                }
            }
            Debug.Log("ItemEffectDatabase에 일치하는 itemName이 없습니다.");
        }
    }
}