using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingBackground : MonoBehaviour
{

    public float parallaxSpeedIndex = 1f;
    public float distanceToTravel = 20.0f;
    public float distanceOffset = 50.0f;
    public BarrelController barrelController;

    private float getSpeed()
    {
        return (parallaxSpeedIndex*-barrelController.Speed)/Mathf.PI;
    }

    void Update()
    {

        float amtToMove = getSpeed()*Time.deltaTime;
        transform.Translate(Vector3.right*amtToMove, Space.World);

        if (transform.position.x < -distanceToTravel*transform.lossyScale.x)
        {
            transform.position = new Vector3(distanceToTravel*transform.lossyScale.x + distanceOffset,
                transform.position.y, transform.position.z);
        }
        if (transform.position.x > distanceToTravel*transform.lossyScale.x)
        {
            transform.position = new Vector3(-distanceToTravel*transform.lossyScale.x + distanceOffset,
                transform.position.y, transform.position.z);
        }
    }
}
