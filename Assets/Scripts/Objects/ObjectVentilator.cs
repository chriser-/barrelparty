using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVentilator : ObjectBase
{
    public float m_strengthMax = 100.0f;
    public float m_duration = 5.0f;

    private void Start()
    {
        WindzoneController windController = GetComponentInChildren<WindzoneController>();
        windController.strengthMax = m_strengthMax;
        Destroy(windController, m_duration);
    }
}
