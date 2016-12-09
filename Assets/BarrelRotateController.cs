using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class BarrelRotateController : MonoBehaviour
{

    [SerializeField] private float m_Speed;

    void Awake()
    {
        
    }
	
    void Update()
    {
        transform.Rotate(Time.deltaTime*m_Speed, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        ThirdPersonCharacter character;
        if ((character = collision.gameObject.GetComponent<ThirdPersonCharacter>()) != null)
        {
            character.AddBarrelFriction(m_Speed*Time.deltaTime*transform.lossyScale.x);
        }
    }
}
