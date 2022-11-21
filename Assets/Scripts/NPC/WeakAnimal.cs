using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakAnimal : Animal
{
    
    public void Run(Vector3 _targetPos)
    {
        _destination = new Vector3(transform.position.x - _targetPos.x, 0f, transform.position.z - _targetPos.z)
            .normalized;
        _currentTime = _runTime;
        _isWalking = false;
        _isRunning = true;
        _nav.speed = _runSpeed;
        _anim.SetBool("Running", _isRunning);
    }

    public override void Damage(int _dmg, Vector3 _targetPos)
    {
        base.Damage(_dmg, _targetPos);
        if (!_isDead)
        {
            Run(_targetPos);
        }
    }
}
