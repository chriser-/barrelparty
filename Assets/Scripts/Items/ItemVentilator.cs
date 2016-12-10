using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVentilator : ItemBase
{
    protected override IEnumerator useItem()
    {
        m_SpawnObject.PlaceObject<ObjectVentilator>(gameObject.transform.position);
        yield return null;
    }
}
