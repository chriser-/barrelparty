using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLives : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setLives(float lives)
    {
        GameManager.startLives = (int)lives;
        text.text = "Lives: " + (int)lives;
    }
}
