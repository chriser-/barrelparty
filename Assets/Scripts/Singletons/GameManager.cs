using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private List<PlayerController> m_Players = new List<PlayerController>();

    public List<PlayerController> Players
    {
        get { return m_Players; }
    }

    void Update()
    {
        /*
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                Debug.Log("KeyCode down: " + kcode);
        }
        for (int i = 1; i < 29; i++)
        {
            string axis = "Joy1Axis" + i;
            if (Mathf.Abs(Input.GetAxis(axis)) > 0.01f)
            {
                Debug.Log(axis + ": " + Input.GetAxis(axis));
            }
        }

        for (int i = 0; i < 127; i++)
        {
            foreach (var controller in ReInput.controllers.Controllers)
            {
                if (controller.GetButtonDown(i))
                {
                    Debug.Log("Button " + i);
                }
            }
        }
        */
    }

}
