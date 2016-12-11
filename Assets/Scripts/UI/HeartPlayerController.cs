using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartPlayerController : MonoBehaviour {

    private List<HeartView> m_Hearts = new List<HeartView>();
    private HeartView m_HeartPrefab;
    private PlayerController m_Player;
	void Awake ()
	{
	    m_HeartPrefab = GetComponentInChildren<HeartView>();
	    m_HeartPrefab.gameObject.SetActive(false);
	}
	
	void Update ()
    {
	    if (m_Player.Hearts != m_Hearts.Count)
	    {
	        if (m_Player.Hearts > m_Hearts.Count)
	        {
	            for (int i = 0; i < m_Player.Hearts - m_Hearts.Count; i++)
	            {
	                addHeart();
	            }
	        }
	    }
	    foreach (var heartView in m_Hearts)
	    {
	        heartView.UpdateHeart(m_Player.Hearts);
	    }
	}

    public void InitializeHearts(PlayerController player)
    {
        m_Player = player;
        m_Player.Hearts = GameManager.startLives;
        for (int i = 0; i < m_Player.Hearts; i++)
        {
            addHeart();
        }
    }

    private void addHeart()
    {
        HeartView newHeart = Instantiate(m_HeartPrefab);
        newHeart.transform.SetParent(transform, false);
        newHeart.gameObject.SetActive(true);
        newHeart.HeartNum = m_Hearts.Count;
        m_Hearts.Add(newHeart);
    }
}
