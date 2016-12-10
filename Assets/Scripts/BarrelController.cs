using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class BarrelController : MonoBehaviour
{
    [SerializeField] private float m_Speed;

    void Awake()
    {
        
    }
	
    void Update()
    {
        transform.Rotate(Time.deltaTime*m_Speed, 0, 0);
    }

    private void OnCollisionExit(Collision collision)
    {
        //Fix to make jump feel better
        PlayerController player;
        if ((player = collision.gameObject.GetComponent<PlayerController>()) != null)
        {
            player.Character.AddBarrelFriction(-m_Speed * Time.deltaTime * 100f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        PlayerController player;
        if ((player = collision.gameObject.GetComponent<PlayerController>()) != null)
        {
            player.Character.AddBarrelFriction(m_Speed*Time.deltaTime*100f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController player;
        if ((player = other.gameObject.GetComponent<PlayerController>()) != null)
        {
            player.Hearts--;
            if (player.Hearts > 0)
            {
                player.transform.position = Vector3.zero;
            }
            else
            {
                player.gameObject.SetActive(false);
            }
        }
    }
}
