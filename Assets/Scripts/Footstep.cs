using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour {

    [System.Serializable]
    public struct Audiopair
    {
        public string audioName;
        public PhysicMaterial mat;
    }

    public Audiopair[] audios;

    protected void OnTriggerEnter(Collider c)
    {
        PhysicMaterial m = c.sharedMaterial;

        foreach (Audiopair p in audios)
        {
            if (p.mat == m) audioID = p.audioName;
        }
        if (string.IsNullOrEmpty(audioID)) return;

        _Play();

    }

    public enum PlayPosition
    {
        Global,
        ChildObject,
        ObjectPosition,
    }

    private string audioID;
    public PlayPosition position = PlayPosition.Global; // has no meaning for Music
    public float volume = 1;
    public float delay = 0;
    public float startTime = 0;



    private void _Play()
    {
        switch (position)
        {
            case PlayPosition.Global:
                AudioController.Play(audioID, volume, delay, startTime); break;
            case PlayPosition.ChildObject:
                AudioController.Play(audioID, transform, volume, delay, startTime); break;
            case PlayPosition.ObjectPosition:
                AudioController.Play(audioID, transform.position, null, volume, delay, startTime); break;
        }
    }


}
