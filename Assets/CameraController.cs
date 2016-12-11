using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float m_Multiplier = 2f;

    private Vector3 initPosition;
    private Vector3 gravityReversedPosition;

    private Quaternion initRotation;
    private Quaternion gravityReversedRotation;

    private float m_InitSize = 5f;
    [SerializeField] private float m_GravityChangeSize = 10f;

    private float m_InitDistance;
    private float m_CurrentDistance;

    private float m_Lerp;

    private Animator m_Animator;

    void Awake()
    {
        initPosition = new Vector3(0f, 4f, -20f);
        gravityReversedPosition = new Vector3(0f, -4f, -20f);

        initRotation = Quaternion.Euler(new Vector3(40f, 0f, 0f));
        gravityReversedRotation = Quaternion.Euler(new Vector3(-40f, 0f, 0f));

        transform.position = initPosition;
        transform.rotation = initRotation;

        m_InitDistance = (initPosition - gravityReversedPosition).sqrMagnitude;

        m_Animator = GetComponent<Animator>();
        //m_InitDistance /= 2f;
    }

    void Update()
    {
        if (Physics.gravity.y > 0)
        {
            if ((m_CurrentDistance = (gameObject.transform.position - gravityReversedPosition).sqrMagnitude) >= 0.01f)
            {
                gameObject.transform.position = Vector3.Slerp(gameObject.transform.position, gravityReversedPosition,
                    Time.deltaTime* m_Multiplier);
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, gravityReversedRotation,
                    Time.deltaTime * m_Multiplier);
                //m_Lerp = Mathf.PingPong(Time.time, m_GravityChangeSize);
                m_Lerp = Mathf.PingPong(m_CurrentDistance, m_InitDistance);
                //Camera.main.orthographicSize = Mathf.Lerp(m_InitSize, m_GravityChangeSize, m_Lerp);
            }
            else
            {
                gameObject.transform.position = gravityReversedPosition;
                gameObject.transform.rotation = gravityReversedRotation;
            }
        }
        else
        {
            if ((m_CurrentDistance = (gameObject.transform.position - initPosition).sqrMagnitude) >= 0.01f)
            {
                gameObject.transform.position = Vector3.Slerp(gameObject.transform.position, initPosition,
                    Time.deltaTime * m_Multiplier);
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, initRotation,
                    Time.deltaTime * m_Multiplier);

                m_Lerp = Mathf.PingPong(m_CurrentDistance, m_InitDistance);
                //Camera.main.orthographicSize = Mathf.Lerp(m_InitSize, m_GravityChangeSize, m_Lerp);
            }
            else
            {
                gameObject.transform.position = initPosition;
                gameObject.transform.rotation = initRotation;
            }
        }

        m_Animator.SetBool("gravityOn", Physics.gravity.y > 0);

    }

}
