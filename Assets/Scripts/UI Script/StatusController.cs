using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    [SerializeField]
    private int _hp;
    private int _currentHp;

    [SerializeField]
    private int _sp;
    private int _currentSp;

    [SerializeField] 
    private int _spIncreaseSpeed;
    
    //스테미나 재회복 딜레이
    [SerializeField] 
    private int _spRechargeTime;
    private int _currentSpRechargeTime;

    //스테미나 감소여부
    private bool _spUsed;

    [SerializeField] 
    private int _dp;
    private int _currentDp;

    [SerializeField]
    private int _hungry;
    private int _currentHungry;
    
    [SerializeField] 
    private int _hungryDecreaseTime;
    private int _currentHungryDecreaseTime;

    [SerializeField] 
    private int _thirsty;
    private int _currentThirsty;

    [SerializeField] 
    private int _thirstyDecreaseTime;
    private int _currentThirstyDecreaseTime;

    [SerializeField] 
    private int _satisfy;
    private int _currentSatisfy;

    [SerializeField] 
    private Image[] _images_Gauge;

    private const int HP = 0, DP = 1, SP = 2, HUNGRY = 3, THIRSTY = 4, SATISFY = 5;
    // Start is called before the first frame update
    private void Start()
    {
        _currentHp = _hp;
        _currentDp = _dp;
        _currentSp = _sp;
        _currentHungry = _hungry;
        _currentThirsty = _thirsty;
        _currentSatisfy = _satisfy;
    }

    // Update is called once per frame
    private void Update()
    {
        Hungry();
        Thirsty();
        SPRechargeTime();
        SPRecover();
        GaugeUpdate();
        
    }

    private void SPRechargeTime()
    {
        if (_spUsed)
        {
            if (_currentSpRechargeTime < _spRechargeTime)
            {
                _currentSpRechargeTime++;
            }
            else
            {
                _spUsed = false;
            }
        }
    }

    private void SPRecover()
    {
        if (!_spUsed && _currentSp < _sp)
        {
            _currentSp += _spIncreaseSpeed;
        }
    }
    
    private void Hungry()
    {
        if (_currentHungry > 0)
        {
            if (_currentHungryDecreaseTime <= _hungryDecreaseTime)
            {
                _currentHungryDecreaseTime++;
            }
            else
            {
                _currentHungry--;
                _currentHungryDecreaseTime = 0;
            }
        }
        else
        {
            Debug.Log("배고픔 수치가 0이 되었습니다.");
        }
    }

    private void Thirsty()
    {
        if (_currentThirsty > 0)
        {
            if (_currentThirstyDecreaseTime <= _thirstyDecreaseTime)
            {
                _currentThirstyDecreaseTime++;
            }
            else
            {
                _currentThirsty--;
                _currentThirstyDecreaseTime = 0;
            }
        }
        else
        {
            Debug.Log("목마름 수치가 0이 되었습니다.");
        }
    }

    private void GaugeUpdate()
    {
        _images_Gauge[HP].fillAmount = (float)_currentHp / _hp;
        _images_Gauge[SP].fillAmount = (float)_currentSp / _sp;
        _images_Gauge[DP].fillAmount = (float)_currentDp / _dp;
        _images_Gauge[HUNGRY].fillAmount = (float)_currentHungry / _hungry;
        _images_Gauge[THIRSTY].fillAmount = (float)_currentThirsty / _thirsty;
        _images_Gauge[SATISFY].fillAmount = (float)_currentSatisfy / _satisfy;
    }

    public void IncreaseHP(int _count)
    {
        if (_currentHp +_count<_hp)
        {
            _currentHp += _count;
        }
        else
        {
            _currentHp = _hp;
        }
    }

    public void DecreaseHP(int _count)
    {
        if (_currentDp>0)
        {
            DecreaseDP(_count);
            return;
        }
        _currentHp -= _count;
        if (_currentHp <= 0)
        {
            Debug.Log("캐릭터의 hp가 0이 되었습니다.");
        }
    }
    
    public void IncreaseDP(int _count)
    {
        if (_currentDp +_count<_dp)
        {
            _currentDp += _count;
        }
        else
        {
            _currentDp = _dp;
        }
    }

    public void DecreaseDP(int _count)
    {
        _currentDp -= _count;
        if (_currentDp <= 0)
        {
            Debug.Log("방어력이 0이 되었습니다.");
        }
    }
    
    public void IncreaseHungry(int _count)
    {
        if (_currentHungry +_count<_hungry)
        {
            _currentHungry += _count;
        }
        else
        {
            _currentHungry = _hungry;
        }
    }

    public void DecreaseHungry(int _count)
    {
        if (_currentHungry- _count<0)
        {
            _currentHungry = 0;
        }
        else
        {
            _currentHungry -= _count;
        }
    }
    
    public void IncreaseThirsty(int _count)
    {
        if (_currentThirsty +_count<_thirsty)
        {
            _currentThirsty += _count;
        }
        else
        {
            _currentThirsty = _thirsty;
        }
    }
    
    public void DecreaseThirsty(int _count)
    {
        if (_currentThirsty- _count<0)
        {
            _currentThirsty = 0;
        }
        else
        {
            _currentThirsty -= _count;
        }
    }
    
    public void DecreaseStamina(int _count)
    {
        _spUsed = true;
        _currentSpRechargeTime = 0;

        if (_currentSp - _count >0)
        {
            _currentSp -= _count;
        }
        else
        {
            _currentSp = 0;
        }
    }

    public int GetCurrentSp()
    {
        return _currentSp;
    }
}
