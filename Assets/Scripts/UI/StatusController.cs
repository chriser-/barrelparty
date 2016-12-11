using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{

    [SerializeField]
    private PlayerStatusController[] m_PlayerStatusControllers;

    void Start()
    {
        for (int i = 0; i < m_PlayerStatusControllers.Length; i++)
        {
            if (i < GameManager.Instance.Players.Count)
            {
                m_PlayerStatusControllers[i].gameObject.SetActive(true);
                m_PlayerStatusControllers[i].SetPlayer(GameManager.Instance.Players[i]);
            }
            else
            {
                m_PlayerStatusControllers[i].gameObject.SetActive(false);
            }
        }
    }
}
