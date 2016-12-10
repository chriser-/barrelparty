using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ObjectRocket : ObjectBase
{
    public float m_speed = 1.0f;
    public float m_duration = 3.0f;

    
    public PlayerController CharController;

    [SerializeField] private GameObject ExplosionPrefab;
    private bool m_isActive = false;
    private IEnumerator destroyCoroutine;

    void Awake()
    {
        destroyCoroutine = destroyRocket();
    }

    void Update()
    {
        if (!m_isActive)
            return;

        transform.RotateAround(Vector3.zero, Vector3.forward, m_speed);
    }

    private void OnDestroy()
    {
        if(CharController != null)
            CharController.DetachHands();
    }

    public void StartRocket()
    {
        if (CharController != null)
        {
            CharController.AttachHands(transform, transform);
            m_isActive = true;
            StartCoroutine(destroyCoroutine);
            CharController.SetInvincible(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerController otherController;
        if ((otherController = collision.gameObject.GetComponent<PlayerController>()) != null)
        {
            otherController.ForceMultiplier += 0.2f;
            CharController.SetInvincible(0.5f);
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            AudioController.Play("explosion", 1, 0, 0);
            if (destroyCoroutine != null)
                StopCoroutine(destroyCoroutine);
            CharController.DetachHands();
            CharController.GravityDone = false;
            Destroy(gameObject);
        }
    }

    

    private IEnumerator destroyRocket()
    {
        yield return new WaitForSeconds(m_duration);
        CharController.DetachHands();
        CharController.GravityDone = false;
        Destroy(gameObject);
        CharController.SetInvincible(false);
    }
}
