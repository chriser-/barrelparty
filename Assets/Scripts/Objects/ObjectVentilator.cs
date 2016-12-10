using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVentilator : ObjectBase
{
    public float m_strengthMax = 100.0f;
    public float m_duration = 5.0f;

    private AudioSource audio;
    private float startVolume;

    private float timer;

    private void Start()
    {
        WindzoneController windController = GetComponentInChildren<WindzoneController>();
        windController.strengthMax = m_strengthMax;
        Destroy(this.gameObject, m_duration);
        timer = 0;
        audio = GetComponent<AudioSource>();
        audio.pitch = 0.4f;
        startVolume = audio.volume;
        audio.volume = 0;
    }

    private void Update()
    {


        if (timer + 1.0f > m_duration)
        {
            audio.pitch -= Time.deltaTime * 0.7f;
            audio.volume -= Time.deltaTime * 1.0f;
        }
        else
        {
            if (audio.pitch < 1)
                audio.pitch += Time.deltaTime * 0.3f;

            if (audio.volume < startVolume)
                audio.volume += Time.deltaTime * 0.3f;

            timer += Time.deltaTime;
        }
        
    }
}
