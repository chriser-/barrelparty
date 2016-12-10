using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVentilator : ItemBase
{
    protected override IEnumerator useItem()
    {
        ObjectVentilator ventilator = m_SpawnObject.PlaceObject<ObjectVentilator>(m_Player.transform.position);
        ventilator.transform.Rotate(Vector3.up, 90.0f);
        ventilator.transform.position += new Vector3(0, m_Player.transform.localScale.y, 0) - m_Player.transform.forward * 0.25f;
        ventilator.gameObject.transform.SetParent(m_Player.transform);
        yield return null;
    }
}
