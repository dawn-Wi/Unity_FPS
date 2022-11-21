using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    
    [SerializeField] protected string _animalName;

    [SerializeField] protected int _hp;

    [SerializeField] protected float _walkSpeed;
    [SerializeField] protected float _runSpeed;
    
    protected Vector3 _destination;
    
    protected bool _isAction;
    protected bool _isWalking;
    protected bool _isRunning;
    protected bool _isDead;

    [SerializeField] protected float _walkTime;
    [SerializeField] protected float _waitTime;
    [SerializeField] protected float _runTime;
    protected float _currentTime;

    [SerializeField] protected Animator _anim;
    [SerializeField] protected Rigidbody _rigid;
    [SerializeField] protected BoxCollider _boxCol;
    protected AudioSource _theAudio;
    protected NavMeshAgent _nav;
    
    [SerializeField] protected AudioClip[] _sound_Normal;
    [SerializeField] protected AudioClip _sound_Hurt;
    [SerializeField] protected AudioClip _sound_Dead;
// Start is called before the first frame update
    private void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
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
            ElapseTime();
        }
    }

    protected void Move()
    {
        if (_isWalking || _isRunning)
        {
            // _rigid.MovePosition(transform.position+(transform.forward * _applySpeed * Time.deltaTime));
            _nav.SetDestination(transform.position + _destination * 5f);
        }
    }

    protected void ElapseTime()
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

    protected virtual void Reset()
    {
        _isWalking = false;
        _isAction = true;
        _isRunning = false;
        _nav.speed = _walkSpeed;
        _nav.ResetPath();
        _anim.SetBool("Walking", _isWalking);
        _anim.SetBool("Running", _isRunning);
        _destination.Set(Random.Range(-0.2f, 0.2f),0f,Random.Range(0.5f, 1f));
    }

    protected void TryWalk()
    {
        _isWalking = true;
        _anim.SetBool("Walking", _isWalking);
        _currentTime = _walkTime;
        _nav.speed = _walkSpeed;
        Debug.Log("걷기");
    }

   

    public virtual void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!_isDead)
        {
            _hp -= _dmg;
            if (_hp <= 0)
            {
                Dead();
                return;
            }
            PlaySE(_sound_Hurt);
            _anim.SetTrigger("Hurt");
        }
        
    }

    protected void Dead()
    {
        PlaySE(_sound_Dead);
        _isWalking = false;
        _isRunning = false;
        _isDead = true;
        _anim.SetTrigger("Dead");
    }

    protected void RandomSound()
    {
        int _random = Random.Range(0, 3);
        PlaySE(_sound_Normal[_random]);
    }
    
    protected void PlaySE(AudioClip _clip)
    {
        _theAudio.clip = _clip;
        _theAudio.Play();
    }
}
