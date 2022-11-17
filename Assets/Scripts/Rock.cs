using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int _hp;

    [SerializeField] 
    private float _destroyTime;

    [SerializeField] 
    private SphereCollider _col;

    [SerializeField]
    private GameObject _go_rock;

    [SerializeField] 
    private GameObject _go_debris;

    [SerializeField] private GameObject _go_effect_prefabs;

    [SerializeField] private string _strike_Sound;

    [SerializeField] private string _destroy_Sound;

    public void Mining()
    {
        SoundManager._instansce.PlaySE(_strike_Sound);
        var clone =Instantiate(_go_effect_prefabs, _col.bounds.center, Quaternion.identity);
        Destroy(clone, _destroyTime);
        
        _hp--;
        if (_hp <= 0)
        {
            Destruction();
        }
    }

    private void Destruction()
    {
        SoundManager._instansce.PlaySE(_destroy_Sound);
        _col.enabled = false;
        Destroy(_go_rock);
        
        _go_debris.SetActive(true);
        Destroy(_go_debris, _destroyTime);
    }
}
