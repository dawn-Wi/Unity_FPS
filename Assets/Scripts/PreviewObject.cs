using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{

    private List<Collider> _colliderList = new List<Collider>();

    [SerializeField] private int _layerGround;

    private const int IGNORE_RAYCAST_LAYER = 2;

    [SerializeField] private Material _green;

    [SerializeField] private Material _red;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (_colliderList.Count > 0)
        {
            SetColor(_red);
        }
        else
        {   
            SetColor(_green);
        }
    }

    private void SetColor(Material _mat)
    {
        foreach (Transform _tf_Child in this.transform)
        {
            var newMaterials = new Material[_tf_Child.GetComponent<Renderer>().materials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = _mat;
            }

            _tf_Child.GetComponent<Renderer>().materials = newMaterials;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != _layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
        {
            _colliderList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != _layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
        {
            _colliderList.Remove(other);
        }
    }

    public bool IsBuildable()
    {
        return _colliderList.Count == 0;
    }
}
