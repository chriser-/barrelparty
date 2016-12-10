using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ObjectRocket : ObjectBase
{
    public float m_speed = 1.0f;
    public float m_duration = 10.0f;

    private bool m_isActive = false;
    private PlayerController charController;

    void Update()
    {
        if (!m_isActive)
            return;

        transform.RotateAround(Vector3.zero, Vector3.forward, m_speed);
    }

    private void OnDestroy()
    {
        charController.DetachHands();
    }

    private void OnCollisionEnter(Collision collision)
    {
        charController = collision.gameObject.GetComponent<PlayerController>();
        if (charController != null)
        {
            charController.AttachHands(transform, transform);
            m_isActive = true;
            Destroy(gameObject, m_duration);
        }
    }
}
