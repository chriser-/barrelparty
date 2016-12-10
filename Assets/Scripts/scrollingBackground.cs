using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingBackground : MonoBehaviour {

	// Transformation of 3 Sprites
	private Transform[] backgrounds;
	private static float scrollSpeed;

	void Start () {
		backgrounds = new Transform[2];   // 3 planes/walls 
		backgrounds[0] = transform.GetChild(0);
		backgrounds[1] = transform.GetChild(1);
		backgrounds[2] = transform.GetChild(2);

		scrollSpeed = - BarrelRotateController.getSpeed ();
	}

	private float getSpeed (){
		return scrollSpeed;
	}

	private void moveToLeft(){
		int positionTmp = backgrounds [0].position;
		float amtToMove = getSpeed() * Time.deltaTime;
		transform.Translate(Vector3.left * amtToMove, Space.World);
		if (backgrounds [2].position <= positionTmp)
			backgrounds [2].position = backgrounds [0].position;
	}

	private void moveToRight(){
		int positionTmp = backgrounds [2].position;
		float amtToMove = getSpeed() * Time.deltaTime;
		transform.Translate(Vector3.right * amtToMove, Space.World);
		if (backgrounds [0].position >= positionTmp)
			backgrounds [0].position = backgrounds [2].position;
	}

	void Update () {
		if (scrollSpeed > 0) moveToLeft ();
		if (scrollSpeed < 0) moveToRight ();
	}
}
