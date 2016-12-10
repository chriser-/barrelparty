using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrampolin : ItemBase
{

    [SerializeField] private float m_JumpMultiplicator = 1.2f;
    [SerializeField] private float m_Duration = 3f;
    protected override IEnumerator useItem()
    {
        m_Player.Character.JumpPower *= m_JumpMultiplicator;
        yield return new WaitForSeconds(m_Duration);  
        m_Player.Character.ResetJumpPower();
    }
}
