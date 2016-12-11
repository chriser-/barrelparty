﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour {

    [System.Serializable]
    public struct SpawnGroup
    {
        public GameObject SpawnObject;
        public float probability;
        public bool stickToBarrel;
    }


    public SpawnGroup[] spawn;
    public GameObject barrel;
    public Vector2 startZendZ;

	// Use this for initialization
	void Start () {
        Random.InitState((int)System.DateTime.Now.Ticks);
        float result = 0f;
        foreach (SpawnGroup s in spawn)
        {
            result += s.probability;
        }
        for (int i = 0; i < spawn.Length; i++)
        {
            spawn[i].probability /= result;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Random.InitState((int)System.DateTime.Now.Ticks);
        if (Random.Range(0f, 1f) > 0.985f)
        {
            float r = Random.Range(0f, 1f);

            float start = 0f;

            foreach (SpawnGroup s in spawn)
            {
                if (start <= r && r <= s.probability)
                {
                    SpawnObject(s);
                }
                else
                {
                    start += s.probability;
                }
            }
        }
	}

    void SpawnObject(SpawnGroup g)
    {
        if (g.stickToBarrel)
        {
            
            Instantiate(g.SpawnObject, new Vector3(transform.position.x, Random.Range(transform.position.y - startZendZ.x, transform.position.y + startZendZ.y), Random.Range(transform.position.z - startZendZ.x, transform.position.z + startZendZ.y)), g.SpawnObject.transform.rotation, barrel.transform);

        }
        else
        {
            Instantiate(g.SpawnObject, new Vector3(transform.position.x, Random.Range(transform.position.y - startZendZ.x, transform.position.y + startZendZ.y), Random.Range(transform.position.z - startZendZ.x, transform.position.z + startZendZ.y)), g.SpawnObject.transform.rotation);
        }
    }
}
