﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class BarrelController : MonoBehaviour
{
    public float Speed
    {
        get { return m_Speed; }
    }
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_SpeedGainPerSecond = 0.1f;

    void Awake()
    {
        
    }
	
    void Update()
    {
        transform.Rotate(Time.deltaTime*m_Speed, 0, 0);
        m_Speed += m_SpeedGainPerSecond*Time.deltaTime;
    }

    private void OnCollisionExit(Collision collision)
    {
        //Fix to make jump feel better
        PlayerController player;
        if ((player = collision.gameObject.GetComponent<PlayerController>()) != null)
        {
            player.Character.AddBarrelFriction(-m_Speed * Time.deltaTime * 100f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        PlayerController player;
        if ((player = collision.gameObject.GetComponent<PlayerController>()) != null)
        {
            player.Character.AddBarrelFriction(m_Speed*Time.deltaTime*100f);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        
        PlayerController player;
        if ((player = other.gameObject.GetComponent<PlayerController>()) != null)
        {
            player.Die();
        }
    }
    
}
