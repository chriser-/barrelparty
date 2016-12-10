using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class BarrelRotateController : MonoBehaviour
{
    [SerializeField] private float m_Speed;
	
    void Update()
    {
        transform.Rotate(Time.deltaTime*m_Speed, 0, 0);
    }

    private void OnCollisionExit(Collision collision)
    {
        //Fix to make jump feel better
        ThirdPersonCharacter character;
        if ((character = collision.gameObject.GetComponent<ThirdPersonCharacter>()) != null)
        {
            character.AddBarrelFriction(-m_Speed * Time.deltaTime * 100f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ThirdPersonCharacter character;
        if ((character = collision.gameObject.GetComponent<ThirdPersonCharacter>()) != null)
        {
            character.AddBarrelFriction(m_Speed*Time.deltaTime*100f);
        }
    }
}
