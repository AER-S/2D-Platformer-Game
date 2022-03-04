using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Sound
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;

    [SerializeField, Range(0f, 1f)] private float volume;
    [SerializeField, Range(1f, 3f)] private float pitch;
    [SerializeField] private bool loop;

    public string GetName() => name;
    public AudioClip GetClip() => clip;
    public float GetVolume() => volume;
    public float GetPitch() => pitch;
    public bool Getloop() => loop;
}
