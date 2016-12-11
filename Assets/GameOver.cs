using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
		if (GameManager.Players.Count == 1)
        {
            text.text = "You survived " + (int)GameManager.timePlayed + " seconds";
        }
        else
        {
            foreach(PlayerController p in GameManager.Players)
            {
                if (p.Hearts > 0)
                {
                    text.text = "Player " + (p.PlayerId + 1) + "won!";
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
