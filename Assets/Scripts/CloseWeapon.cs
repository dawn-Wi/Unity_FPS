using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    public string _closeWeaponName;

    public bool _isHand;
    public bool _isAxe;
    public bool _isPickaxe;
    
    public float _range;
    public int _damage;
    public float _workSpeed;
    public float _attackDelay;
    public float _attackDelayA;
    public float _attackDelayB;
    
    public Animator anim;
}
