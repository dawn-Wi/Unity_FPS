using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour
{
    [SerializeField] private float _waterDrag;
    private float _originDrag;

    [SerializeField] private Color _waterColor;

    [SerializeField] private float _waterFogDensity;

    [SerializeField] private Color _waterNightColor;
    [SerializeField] private float _waterNightFogDensity;

    private Color _originColor;
    private float _originFogDensity;

    [SerializeField] private Color _originNightColor;
    [SerializeField] private float _originNightFogDensity;

    [SerializeField] private string _sound_WaterOut;
    [SerializeField] private string _sound_WaterIn;
    [SerializeField] private string _sound_Breathe;

    [SerializeField] private float _breatheTime;
    private float _currentBreatheTime;

    [SerializeField] private float _totalOxygen;
    private float _currentOxygen;
    private float _temp;

    [SerializeField] private GameObject _go_baseUi;
    [SerializeField] private Text _text_totalOxygen;
    [SerializeField] private Text _text_currentOxygen;
    [SerializeField] private Image _image_gauge;

    private StatusController _thePlayerStat;
    
    // Start is called before the first frame update
    private void Start()
    {
        _originColor = RenderSettings.fogColor;
        _originFogDensity = RenderSettings.fogDensity;

        _originDrag = 0;
        _thePlayerStat = FindObjectOfType<StatusController>();
        _currentOxygen = _totalOxygen;
        _text_totalOxygen.text = _totalOxygen.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager._isWater)
        {
            _currentBreatheTime += Time.deltaTime;
            if (_currentBreatheTime>=_breatheTime)
            {
                SoundManager._instansce.PlaySE(_sound_Breathe);
                _currentBreatheTime = 0;
            }
        }

        DecreaseOxygen();
    }

    private void DecreaseOxygen()
    {
        if (GameManager._isWater)
        {
            _currentOxygen -= Time.deltaTime;
            _text_currentOxygen.text = Mathf.RoundToInt(_currentOxygen).ToString();
            _image_gauge.fillAmount = _currentOxygen/_totalOxygen;

            if (_currentOxygen<=0)
            {
                _temp += Time.deltaTime;
                if (_temp>=1)
                {
                    _thePlayerStat.DecreaseHP(1);
                    _temp = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GetWater(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GetOutWater(other);
        }
    }

    private void GetWater(Collider _player)
    {
        SoundManager._instansce.PlaySE(_sound_WaterIn);
        _go_baseUi.SetActive(true);
        GameManager._isWater = true;
        _player.transform.GetComponent<Rigidbody>().drag = _waterDrag;

        if (!GameManager._isNight)
        {
            RenderSettings.fogColor = _waterColor;
            RenderSettings.fogDensity = _waterFogDensity;
        }
        else
        {
            RenderSettings.fogColor = _waterNightColor;
            RenderSettings.fogDensity = _waterNightFogDensity;
        }

    }
    
    private void GetOutWater(Collider _player)
    {
        if (GameManager._isWater)
        {
            _go_baseUi.SetActive(false);
            _currentOxygen = _totalOxygen;
            SoundManager._instansce.PlaySE(_sound_WaterOut);
            
            GameManager._isWater = false;
            _player.transform.GetComponent<Rigidbody>().drag = _originDrag;

            if (!GameManager._isNight)
            {
                RenderSettings.fogColor = _originColor;
                RenderSettings.fogDensity = _originFogDensity;
            }
            else
            {
                
                RenderSettings.fogColor = _originNightColor;
                RenderSettings.fogDensity = _originNightFogDensity;
            }
        }
    }
}
