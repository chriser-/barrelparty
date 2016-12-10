using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;


    public class ExplosionCustom : MonoBehaviour
    {
        public float explosionForce = 4;

        [SerializeField] private float m_MultiplierDamage = 0.2f;


        private IEnumerator Start()
        {
            // wait one frame because some explosions instantiate debris which should then
            // be pushed by physics force
            yield return null;

            float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;

            float r = 10 * multiplier;
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
                        character.ForceMultiplier += m_MultiplierDamage;
                        character.AddExplosionForce(explosionForce * multiplier * 0.5f, transform.position, r, 1 * multiplier * 0.05f);
                    }
                    
                }
                
            }

            yield return new WaitForSeconds(3);

            Destroy(gameObject);
        }
    }
