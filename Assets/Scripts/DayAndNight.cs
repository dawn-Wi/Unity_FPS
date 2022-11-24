using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private float _secondPerRealTimeSecond;

    [SerializeField] private float _fogDensityCalc;

    [SerializeField] private float _nightFogDensity;

    private float _dayFogDensity;

    private float _currentFogDensity;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.right, 0.1f*_secondPerRealTimeSecond*Time.deltaTime);

        if (transform.eulerAngles.x >= 170)
        {
            GameManager._isNight = true;
        }
        else if (transform.eulerAngles.x>=340)
        {
            GameManager._isNight = false;
        }

        if (GameManager._isNight)
        {
            if (_currentFogDensity <= _nightFogDensity)
            {
                _currentFogDensity += 0.1f * _fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = _currentFogDensity;
            }
        }
        else
        {
            if (_currentFogDensity <= _dayFogDensity)
            {
                _currentFogDensity -= 0.1f * _fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = _currentFogDensity;
            }
        }
    }
}
