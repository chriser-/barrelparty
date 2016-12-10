using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotationController : MonoBehaviour
{
    public Transform m_rotationPivot;
    public float m_speed = 1.0f;

	void Update ()
    {
        transform.RotateAround(m_rotationPivot.position, Vector3.up, m_speed);
	}
}
