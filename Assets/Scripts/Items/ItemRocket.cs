using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRocket : ItemBase
{
    protected override IEnumerator useItem()
    {
        ObjectRocket rocket = m_SpawnObject.PlaceObject<ObjectRocket>(m_Player.transform.position);
        rocket.CharController = m_Player;
        rocket.StartRocket();
        Debug.Log(m_Player);
        yield return null;
    }
}
