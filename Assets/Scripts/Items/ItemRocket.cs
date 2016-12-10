using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRocket : ItemBase
{
    protected override IEnumerator useItem()
    {
        ObjectRocket rocket = m_SpawnObject.PlaceObject<ObjectRocket>(m_Player.transform.position + new Vector3(0, m_Player.transform.lossyScale.y, 0));
        rocket.CharController = m_Player;
        rocket.StartRocket();
        m_Player.transform.position -= m_Player.transform.forward * 0.25f;
        yield return null;
    }
}
