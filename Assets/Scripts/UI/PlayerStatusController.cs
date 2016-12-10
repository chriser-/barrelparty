using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusController : MonoBehaviour
{
    private PlayerController m_Player;
    [SerializeField] private HeartPlayerController m_PlayerHearts;
    [SerializeField] private Text m_MultiplierText;
	// Use this for initialization
	void Start ()
    {
	    	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    m_MultiplierText.text = (m_Player.ForceMultiplier*100).ToString("f0") + "%";
	}

    public void SetPlayer(PlayerController player)
    {
        m_Player = player;
        m_PlayerHearts.InitializeHearts(m_Player);
    }
}
