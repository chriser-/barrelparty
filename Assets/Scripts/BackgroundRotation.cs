using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotation : MonoBehaviour
{
    public GameObject m_cameraObject;
    [Range(0.1f, 1.0f)]
    public float m_influenceFactor = 0.5f;

    // Camera will rotate from 40 to -40
    // We will know where the end point is
    // ergo we can interpolate with influence parameter

    private Vector3 startPos;
    private Vector3 endPos;

	void Start ()
    {
        startPos = transform.position;
        endPos = new Vector3(transform.position.x, -2.0f * transform.position.y * m_influenceFactor, transform.position.z);
    }
	
	void Update ()
    {
        float angle = m_cameraObject.transform.rotation.eulerAngles.x;
        angle = angle > 180.0f ? angle - 360.0f : angle;
        transform.position = Vector3.Lerp(startPos, endPos, 1.0f - (angle + 40.0f) / 80.0f);
	}
}
