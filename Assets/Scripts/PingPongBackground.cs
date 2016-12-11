using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongBackground : MonoBehaviour
{
    public float speed = 1.0f;
    public float travelDistance = 0.5f;

	void Start ()
    {
		
	}

	void Update ()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, travelDistance), transform.position.y, transform.position.z);
    }
}
