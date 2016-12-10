using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGlue : ItemBase
{
    protected override IEnumerator useItem()
    {
        ObjectGlue glue = m_SpawnObject.PlaceObject<ObjectGlue>(m_Player.transform.position - m_Player.transform.forward * 0.25f);
        yield return null;
    }
}
