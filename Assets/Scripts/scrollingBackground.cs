using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingBackground : MonoBehaviour {

	// Transformation of 3 Sprites
	public BarrelRotateController barrelController;

	private Transform[] backgrounds;
	private float scrollSpeed;

	void Start () {
		backgrounds = new Transform[3];   // 3 planes/walls 
		backgrounds[0] = transform.GetChild(0);
		backgrounds[1] = transform.GetChild(1);
		backgrounds[2] = transform.GetChild(2);
	}

	void Update () {
		scrollSpeed = - barrelController.getSpeed ();

		if (scrollSpeed > 0) moveToLeft ();
		if (scrollSpeed < 0) moveToRight ();
	}

	private float getSpeed (){
		return scrollSpeed;
	}

	private void moveToLeft(){
		Vector3 positionTmp = backgrounds [0].position;
		float amtToMove = getSpeed() * Time.deltaTime;
		transform.Translate(Vector3.right * amtToMove, Space.World);
		if (backgrounds [2].position.x <= positionTmp.x){
			backgrounds[0].position = backgrounds[1].position;
			backgrounds[1].position = backgrounds[2].position;
			backgrounds [2].position = positionTmp;
		}
	}

	private void moveToRight(){
		Vector3 positionTmp = backgrounds [2].position;
		float amtToMove = getSpeed() * Time.deltaTime;
		transform.Translate(Vector3.right * amtToMove, Space.World);
		if (backgrounds [0].position.x >= positionTmp.x) {
			backgrounds[2].position = backgrounds[1].position;
			backgrounds[1].position = backgrounds[0].position;
			backgrounds [0].position = positionTmp;
		}
	}


}
