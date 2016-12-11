using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setVolume(float volume)
    {
        AudioController.SetGlobalVolume(volume);
    }

    public void setMusicVolume(float volume)
    {
        AudioController.SetCategoryVolume("Music", volume);
    }

    public void setSFXVolume(float volume)
    {
        AudioController.SetCategoryVolume("SFX", volume);
        AudioController.Play("Countdown");
    }
}
