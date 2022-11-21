using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewAngle : MonoBehaviour
{
    [SerializeField] private float _viewAngle;

    [SerializeField] private float _viewDistance;

    [SerializeField] private LayerMask _targetMask;

    private Pig _thePig;

    private void Start()
    {
        _thePig = GetComponent<Pig>();
    }

    // Update is called once per frame
    private void Update()
    {
        View();
    }

    private Vector3 BoundaryAngle(float _angle)
    {
        _angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(-_angle * Mathf.Deg2Rad),0f,Mathf.Cos(_angle * Mathf.Deg2Rad));
    }

    private void View()
    {
        Vector3 _leftBoundary = BoundaryAngle(-_viewAngle*0.5f);
        Vector3 _rightBoundary = BoundaryAngle(_viewAngle*0.5f);
        
        Debug.DrawRay(transform.position+ transform.up, _leftBoundary,Color.red);
        Debug.DrawRay(transform.position+ transform.up, _rightBoundary,Color.red);

        Collider[] _target = Physics.OverlapSphere(transform.position, _viewDistance, _targetMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            if (_targetTf.name=="Player")
            {
                Vector3 _direction = (_targetTf.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);

                if (_angle<_viewAngle * 0.5f)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, _viewDistance ))
                    {
                        if (_hit.transform.name == "Player")
                        {
                            Debug.Log("플레이어가 돼지 시야 내에 있습니다.");
                            Debug.DrawRay(transform.position + transform.up, _direction, Color.blue);
                            _thePig.Run(_hit.transform.position);
                        }
                        
                    }
                }
            }
        }
    }
}
