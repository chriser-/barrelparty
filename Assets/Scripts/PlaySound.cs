using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    public enum AudioT
    {
        SFX,
        Music
    }

    public AudioT type;
    public string audioID;

public void Play()
    {
        if (type == AudioT.SFX)
            AudioController.Play(audioID);
        else
        {
            AudioController.PlayMusic(audioID);
        }
    }
}
