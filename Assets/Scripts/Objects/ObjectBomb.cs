using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ObjectBomb : ObjectBase
{

    [SerializeField]
    float m_ExplosionTime = 3f;

    [SerializeField] private Material m_BeforeExplosionMat;
    [SerializeField] private Material m_DefaultMaterial;

    private Renderer m_Renderer;

    private SphereCollider m_TriggerCollider;

    private GameObject m_ExplosionChild;

    private IEnumerator m_ChangeMaterialCoroutine;


    void Awake()
    {
        m_TriggerCollider = GetComponents<SphereCollider>().First(c => c.isTrigger);
        m_TriggerCollider.enabled = false;

        m_Renderer = GetComponent<Renderer>();

        m_ChangeMaterialCoroutine = ChangeMaterialCoroutine();
        StartCoroutine(m_ChangeMaterialCoroutine);

        StartCoroutine(ExplodeCoroutine());

        m_ExplosionChild = transform.GetChild(0).gameObject;
    }

    IEnumerator ExplodeCoroutine()
    {
        float currentTime = 0f;

        while (currentTime < m_ExplosionTime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        m_TriggerCollider.enabled = true;

        yield return new WaitForSeconds(0.01f);

        if(m_ExplosionChild != null)
            m_ExplosionChild.SetActive(true);

        AudioController.Play("explosion", 1, 0, 0);

        StopCoroutine(m_ChangeMaterialCoroutine);

        yield return new WaitForSeconds(2f);   

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
