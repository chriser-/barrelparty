using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleBarrelRotation : MonoBehaviour {

    public float speed = 20f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Time.deltaTime * speed, 0, 0);
        if (Input.GetKeyDown("s"))
        {
            AudioController.Play("buttonpress");
            SceneManager.LoadScene(1);
        }
    }
}
