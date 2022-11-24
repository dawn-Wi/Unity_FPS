using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool _canPlayerMove = true;

    public static bool _isOpenInventory = false;
    public static bool _isOpenCraftManual = false;

    public static bool _isNight = false;
    public static bool _isWater = false;

    private WeaponManager _theWM;
    private bool _flag = false;
   
    // Update is called once per frame
    private void Update()
    {
        if (_isOpenInventory|| _isOpenCraftManual)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _canPlayerMove = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _canPlayerMove = true;
        }

        if (_isWater)
        {
            if (!_flag)
            {
                StopAllCoroutines();
                StartCoroutine(_theWM.WeaponInCoroutine());
                _flag = true;
            }
        }
        else
        {
            if (_flag)
            {
                _theWM.WeaponOut();
                _flag = false;
            }
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _theWM = FindObjectOfType<WeaponManager>();
    }
}
