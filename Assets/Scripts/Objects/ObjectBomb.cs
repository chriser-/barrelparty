using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ObjectBomb : ObjectBase
{

    [SerializeField]
    float m_ExplosionTime = 3f;

    [SerializeField] private GameObject ExplosionParticle;
    [SerializeField] private Material m_BeforeExplosionMat;
    [SerializeField] private Material m_DefaultMaterial;

    private Renderer m_Renderer;

    private IEnumerator m_ChangeMaterialCoroutine;


    void Awake()
    {
        m_Renderer = GetComponent<Renderer>();

        m_ChangeMaterialCoroutine = ChangeMaterialCoroutine();
        StartCoroutine(m_ChangeMaterialCoroutine);

        StartCoroutine(ExplodeCoroutine());
    }

    IEnumerator ExplodeCoroutine()
    {
        float currentTime = 0f;

        while (currentTime < m_ExplosionTime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.01f);

        AudioController.Play("explosion", 1, 0, 0);

        if (ExplosionParticle != null)
            Instantiate(ExplosionParticle, transform.position, Quaternion.identity);

        StopCoroutine(m_ChangeMaterialCoroutine);
        yield return null;

        Destroy(gameObject);
    }

    IEnumerator ChangeMaterialCoroutine()
    {
        float duration = 0.1f;
        float lerp;

        while (true)
        {
            lerp = Mathf.PingPong(Time.time, duration) / duration;
            m_Renderer.materials[1].Lerp(m_DefaultMaterial, m_BeforeExplosionMat, lerp);
            yield return null;
        }
    }
}
