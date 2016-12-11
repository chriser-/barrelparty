using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotation : MonoBehaviour
{
    public GameObject m_cameraObject;
    [Range(0.1f, 1.0f)]
    public float m_influenceFactor = 0.5f;

    // Camera will rotate from -40 to 40
    // We will know where the end point is
    // ergo we can interpolate with influence parameter

    private Vector3 startPos;
    private Vector3 endPos;

	void Start ()
    {
        startPos = transform.position;
        endPos = new Vector3(transform.position.x, 2.0f * transform.position.y * m_influenceFactor, transform.position.z);
    }
	
	void Update ()
    {
        transform.position = Vector3.Lerp(startPos, endPos, (m_cameraObject.transform.rotation.x + 40.0f) / 80.0f);
	}
}
