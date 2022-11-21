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
    [SerializeField] private float _runSpeed;
    private float _applySpeed;
    
    private Vector3 _direction;
    
    private bool _isAction;
    private bool _isWalking;
    private bool _isRunning;
    private bool _isDead;

    [SerializeField] private float _walkTime;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _runTime;
    private float _currentTime;

    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody _rigid;
    [SerializeField] private BoxCollider _boxCol;
    private AudioSource _theAudio;

    [SerializeField] private AudioClip[] _sound_Pig_Normal;
    [SerializeField] private AudioClip _sound_Pig_Hurt;
    [SerializeField] private AudioClip _sound_Pig_Dead;

    // Start is called before the first frame update
    private void Start()
    {
        _theAudio = GetComponent<AudioSource>();
        _currentTime = _waitTime;
        _isAction = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_isDead)
        {
            Move();
            Rotation();
            ElapseTime();
        }
    }

    private void Move()
    {
        if (_isWalking || _isRunning)
        {
            _rigid.MovePosition(transform.position+(transform.forward * _applySpeed * Time.deltaTime));
        }
    }

    private void Rotation()
    {
        if (_isWalking||_isRunning)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, _direction.y, 0f),0.01f);
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
        _isRunning = false;
        _applySpeed = _walkSpeed;
        _anim.SetBool("Walking", _isWalking);
        _anim.SetBool("Running", _isRunning);
        _direction.Set(0f,Random.Range(0f,360f),0f);
        RandomAction();
    }

    private void RandomAction()
    {
        RandomSound();

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
        _applySpeed = _walkSpeed;
        Debug.Log("걷기");
    }

    private void Run(Vector3 _targetPos)
    {
        _direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;
        _currentTime = _runTime;
        _isWalking = false;
        _isRunning = true;
        _applySpeed = _runSpeed;
        _anim.SetBool("Running", _isRunning);
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!_isDead)
        {
            _hp -= _dmg;
            if (_hp <= 0)
            {
                Dead();
                return;
            }
            PlaySE(_sound_Pig_Hurt);
            _anim.SetTrigger("Hurt");
            Run(_targetPos);
        }
        
    }

    private void Dead()
    {
        PlaySE(_sound_Pig_Dead);
        _isWalking = false;
        _isRunning = false;
        _isDead = true;
        _anim.SetTrigger("Dead");
    }

    private void RandomSound()
    {
        int _random = Random.Range(0, 3);
        PlaySE(_sound_Pig_Normal[_random]);
    }
    
    private void PlaySE(AudioClip _clip)
    {
        _theAudio.clip = _clip;
        _theAudio.Play();
    }
}
