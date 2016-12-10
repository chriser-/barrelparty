using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaufbandController : MonoBehaviour
{
    [Range(0.0f, 0.03f)]
    public float m_speedTexture = 0.02f;
    public GameObject m_textureMat;

    private void Start()
    {
        Renderer rend = m_textureMat.GetComponent<Renderer>();
        Texture tex = rend.material.GetTexture("_MainTex");
    }

    void FixedUpdate()
    {
        Renderer rend = m_textureMat.GetComponent<Renderer>();
        Vector2 offset = rend.material.GetTextureOffset("_MainTex");
        offset.x += m_speedTexture;
        rend.material.SetTextureOffset("_MainTex", offset);
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(gameObject.transform.right * 8000.0f * m_speedTexture);
        }
    }
}
