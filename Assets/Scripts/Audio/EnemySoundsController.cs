using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundsController : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    
    public void PlayEnemySound(EnemySound _sound)
    {
        AudioManager.Instance.PlayEnemySound(_sound, source);
    }

    public void StopSounds()
    {
        source.Stop();
    }

    
}
