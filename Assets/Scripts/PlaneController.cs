﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public bool IsReady { get { return m_FlightDurationTimer < 0f; } }

    private Vector3 m_StartPosition;
    private const float m_FlightDuration = 5f;
    private float m_FlightDurationTimer = 0f;

    // Use this for initialization
    void Start()
    {
        m_StartPosition = transform.position;
        StartCoroutine(StartFlying());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FlightDurationTimer > 0)
        {
            transform.Translate(Vector3.up*Time.deltaTime*50f, Space.Self);
            m_FlightDurationTimer -= Time.deltaTime;
        }
        else
        {
            transform.position = m_StartPosition;
        }
    }

    private IEnumerator StartFlying()
    {
        yield return new WaitForSeconds(Random.Range(10,20));
        m_FlightDurationTimer = m_FlightDuration;
        AudioController.Play("Flyby");
    }
}
