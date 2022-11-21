using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pig : WeakAnimal
{
    protected override void Reset()
    {
        base.Reset();
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

}
