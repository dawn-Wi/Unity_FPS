using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    private Vector3 _originPos;

    private Vector3 _currentPos;

    [SerializeField] 
    private Vector3 _limitPos;

    [SerializeField]
    private Vector3 _fineSigntLimitPos;

    [SerializeField] 
    private Vector3 _smoothSway;

    [SerializeField] 
    private GunController _theGunController;

    // Start is called before the first frame update
    private void Start()
    {
        _originPos = this.transform.localPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Inventory._inventoryActivated)
        {
            TrySway();
        }
    }

    private void TrySway()
    {
        if (Input.GetAxisRaw("Mouse X") != 0|| Input.GetAxisRaw("Mouse Y")!= 0)
        {
            Swaying();
        }
        else
        {
            BackToOriginPos();
        }
    }

    private void Swaying()
    {
        float _moveX = Input.GetAxisRaw("Mouse X");
        float _moveY = Input.GetAxisRaw("Mouse Y");

        if (_theGunController._isFineSightMode)
        {
            _currentPos.Set(Mathf.Clamp(Mathf.Lerp(_currentPos.x, -_moveX, _smoothSway.x), -_limitPos.x, _limitPos.x), 
                Mathf.Clamp(Mathf.Lerp(_currentPos.y, -_moveX, _smoothSway.x), -_limitPos.y, _limitPos.y),
                _originPos.z);
        }
        else
        {
            _currentPos.Set(Mathf.Clamp(Mathf.Lerp(_currentPos.x, -_moveX, _smoothSway.y), -_fineSigntLimitPos.x, _fineSigntLimitPos.x), 
                Mathf.Clamp(Mathf.Lerp(_currentPos.y, -_moveX, _smoothSway.y), -_fineSigntLimitPos.y, _fineSigntLimitPos.y),
                _originPos.z);
        }
        transform.localPosition = _currentPos;
    }

    private void BackToOriginPos()
    {
        _currentPos = Vector3.Lerp(_currentPos, _originPos, _smoothSway.x);
        transform.localPosition = _currentPos;
    }
}