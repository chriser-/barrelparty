using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectBase : MonoBehaviour
{
    public ObjectBase PlaceObject(Vector3 position)
    {
        ObjectBase newObjectBase = Instantiate(this);
        newObjectBase.transform.position = position;
        return newObjectBase;
    }
}
