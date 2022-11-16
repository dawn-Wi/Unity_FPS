 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] 
    private Gun _currentGun;

    private float _currentFireRate;

    private bool _isReload = false;

    private bool _isFineSightMode = false;

    private Vector3 _originPos;
    
    
    private AudioSource _audioSource;

    private RaycastHit _hitInfo;

    [SerializeField]
    private Camera _theCam;

    private void Start()
    {
        _originPos = Vector3.zero;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        GunFireRateCalc();
        TryFire();
        TryReload();
        TryFineSight();
    }

    private void GunFireRateCalc()
    {
        if (_currentFireRate > 0)
            _currentFireRate -= Time.deltaTime;
    }

    private void TryFire() 
    {
        if (Input.GetButton("Fire1") && _currentFireRate<=0 && !_isReload)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (!_isReload)
        {
            if (_currentGun._currentBulletCount > 0){
                Shoot(); 
            }
            else
            {
                CancelFineSight();
                StartCoroutine(ReloadCoroutine());
            }
        }
    }

    private void Shoot()
    {
        _currentGun._currentBulletCount--;
        _currentFireRate = _currentGun._fireRate; 
        PlaySE(_currentGun._fire_Sound);
        _currentGun._muzzleFlash.Play();
        Hit();
        StopAllCoroutines();
        StartCoroutine(RetroActionCoroutine());
    }

    private void Hit()
    {
        if (Physics.Raycast(_theCam.transform.position, _theCam.transform.forward, out _hitInfo, _currentGun._range))
        {
            Debug.Log(_hitInfo.transform.name);
        }
    }

    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !_isReload && _currentGun._currentBulletCount < _currentGun._reloadBulletCount)
        {
            CancelFineSight();
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine() 
    {
        if (_currentGun._carryBulletCount > 0)
        {
            _isReload = true;
            _currentGun.anim.SetTrigger("Reload");

            _currentGun._carryBulletCount += _currentGun._currentBulletCount;
            _currentGun._currentBulletCount = 0;
            
            yield return new WaitForSeconds(_currentGun._reloadTime);

            if (_currentGun._carryBulletCount >= _currentGun._reloadBulletCount)
            {
                _currentGun._currentBulletCount = _currentGun._reloadBulletCount;
                _currentGun._carryBulletCount -= _currentGun._reloadBulletCount;
            }
            else
            {
                _currentGun._currentBulletCount = _currentGun._carryBulletCount;
                _currentGun._carryBulletCount = 0; 
            }

            _isReload = false;
        }
        else{
            Debug.Log("소유한 총알이 없습니다.");
        }

    }

    private void TryFineSight()
    {
        if (Input.GetButtonDown("Fire2")&&!_isReload)
        {
            FineSight();
        }
    }

    public void CancelFineSight()
    {
        if(_isFineSightMode)
            FineSight();
    }
    private void FineSight()
    {
        _isFineSightMode = !_isFineSightMode;
        _currentGun.anim.SetBool("FineSightMode", _isFineSightMode);

        if (_isFineSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(FineSightActivateCoroutine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FineSightDeactivateCoroutine());
        }
    }

    IEnumerator FineSightActivateCoroutine()
    {
        while (_currentGun.transform.localPosition != _currentGun._fineSightOriginPos)
        {
            _currentGun.transform.localPosition = Vector3.Lerp(_currentGun.transform.localPosition,
                _currentGun._fineSightOriginPos, 0.2f);
            yield return null;
        }
    }
    
    IEnumerator FineSightDeactivateCoroutine()
    {
        while (_currentGun.transform.localPosition != _originPos)
        {
            _currentGun.transform.localPosition = Vector3.Lerp(_currentGun.transform.localPosition,
                _originPos, 0.2f);
            yield return null;
        }
    }

    IEnumerator RetroActionCoroutine()
    {
        Vector3 recoilBack = new Vector3(_currentGun._retroActionForce, _originPos.y, _originPos.z);
        Vector3 retroActionRecoilBack = new Vector3(_currentGun._retroActionFineSightForce, _currentGun._fineSightOriginPos.y, _currentGun._fineSightOriginPos.z);

        if (!_isFineSightMode)
        {
            _currentGun.transform.localPosition = _originPos;
            
            while (_currentGun.transform.localPosition.x <= _currentGun._retroActionForce -0.02f)
            {
                _currentGun.transform.localPosition =
                    Vector3.Lerp(_currentGun.transform.localPosition, recoilBack, 0.4f);
                yield return null;
            }

            while (_currentGun.transform.localPosition != _originPos)
            {
                _currentGun.transform.localPosition =
                    Vector3.Lerp(_currentGun.transform.localPosition, _originPos, 0.1f);
                yield return null;
            }
            
            
        }
        else
        {
            _currentGun.transform.localPosition = _currentGun._fineSightOriginPos;
            
            while (_currentGun.transform.localPosition.x <= _currentGun._retroActionFineSightForce -0.02f)
            {
                _currentGun.transform.localPosition =
                    Vector3.Lerp(_currentGun.transform.localPosition, retroActionRecoilBack, 0.4f);
                yield return null;
            }

            while (_currentGun.transform.localPosition != _currentGun._fineSightOriginPos)
            {
                _currentGun.transform.localPosition =
                    Vector3.Lerp(_currentGun.transform.localPosition, _currentGun._fineSightOriginPos, 0.1f);
                yield return null;
            }
        }
    }
    
    private void PlaySE(AudioClip _clip)
    {
        _audioSource.clip = _clip;
        _audioSource.Play();
    }
}
