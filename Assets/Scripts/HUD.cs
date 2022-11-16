using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] 
    private GunController _theGunController;

    private Gun _currentGun;

    [SerializeField] 
    private GameObject _go_BulletHUD;

    [SerializeField]
    private Text[] _text_Bullet;

    // Update is called once per frame
    private void Update() 
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        _currentGun = _theGunController.GetGun();
        _text_Bullet[0].text = _currentGun._carryBulletCount.ToString();
        _text_Bullet[1].text = _currentGun._reloadBulletCount.ToString();
        _text_Bullet[2].text = _currentGun._currentBulletCount.ToString();
    }
}
