using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;


    public class ExplosionCustom : MonoBehaviour
    {
        public float explosionForce = 4;

        [SerializeField] private float m_Multiplier;
        [SerializeField] private float m_MultiplierDamage = 0.2f;


        private IEnumerator Start()
        {
            // wait one frame because some explosions instantiate debris which should then
            // be pushed by physics force
            yield return null;
            
            float r = m_Multiplier * 10f;
            var cols = Physics.OverlapSphere(transform.position, r);
            var rigidbodies = new List<Rigidbody>();
            foreach (var col in cols)
            {
                if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
                {
                    rigidbodies.Add(col.attachedRigidbody);
                    
                }
            }
            foreach (var rb in rigidbodies)
            {
                PlayerController character = rb.GetComponent<PlayerController>();
                if (character != null)
                {
                    if (!character.IsInvincible)
                    {
                        character.ForceMultiplier += m_MultiplierDamage * UnityEngine.Random.Range(0.5f, 1.5f);
                        character.AddExplosionForce(explosionForce * m_Multiplier * 0.5f, transform.position, r, 1 * m_Multiplier * 0.05f);
                    }
                    
                }
                
            }

            yield return new WaitForSeconds(3);

            Destroy(gameObject);
        }
    }
