using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
		if (GameManager.Instance.Players.Count == 1)
        {
            text.text = "You survived " + (int)GameManager.Instance.timePlayed + " seconds";
        }
        else
        {
            foreach(PlayerController p in GameManager.Instance.Players)
            {
                if (p.Hearts > 0)
                {
                    text.text = "Player " + (p.PlayerId + 1) + " won!\n You survived " + (int)GameManager.Instance.timePlayed + " seconds";
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
