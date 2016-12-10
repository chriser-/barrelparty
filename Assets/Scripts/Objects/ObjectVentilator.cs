using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVentilator : ObjectBase
{
    public float m_strengthMax = 100.0f;
    public float m_duration = 5.0f;

    private AudioSource aud;
    private float startVolume;

    private float timer;

    private void Start()
    {
        WindzoneController windController = GetComponentInChildren<WindzoneController>();
        windController.strengthMax = m_strengthMax;
        Destroy(this.gameObject, m_duration);
        timer = 0;
        aud = GetComponent<AudioSource>();
        aud.pitch = 0.4f;
        startVolume = aud.volume;
        aud.volume = 0;
    }

    private void Update()
    {


        if (timer + 1.0f > m_duration)
        {
            aud.pitch -= Time.deltaTime * 0.7f;
            aud.volume -= Time.deltaTime * 1.0f;
        }
        else
        {
            if (aud.pitch < 1)
                aud.pitch += Time.deltaTime * 0.3f;

            if (aud.volume < startVolume)
                aud.volume += Time.deltaTime * 0.3f;

            timer += Time.deltaTime;
        }
        
    }
}
