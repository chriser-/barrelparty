using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectBase : MonoBehaviour
{
    public T PlaceObject<T>(Vector3 position) where T : ObjectBase
    {
        ObjectBase newObjectBase = Instantiate(this);
        newObjectBase.transform.position = position;
        return (T)newObjectBase;
    }
}
