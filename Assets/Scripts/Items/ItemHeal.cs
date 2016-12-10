using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : ItemBase
{
    [SerializeField] private int m_HPGain;

    public override void UseItem()
    {
        m_Player.HealthPoints += m_HPGain;
    }
}
