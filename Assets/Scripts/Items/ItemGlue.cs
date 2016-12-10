using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGlue : ItemBase
{
    protected override IEnumerator useItem()
    {
        ObjectGlue glue = m_SpawnObject.PlaceObject<ObjectGlue>(m_Player.transform.position - m_Player.transform.forward * m_Player.transform.lossyScale.y * 1.5f);
        //Ray ray = new Ray(glue.transform.position, Vector3.down);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    //glue.transform.up, hit.normal);
        //}
        yield return null;
    }
}
