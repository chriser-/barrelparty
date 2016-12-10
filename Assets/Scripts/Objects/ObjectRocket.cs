﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ObjectRocket : ObjectBase
{
    public float m_speed = 1.0f;
    public float m_duration = 3.0f;

    
    public PlayerController CharController;

    private bool m_isActive = false;


    void Update()
    {
        if (!m_isActive)
            return;

        transform.RotateAround(Vector3.zero, Vector3.forward, m_speed);
    }

    private void OnDestroy()
    {
        if(CharController != null)
            CharController.DetachHands();
    }

    public void StartRocket()
    {
        if (CharController != null)
        {
            CharController.AttachHands(transform, transform);
            m_isActive = true;
            StartCoroutine(destroyRocket());
        }
    }

    private IEnumerator destroyRocket()
    {
        yield return new WaitForSeconds(m_duration);
        CharController.DetachHands();
        CharController.GravityDone = false;
        Destroy(gameObject);
    }
}
