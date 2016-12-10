using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackVersion : MonoBehaviour {

    GUIText txt;

	// Use this for initialization
	void Start () {
        txt = gameObject.GetComponent<GUIText>();
        txt.text = "version: " + Application.version;
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
