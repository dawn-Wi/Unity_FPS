using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static bool _isChangeWeapon = false;

    public static Transform _currentWeapon;

    public static Animator _currentWeaponAnim;

    [SerializeField] 
    private string _currentWeaponType;


    [SerializeField]
    private float changeWeaponDelayTime;

    [SerializeField]
    private float _changeWeaponEndDelayTime;

    [SerializeField] 
    private Gun[] _guns;

    [SerializeField] 
    private Hand[] _hands;

    private Dictionary<String, Gun> _gunDictionary = new Dictionary<string, Gun>();
    private Dictionary<String, Hand> _handDictionary = new Dictionary<string, Hand>();

    [SerializeField]
    private GunController _theGunController;
    
    [SerializeField]
    private HandController _theHandController;

    
    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            _gunDictionary.Add(_guns[i]._gunName, _guns[i]);
        }

        for (int i = 0; i < _hands.Length; i++)
        {
            _handDictionary.Add(_hands[i]._handName, _hands[i]);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_isChangeWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartCoroutine(ChangeWeaponCoroutine("HAND", "맨손"));
            }else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StartCoroutine(ChangeWeaponCoroutine("GUN", "SubMachineGun1"));
            }
        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        _isChangeWeapon = true;
        _currentWeaponAnim.SetTrigger("Weapon_Out");

        yield return new WaitForSeconds(changeWeaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type, _name);

        yield return new WaitForSeconds(_changeWeaponEndDelayTime);

        _currentWeaponType = _type;
        _isChangeWeapon = false;
    }

    private void CancelPreWeaponAction()
    {
        switch (_currentWeaponType)
        {
            case "GUN":
                _theGunController.CancelFineSight();
                _theGunController.CancelReload();
                GunController._isActivate = false;
                break;
            case "HAND":
                HandController._isActivate = false;
                break;
        }
    }

    private void WeaponChange(string _type, string _name)
    {
        if (_type == "GUN")
        {
            _theGunController.GunChange(_gunDictionary[_name]);
        }
        else if (_type == "HAND")
        {
            _theHandController.HandChange(_handDictionary[_name]);
        }
    }
}
