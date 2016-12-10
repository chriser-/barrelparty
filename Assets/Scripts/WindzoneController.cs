using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindzoneController : MonoBehaviour
{
    private float m_strengthMax = 100.0f;
    public float strengthMax
    {
        get { return m_strengthMax;  }
        set { m_strengthMax = Mathf.Max(value, 0.0f); }
    }
    private float m_strength = 0.0f;

    // Maybe add force should be shifted into FixedUpdate()
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float distanceMax = transform.lossyScale.x;
            float distance = (transform.position - transform.localPosition).x - other.transform.position.x;
            // distance / distanceMax is sometimes smaller or bigger than 1 due to colliders
            m_strength = (1.0f - distance / distanceMax) * m_strengthMax;
            rb.AddForce(m_strength * -gameObject.transform.right);
        }
    }
}
