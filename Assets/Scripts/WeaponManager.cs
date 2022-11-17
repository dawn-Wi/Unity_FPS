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
    private CloseWeapon[] _hands;

    [SerializeField] 
    protected CloseWeapon[] _axes;
    
    [SerializeField] 
    protected CloseWeapon[] _pickaxes;

    private Dictionary<string, Gun> _gunDictionary = new Dictionary<string, Gun>();
    private Dictionary<string, CloseWeapon> _handDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> _axeDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> _pickaxeDictionary = new Dictionary<string, CloseWeapon>();

    [SerializeField]
    private GunController _theGunController;
    
    [SerializeField]
    private HandController _theHandController;
    
    [SerializeField]
    private AxeController _theAxeController;

    [SerializeField]
    private PickaxeController _thePickaxeController;

    
    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            _gunDictionary.Add(_guns[i]._gunName, _guns[i]);
        }

        for (int i = 0; i < _hands.Length; i++)
        {
            _handDictionary.Add(_hands[i]._closeWeaponName, _hands[i]);
        }

        for (int i = 0; i < _axes.Length; i++)
        {
            _axeDictionary.Add(_axes[i]._closeWeaponName, _axes[i]);
        }

        for (int i = 0; i < _pickaxes.Length; i++)
        {
            _pickaxeDictionary.Add(_pickaxes[i]._closeWeaponName, _pickaxes[i]);
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
            }else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartCoroutine(ChangeWeaponCoroutine("AXE", "Axe"));
            }else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                StartCoroutine(ChangeWeaponCoroutine("PICKAXE", "Pickaxe"));
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
            case "AXE":
                AxeController._isActivate = false;
                break;
            case "PICKAXE":
                PickaxeController._isActivate = false;
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
            _theHandController.CloseWeaponChange(_handDictionary[_name]);
        }
        else if (_type == "AXE")
        {
            _theAxeController.CloseWeaponChange(_axeDictionary[_name]);
        }
        else if (_type == "PICKAXE")
        {
            _thePickaxeController.CloseWeaponChange(_pickaxeDictionary[_name]);
        }
    }
}
