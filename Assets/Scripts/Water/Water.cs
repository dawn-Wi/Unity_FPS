using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static bool _isWater = false;
    [SerializeField] private float _waterDrag;
    private float _originDrag;

    [SerializeField] private Color _waterColor;

    [SerializeField] private float _waterFogDensity;

    private Color _originColor;
    private float _originFogDensity;
    
    // Start is called before the first frame update
    private void Start()
    {
        _originColor = RenderSettings.fogColor;
        _originFogDensity = RenderSettings.fogDensity;

        _originDrag = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
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
        
    }
    
    private void GetOutWater(Collider _player)
    {
        
    }
}
