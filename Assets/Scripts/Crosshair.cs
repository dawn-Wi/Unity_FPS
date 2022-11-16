using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] 
    private Animator _animator;

    private float _gunAccuracy;

    [SerializeField] 
    private GameObject _go_CrosshairHUD;

    [SerializeField] private GunController _theGunController;
    
    public void WalkingAnimation(bool _flag)
    {
        _animator.SetBool("Walking", _flag);
    }
    
    public void RunningAnimation(bool _flag)
    {
        _animator.SetBool("Running", _flag);
    }
    public void CrouchingAnimation(bool _flag)
    {
        _animator.SetBool("Crouching", _flag);
    }
    
    public void FineSightAnimation(bool _flag)
    {
        _animator.SetBool("FineSight", _flag);
    }

    public void FireAnimation()
    {
        if (_animator.GetBool("Walking"))
        {
            _animator.SetTrigger("Walk_Fire");
        }else if (_animator.GetBool("Crouching"))
        {
            _animator.SetTrigger("Crouch_Fire");
        }
        else
        {
            _animator.SetTrigger("Idle_Fire");
        }
    }

    public float GetAccuracy()
    {
        if (_animator.GetBool("Walking"))
        {
            _gunAccuracy = 0.08f;
        }else if (_animator.GetBool("Crouching"))
        {
            _gunAccuracy = 0.02f;
        }
        else if(_theGunController.GetFineSightMode())
        {
            _gunAccuracy = 0.001f;
        }
        else
        {
            _gunAccuracy = 0.04f;
        }

        return _gunAccuracy;
    }
}
