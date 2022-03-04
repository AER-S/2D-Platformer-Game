using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UISound
{
    Havor,
    Exit,
    Confirm,
    Start,
    Back
}

public enum PlayerSound
{
    Walk,
    Run,
    Jump,
    Land,
    Hurt,
    Die
}

public enum EnemySound
{
    Walk,
    Attack
}
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    [SerializeField] private AudioSource enterButtonSource;
    [SerializeField] private AudioSource exitButtonSource;
    [SerializeField] private AudioSource clickButtonSource;
    [SerializeField] private Sound[] uISounds;
    [SerializeField] private Sound[] enemySounds;
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayUI(UISound _name)
    {
        string name = GetUISourceName(_name);
        AudioSource source = GetUIAudioSource(_name);
        PlaySound(name, uISounds, source);
    }

    AudioSource GetUIAudioSource(UISound _name)
    {
        AudioSource source = null;
        switch (_name)
        {
            case UISound.Havor: 
                source= enterButtonSource;
                break;
            case UISound.Exit : 
                source= exitButtonSource;
                break;
            case UISound.Confirm:
            case UISound.Start:
            case UISound.Back: 
                source = clickButtonSource;
                break;
        }

        return source;
    }

    string GetUISourceName(UISound _name)
    {
        string name = null;
        switch (_name)
        {
            case UISound.Havor:
                name = "Havor";
                break;
            case UISound.Exit:
                name = "Exit";
                break;
            case UISound.Confirm:
                name = "Confirm";
                break;
            case UISound.Back: 
                name = "Back";
                break;
            case UISound.Start: 
                name = "Start";
                break;
        }

        return name;
    }
    public void PlaySound(string _name, Sound[] _sounds, AudioSource _source)
    {
        Sound sound = Array.Find(_sounds, i => i.GetName() == _name);
        if (sound==null)
        {
            return;
        }

        if (_source.clip!=sound.GetClip())
        {
            _source.clip = sound.GetClip();
            _source.clip.name = sound.GetName();
            _source.volume = sound.GetVolume();
            _source.pitch = sound.GetPitch();
            _source.loop = sound.Getloop();
            _source.Play();
            return;
        }

        if (_source.clip==sound.GetClip() && ((_source.loop &&!_source.isPlaying)||!_source.loop))
        {
            _source.Play();
        }
        

    }

    public void PlayEnemySound(EnemySound _sound, AudioSource _source)
    {
        string name = GetEnemySoundName(_sound);
        PlaySound(name, enemySounds, _source);
    }
    string GetEnemySoundName(EnemySound _sound)
    {
        string sound = null;
        switch (_sound)
        {
            case EnemySound.Walk:
                sound = "Walk";
                break;
            case EnemySound.Attack:
                sound = "Attack";
                break;
        }

        return sound;
    }
    
}
