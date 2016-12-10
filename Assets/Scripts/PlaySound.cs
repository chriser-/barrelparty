using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    public string audioID;

public void Play()
    {
        AudioController.Play(audioID);
    }
}
