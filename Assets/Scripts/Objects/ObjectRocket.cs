using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ObjectRocket : ObjectBase
{
    public float m_speed = 1.0f;
    public float m_duration = 10.0f;

    
    public PlayerController CharController;

    private bool m_isActive = false;

    void Awake()
    {
        startRocket();
    }

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

    private void startRocket()
    {
        if (CharController != null)
        {
            CharController.AttachHands(transform, transform);
            m_isActive = true;
            Destroy(gameObject, m_duration);
        }
    }
}
