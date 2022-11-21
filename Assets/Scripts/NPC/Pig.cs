using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pig : MonoBehaviour
{
    [SerializeField] private string _animalName;

    [SerializeField] private int _hp;

    [SerializeField] private float _walkSpeed;

    private Vector3 _direction;
    
    private bool _isAction;
    private bool _isWalking;

    [SerializeField] private float _walkTime;
    [SerializeField] private float _waitTime;
    private float _currentTime;

    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody _rigid;
    [SerializeField] private BoxCollider _boxCol;

    // Start is called before the first frame update
    private void Start()
    {
        _currentTime = _waitTime;
        _isAction = true;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Rotation();
        ElapseTime();
    }

    private void Move()
    {
        if (_isWalking)
        {
            _rigid.MovePosition(transform.position+(transform.forward * _walkSpeed * Time.deltaTime));
        }
    }

    private void Rotation()
    {
        if (_isWalking)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, _direction, 0.01f);
            _rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }

    private void ElapseTime()
    {
        if (_isAction)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                Reset();
            }
        }
    }

    private void Reset()
    {
        _isWalking = false;
        _isAction = true;
        _anim.SetBool("Walking", _isWalking);
        _direction.Set(0f,Random.Range(0f,360f),0f);
        RandomAction();
    }

    private void RandomAction()
    {
        _isAction = true;

        int _random = Random.Range(0, 4); //대기, 풀뜯기, 두리번, 걷기

        if (_random == 0)
        {
            Wait();
        }
        else if (_random == 1)
        {
            Eat();
        }
        else if (_random == 2)
        {
            Peek();
        }else if (_random == 3)
        {
            TryWalk();
        }
    }

    private void Wait()
    {
        _currentTime = _waitTime;
        Debug.Log("대기");
    }
    
    private void Eat()
    {
        _currentTime = _waitTime;
        _anim.SetTrigger("Eat");
        Debug.Log("풀뜯기");
    }
    
    private void Peek()
    {
        _currentTime = _waitTime;
        _anim.SetTrigger("Peek");
        Debug.Log("두리번");
    }
    
    private void TryWalk()
    {
        _isWalking = true;
        _anim.SetBool("Walking", _isWalking);
        _currentTime = _walkTime;
        Debug.Log("걷기");
    }
}
