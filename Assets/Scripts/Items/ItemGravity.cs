using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGravity : ItemBase
{
    protected override IEnumerator useItem()
    {
        Physics.gravity *= -1;
        yield return  new WaitForSeconds(5f);
        Physics.gravity *= -1;
    }
}
