using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusController : MonoBehaviour
{
    private PlayerController m_Player;
    [SerializeField] private HeartPlayerController m_PlayerHearts;
    [SerializeField] private Text m_MultiplierText;
    [SerializeField] private Image m_SideArrow;
    [SerializeField] private Image m_Panel;
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
	    m_MultiplierText.text = (m_Player.ForceMultiplier*100).ToString("f0") + "%";
        Vector3 point = Camera.main.WorldToScreenPoint(m_Player.transform.position);
	    point.y = Mathf.Clamp(point.y + 50f, 0, Screen.height);
        m_SideArrow.transform.position = new Vector3(m_SideArrow.transform.position.x, point.y, 0);
        m_SideArrow.gameObject.SetActive(point.x < 0);
        if (m_Player.CurrentItem != null) {
            RectTransform CanvasRect = m_Panel.GetComponent<RectTransform>();
            m_Player.CurrentItem.transform.position = Camera.current.ScreenToWorldPoint(m_Panel.transform.position - new Vector3(CanvasRect.rect.width *0.5f, CanvasRect.rect.height *0.5f,-20f));
        }
    }

    public void SetPlayer(PlayerController player)
    {
        m_Player = player;
        m_PlayerHearts.InitializeHearts(m_Player);
    }
}
