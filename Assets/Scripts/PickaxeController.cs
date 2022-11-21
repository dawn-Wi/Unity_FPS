using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeController : CloseWeaponController
{
    public static bool _isActivate = true;

    protected void Start()
    {
        WeaponManager._currentWeapon = _currentCloseWeapon.GetComponent<Transform>();
        WeaponManager._currentWeaponAnim = _currentCloseWeapon.anim;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isActivate)
        {
            TryAttack();
        }
    }

    protected override IEnumerator HitCoroutine()
    {
        while (_isSwing)
        {
            if (CheckObject())
            {
                if (hitInfo.transform.tag == "Rock")
                {
                    hitInfo.transform.GetComponent<Rock>().Mining();
                }
                else if (hitInfo.transform.tag == "WeakAnimal")
                {
                    SoundManager._instansce.PlaySE("Animal_Hit");
                    hitInfo.transform.GetComponent<WeakAnimal>().Damage(1, transform.position);
                }
                _isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        _isActivate = true;
    }
}