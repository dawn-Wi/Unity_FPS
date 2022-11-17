using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string _name;
    public AudioClip _clip;
}

public class SoundManager : MonoBehaviour
{
    static public SoundManager _instansce;

    #region Singleton
    private void Awake()
    {
        if (_instansce == null)
        {
            _instansce = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    #endregion

    public AudioSource[] _audioSourceEffects;
    public AudioSource _audioSourceBgm;

    public string[] _playSoundName;

    public Sound[] _effectSounds;
    public Sound[] _bgmSound;

    private void Start()
    {
        _playSoundName = new string[_audioSourceEffects.Length];
    }

    public void PlaySE(string _name)
    {
        for (int i = 0; i < _effectSounds.Length; i++)
        {
            if (_name == _effectSounds[i]._name)
            {
                for (int j = 0; j < _audioSourceEffects.Length; j++)
                {
                    if (!_audioSourceEffects[j].isPlaying)
                    {
                        _playSoundName[j] = _effectSounds[i]._name;
                        _audioSourceEffects[j].clip = _effectSounds[i]._clip;
                        _audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }

    public void StopALlSE()
    {
        for (int i = 0; i < _audioSourceEffects.Length; i++)
        {
            _audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < _audioSourceEffects.Length; i++)
        {
            if (_playSoundName[i] == _name)
            {
                _audioSourceEffects[i].Stop();
                return;
            }
        }
        Debug.Log("재생중인"+ _name + "사운드가 없습니다.");
    }
    
}
