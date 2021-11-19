using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSoundsController : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private AudioSource movementsSoundsSource;
    [SerializeField] private AudioSource emotesSoundsSource;

    AudioSource GetNeededPlayerAudioSource(PlayerSound _sound)
    {
        AudioSource source = null;
        switch (_sound)
        {
            case PlayerSound.Walk:
            case PlayerSound.Run:
            case PlayerSound.Jump:
            case PlayerSound.Land:
                source = movementsSoundsSource;
                break;
            case PlayerSound.Hurt:
            case PlayerSound.Die:
                source = emotesSoundsSource;
                break;
        }

        return source;
    }

    string GetPlayerSoundName(PlayerSound _sound)
    {
        string sound = null;
        switch (_sound)
        {
            case PlayerSound.Walk:
                sound = "Walk";
                break;
            case PlayerSound.Run:
                sound = "Run";
                break;
            case PlayerSound.Jump:
                sound = "Jump";
                break;
            case PlayerSound.Land:
                sound = "Land";
                break;
            case PlayerSound.Hurt:
                sound = "Hurt";
                break;
            case PlayerSound.Die:
                sound = "Die";
                break;
        }

        return sound;
    }

    public void PlayPlayerSound(PlayerSound _sound)
    {
        AudioManager.Instance.PlaySound(GetPlayerSoundName(_sound),sounds,GetNeededPlayerAudioSource(_sound));
    }

    public void StopPlayingMovementSound()
    {
        if (movementsSoundsSource.isPlaying)
        {
            movementsSoundsSource.Stop();
        }
    }

    public void StopSoundOnJump()
    {

        if (movementsSoundsSource.clip)
        {
            if (movementsSoundsSource.clip.name!="Jump")
            {
                movementsSoundsSource.Stop();
            }
            movementsSoundsSource.Stop();
        }
    }

}
