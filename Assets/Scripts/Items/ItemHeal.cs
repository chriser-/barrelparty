using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : ItemBase
{
    [SerializeField] private float m_HPGain;

    protected override IEnumerator useItem()
    {
        m_Player.ForceMultiplier -= m_HPGain;
        yield return null;
    }
}
