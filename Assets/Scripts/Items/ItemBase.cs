using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public abstract IEnumerator UseItem();
    protected PlayerController m_Player;
    [SerializeField] protected ObjectBase m_SpawnObject;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController;
        if ((playerController = other.gameObject.GetComponent<PlayerController>()) != null)
        {
            m_Player = playerController;
            playerController.CurrentItem = UseItem;
            Destroy(gameObject);
        }
    }
}
