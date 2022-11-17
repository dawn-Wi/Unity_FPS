using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : CloseWeaponController
{
    public static bool _isActivate = false;

    protected void Start()
    {
        
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