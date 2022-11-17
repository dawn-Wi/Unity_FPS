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

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _effect_sound;
    
    [SerializeField] private AudioClip _effect_sound2;

    public void Mining()
    {
        _audioSource.clip = _effect_sound;
        _audioSource.Play();
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
        _audioSource.clip = _effect_sound2;
        _audioSource.Play();
        _col.enabled = false;
        Destroy(_go_rock);
        
        _go_debris.SetActive(true);
        Destroy(_go_debris, _destroyTime);
    }
}
