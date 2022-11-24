using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    // Start is called before the first frame update
    private void Start()
    {
        _originColor = RenderSettings.fogColor;
        _originFogDensity = RenderSettings.fogDensity;

        _originDrag = 0;

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
        SoundManager._instansce.PlaySE(_sound_WaterOut);
        if (GameManager._isWater)
        {
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
