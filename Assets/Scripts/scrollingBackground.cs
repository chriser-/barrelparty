using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingBackground : MonoBehaviour {

	public float parallaxSpeedIndex = 1f;
	public BarrelController barrelController;
	
	private float getSpeed (){
		return (parallaxSpeedIndex * - barrelController.getSpeed ());
	}

	void Update () {

		float amtToMove = getSpeed() * Time.deltaTime;
		transform.Translate(Vector3.right * amtToMove, Space.World);

		if (transform.position.x < -24.50f) {
			transform.position = new Vector3(24.50f, transform.position.y, transform.position.z);
		}
		if (transform.position.x > 24.50f) {
			transform.position = new Vector3(-24.50f, transform.position.y, transform.position.z);
		}
	}
}
