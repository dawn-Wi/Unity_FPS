using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseWeaponController : MonoBehaviour
{
    

    [SerializeField] 
    protected CloseWeapon _currentCloseWeapon;

    protected bool _isAttack = false;
    protected bool _isSwing = false;

    protected RaycastHit hitInfo;
    
    protected void TryAttack()
    {
        if (!Inventory._inventoryActivated)
        {
            if (Input.GetButton("Fire1"))
            {
                if (!_isAttack)
                {
                    StartCoroutine(AttackCoroutine());
                }
            }
        }
    }

    protected IEnumerator AttackCoroutine()
    {
        _isAttack = true;
        _currentCloseWeapon.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(_currentCloseWeapon._attackDelayA);
        _isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(_currentCloseWeapon._attackDelayB);
        _isSwing = false;

        yield return new WaitForSeconds(_currentCloseWeapon._attackDelay - _currentCloseWeapon._attackDelayA -
                                        _currentCloseWeapon._attackDelayB);
        _isAttack = false;
    }

    protected abstract IEnumerator HitCoroutine();

    protected bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, _currentCloseWeapon._range))
        {
            return true;
        }

        return false;
    }
    
    public virtual void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        if (WeaponManager._currentWeapon != null)
        {
            WeaponManager._currentWeapon.gameObject.SetActive(false);
        }

        _currentCloseWeapon = _closeWeapon;
        WeaponManager._currentWeapon = _currentCloseWeapon.GetComponent<Transform>();
        WeaponManager._currentWeaponAnim = _currentCloseWeapon.anim;

        _currentCloseWeapon.transform.localPosition = Vector3.zero;
        _currentCloseWeapon.gameObject.SetActive(true);
        
    }
}