using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGlue : ObjectBase
{
    void Start()
    {
        //Glue has to be parent of barrel
        transform.SetParent(FindObjectOfType<BarrelRotateController>().transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController;
        if ((playerController = other.GetComponent<PlayerController>()) != null)
        {
            //Fix Rigidbody Position
            playerController.Character.Rigidbody.constraints |= RigidbodyConstraints.FreezePosition;
            //Set Player as Parent of barrel
            playerController.transform.SetParent(FindObjectOfType<BarrelRotateController>().transform);
            //Set Rotation to x = 90
            playerController.transform.rotation = Quaternion.Euler(90, 0, 0);
            //restrict input
            
        }
    }
}
