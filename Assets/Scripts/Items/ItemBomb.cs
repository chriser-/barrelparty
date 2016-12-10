using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBomb : ItemBase
{
    protected override IEnumerator useItem()
    {
        ObjectBomb bomb = m_SpawnObject.PlaceObject<ObjectBomb>(m_Player.transform.position - m_Player.transform.forward * 0.25f); 
        yield return null;
    }
}
