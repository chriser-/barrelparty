using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{

    [SerializeField]
    private HeartPlayerController[] m_HeartPlayerControllers;

    void Start()
    {
        for (int i = 0; i < m_HeartPlayerControllers.Length; i++)
        {
            if (i < GameManager.Instance.Players.Count)
            {
                m_HeartPlayerControllers[i].gameObject.SetActive(true);
                m_HeartPlayerControllers[i].InitializeHearts(GameManager.Instance.Players[i]);
            }
            else
            {
                m_HeartPlayerControllers[i].gameObject.SetActive(false);
            }
        }
    }
}
