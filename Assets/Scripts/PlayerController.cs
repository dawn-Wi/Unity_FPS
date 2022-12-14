using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float _walkSpeed;
    [SerializeField]
    private float _runSpeed;
    [SerializeField] 
    private float _crouchSpeed;
    [SerializeField] 
    private float _swimSpeed;
    [SerializeField] 
    private float _swimFastSpeed;
    [SerializeField] 
    private float _upSwimSpeed;
    
    
    
    private float _applySpeed;

    [SerializeField] 
    private float _jumpForce;

    private bool _isWalk = false;
    private bool _isRun = false;
    private bool _isCrouch = false;
    private bool _isGround = true;

    private Vector3 _lastPos;

    [SerializeField]
    private float _crouchPosY;
    private float _originPosY;
    private float _applyCrouchPosY;
    
    private CapsuleCollider _capsuleCollider;
    

    [SerializeField] 
    private float _lookSensitivity;

    [SerializeField] 
    private float _cameraRotationLimit;
    private float _currentCameraRotationX = 0;

    [SerializeField] 
    private Camera _theCamera;
    private Rigidbody _myRigid;

    private GunController _theGunController;

    private Crosshair _theCrosshair;
    private StatusController _theStatusController;
    
    // Start is called before the first frame update
    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _myRigid = GetComponent<Rigidbody>();
        _theGunController = FindObjectOfType<GunController>();
        _theCrosshair = FindObjectOfType<Crosshair>();
        _theStatusController = FindObjectOfType<StatusController>();
        
        _applySpeed = _walkSpeed;
        _originPosY = -_theCamera.transform.localPosition.y;
        _applyCrouchPosY = _originPosY;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager._canPlayerMove)
        {
            WaterCheck();
            IsGround();
            TryJump();
            if (!GameManager._isWater)
            {
                TryRun();
            }
            TryCrouch();
            Move();
            MoveCheck();
            if (!Inventory._inventoryActivated)
            {
                CameraRotationX();
                CharacterRotation();
            }
        } 
    }

    private void WaterCheck()
    {
        if (GameManager._isWater)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _applySpeed = _swimFastSpeed;
            }
            else
            {
                _applySpeed = _swimSpeed;
            }
        }
    }

    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    private void Crouch()
    {
        _isCrouch = !_isCrouch;
        _theCrosshair.CrouchingAnimation(_isCrouch);

        if (_isCrouch)
        {
            _applySpeed = _crouchSpeed;
            _applyCrouchPosY = _crouchPosY;
        }
        else
        {
            _applySpeed = _walkSpeed;
            _applyCrouchPosY = _originPosY;
        }

        StartCoroutine(CrouchCoroutine());
    }

    IEnumerator CrouchCoroutine()
    {
        float _posY = _theCamera.transform.localPosition.y;
        int _count = 0;

        while (_posY != _applyCrouchPosY)
        {
            _count++;
            _posY = Mathf.Lerp(_posY, _applyCrouchPosY, 0.3f);
            _theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if(_count>15)
                break;
            yield return null;
        }
        _theCamera.transform.localPosition = new Vector3(0, _applyCrouchPosY, 0f);
    }
    
    private void IsGround()
    {
        _isGround = Physics.Raycast(transform.position, Vector3.down, _capsuleCollider.bounds.extents.y+0.1f);
        _theCrosshair.JumpingAnimation(!_isGround);
    }
    
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround && _theStatusController.GetCurrentSp()>0 && !GameManager._isWater)
        {
            Jump();
        }
        else if (Input.GetKey(KeyCode.Space) && GameManager._isWater)
        {
            UpSwim();
        }
    }

    private void UpSwim()
    {
        _myRigid.velocity = transform.up * _upSwimSpeed;
    }

    private void Jump()
    {
        if(_isCrouch)
            Crouch();
        _theStatusController.DecreaseStamina(100);
        _myRigid.velocity = transform.up * _jumpForce;
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift)&& _theStatusController.GetCurrentSp()>0)
        {
            Running();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)|| _theStatusController.GetCurrentSp()<=0) 
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        if(_isCrouch)
            Crouch();

        _theGunController.CancelFineSight();

        _isRun = true;
        _theCrosshair.RunningAnimation(_isRun);
        _theStatusController.DecreaseStamina(10);
        _applySpeed = _runSpeed;
    }

    private void RunningCancel()
    {
        _isRun = false;
        _theCrosshair.RunningAnimation(_isRun);
        _applySpeed = _walkSpeed;
    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized*_applySpeed;

        _myRigid.MovePosition(transform.position + _velocity * Time.deltaTime); 
    }

    private void MoveCheck()
    {
        if (!_isRun && !_isCrouch && _isGround )
        {
            if (Vector3.Distance(_lastPos, transform.position)>=0.01f)
            {
                _isWalk = true;
            }
            else
            {
                _isWalk = false;
            }
            _theCrosshair.WalkingAnimation(_isWalk);
            _lastPos = transform.position;
        }
    }

    private void CameraRotationX()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraToationX = _xRotation * _lookSensitivity;
        _currentCameraRotationX -= _cameraToationX;
        _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -_cameraRotationLimit, _cameraRotationLimit);

        _theCamera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()
    {
        float _yRoatation = Input.GetAxisRaw("Mouse X");
        Vector3 _chatacterRotationY = new Vector3(0f, _yRoatation, 0f) * _lookSensitivity;
        _myRigid.MoveRotation(_myRigid.rotation * Quaternion.Euler(_chatacterRotationY));
    }
}
