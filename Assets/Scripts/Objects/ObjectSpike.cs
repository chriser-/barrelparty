using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpike : MonoBehaviour
{

    [SerializeField] private float m_SpikeForce = 10f;

    [SerializeField] private float m_Lifetime = 5f;

    [SerializeField] private float m_MultiplierDamage;

    private BarrelController m_Barrel;

    private void Awake()
    {
        m_Barrel = GameObject.FindGameObjectWithTag("Barrel").GetComponent<BarrelController>();
    }

    private void Start()
    {
        //StartCoroutine(SpawnSpike());
    }

    private void OnCollisionEnter(Collision coll)
    {
        PlayerController character;

        if ((character = coll.gameObject.GetComponent<PlayerController>()) != null)
        {
            character.ForceMultiplier += m_MultiplierDamage*Random.Range(0.5f,1.5f);

            character.AddImpulseForce(transform.up * m_SpikeForce);
        }
    }

    public IEnumerator SpawnSpike()
    {
        float duration = 0.2f;
        float currentTime = 0f;

        Vector3 initPos = transform.position;
        Vector3 endPos = transform.position + transform.up * 1.5f;

        while (currentTime < duration)
        {
            transform.position = Vector3.Slerp(initPos, endPos, currentTime/duration);
            currentTime += Time.fixedDeltaTime;
            yield return Time.fixedDeltaTime;
        }
        transform.parent = m_Barrel.transform;

        yield return new WaitForSeconds(m_Lifetime);
        StartCoroutine(DestroySpike());
    }

    public IEnumerator DestroySpike()
    {
        float duration = 0.2f;
        float currentTime = 0f;

        Vector3 initPos = transform.position;
        Vector3 endPos = transform.position - transform.up * 1.5f;

        while (currentTime < duration)
        {
            transform.localPosition = Vector3.Slerp(initPos, endPos, currentTime / duration);
            currentTime += Time.fixedDeltaTime;
            yield return Time.fixedDeltaTime;
        }

        Destroy(gameObject);
    }
}
