using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SimpleTrap : MonoBehaviour
{
    private Rigidbody[] _rigid;

    [SerializeField] private GameObject _go_Meat;
    [SerializeField] private int _damage;

    private bool _isActivated = false;

    private AudioSource _theAudio;
    [SerializeField] private AudioClip _sound_Activate;

    // Start is called before the first frame update
    private void Start()
    {
        _rigid = GetComponentsInChildren<Rigidbody>();
        _theAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActivated)
        {
            if (other.transform.tag != "Untagged")
            {
                _isActivated = true;
                _theAudio.clip = _sound_Activate;
                _theAudio.Play();

                Destroy(_go_Meat);

                for (int i = 0; i < _rigid.Length; i++)
                {
                    _rigid[i].useGravity = true;
                    _rigid[i].isKinematic = false;
                }

                if (other.transform.name == "Player")
                {
                    FindObjectOfType<StatusController>().DecreaseHP(_damage);
                }
            }
        }
    }
}