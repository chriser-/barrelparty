using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    protected abstract IEnumerator useItem();
    protected PlayerController m_Player;
    [SerializeField] protected ObjectBase m_SpawnObject;

    public IEnumerator UseItem()
    {
        yield return StartCoroutine(useItem());
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController;
        if ((playerController = other.gameObject.GetComponent<PlayerController>()) != null)
        {
            m_Player = playerController;
            playerController.CurrentItem = this;
            //Destroy(gameObject);
            Destroy(GetComponent<Renderer>());
            Destroy(GetComponent<MeshFilter>());
            Destroy(GetComponent<Collider>());

            foreach(Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }
    }
}
