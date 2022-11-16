using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public string _gunName;
    public float _range;
    public float _accuracy;
    public float _fireRate;
    public float _reloadTime;

    public int _damage;

    public int _reloadBulletCount;
    public int _currentBulletCount;
    public int _maxBulletCount;
    public int _carryBulletCount;

    public float _retroActionForce;
    public float _retroActionFineSightForce;

    public Vector3 _fineSightOriginPos;
    public Animator anim;
    public ParticleSystem _muzzleFlash;

    public AudioClip _fire_Sound;
    
}
