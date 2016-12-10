using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectGlue : ObjectBase
{
    [SerializeField] private float m_MultiplierDamage = 0.2f;
    private List<PlayerController> m_Players = new List<PlayerController>();

    void Start()
    {
        //Glue has to be parent of barrel
        transform.SetParent(FindObjectOfType<BarrelController>().transform);
        //disable glue after a while
        StartCoroutine(disableGlue());
    }

    /*
    void Update()
    {
        //destroy gameobject when glue isnt active and no players are inside
        if (!m_GlueActive && m_Players.Count == 0)
        {
            Destroy(gameObject);
        }
        foreach (var player in m_Players.Keys.ToList())
        {
            m_Players[player] -= Time.deltaTime;
            if (m_Players[player] <= 0f)
            {
                removeGlue(player);
            }
        }
        foreach (var player in m_Players.Where(p => p.Value <= 0f))
        {
            m_Players.Remove(player.Key);
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController;
        if ((playerController = other.GetComponent<PlayerController>()) != null)
        {
            m_Players.Add(playerController);
            //Fix Rigidbody Position
            playerController.Character.Rigidbody.constraints |= RigidbodyConstraints.FreezePosition;
            //Set Player as Parent of barrel
            playerController.transform.SetParent(FindObjectOfType<BarrelController>().transform);
            //restrict input
            playerController.DisableInput = true;
            
        }
    }

    private IEnumerator disableGlue()
    {
        yield return new WaitForSeconds(10f);
        m_Players.ForEach(removeGlue);
        Destroy(gameObject);

    }

    private void removeGlue(PlayerController playerController)
    {
        //unfix Rigidbody Position
        playerController.Character.Rigidbody.constraints &= ~RigidbodyConstraints.FreezePosition;
        //remove parent
        playerController.transform.SetParent(null);
        //allow input
        playerController.DisableInput = false;

        if(playerController.transform.position.y >= 0f)
            playerController.ForceMultiplier += m_MultiplierDamage;

        playerController.GravityDone = false;      
    }
}
