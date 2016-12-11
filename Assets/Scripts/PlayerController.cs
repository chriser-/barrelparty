using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Rewired;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerController : MonoBehaviour
{
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    public ThirdPersonCharacter Character { get { return m_Character; } }
    private Transform m_Cam; // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward; // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump; // the world-relative desired move direction, calculated from the camForward and user input.

    [SerializeField]
    private float m_ForceMultiplier = 0f;
    public float ForceMultiplier
    {
        get { return m_ForceMultiplier; }
        set { m_ForceMultiplier = Mathf.Clamp(value, 0, 3); }
    }

    [SerializeField]
    private int m_Hearts = 5;
    public int Hearts
    {
        get { return m_Hearts; }
        set { m_Hearts = Mathf.Clamp(value, 0, 10); }
    }

    private Player m_Player;
    [SerializeField]
    private int m_PlayerId = 1;
    public int PlayerId
    {
        get { return m_PlayerId; }
        set
        {
            m_PlayerId = value;
            m_Player = ReInput.players.GetPlayer(m_PlayerId);
        }
    }
    [SerializeField] private int m_PlayerNum = 1;
    public int PlayerNum
    {
        get { return m_PlayerNum; }
        set { m_PlayerNum = value; }
    }

    [SerializeField] private bool m_UseKeyboardInput = false;

    [SerializeField]
    private ItemBase m_CurrentItem;
    public ItemBase CurrentItem
    {
        get { return m_CurrentItem; }
        set { m_CurrentItem = value; }
    }

    [SerializeField] private bool m_DisableInput = false;
    public bool DisableInput
    {
        get { return m_DisableInput; }
        set
        {
            m_DisableInput = value;
            m_Character.Move(Vector3.zero, false, false);
        }
    }

    public bool GravityDone
    {
        get { return m_GravityDone; }
        set { m_GravityDone = value; }
    }
    private bool m_GravityDone = true;

    private const float m_OutOfFrustumTimerMax = 1f;
    private float m_OutOfFrustumTimer = m_OutOfFrustumTimerMax;

    private bool m_IsInvincible = false;
    public bool IsInvincible
    {
        get
        {
            return m_IsInvincible;
        }
    }

    [SerializeField] private Renderer m_Renderer;

    private Animator m_animator;
    private bool m_ikActive = false;
    private Transform m_handL;
    private Transform m_handR;

    private void Awake()
    {
        GameManager.Instance.Players.Add(this);
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (m_Player == null && GameManager.Instance.Players.Count == 1)
        {
            m_Player = ReInput.players.GetPlayer(4); //Keyboard Player
        }
    }

    private void Update()
    {
        if (DisableInput)
            return;
        if (!m_Jump)
        {
            m_Jump = m_Player.GetButtonDown("Jump");
        }
        if (m_CurrentItem != null && m_Player.GetButtonDown("UseItem"))
        {
            StartCoroutine(m_CurrentItem.UseItem());
            m_CurrentItem = null;
        }
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (!GeometryUtility.TestPlanesAABB(planes, Character.Capsule.bounds) && !m_IsInvincible)
        {
            m_OutOfFrustumTimer -= Time.fixedDeltaTime;
            if (m_OutOfFrustumTimer <= 0)
            {
                Debug.Log("out of bounds");
                Die();
                m_OutOfFrustumTimer = m_OutOfFrustumTimerMax;
            }
        }
        else
        {
            m_OutOfFrustumTimer = m_OutOfFrustumTimerMax;
        }
        if (DisableInput)
        {
            return;
        }
        // read inputs
        float h = m_Player.GetAxis("Horizontal");
        float v = m_Player.GetAxis("Vertical");
        //bool crouch = Input.GetKey(KeyCode.C);

        // we use world-relative directions in the case of no main camera
        m_Move = v * Vector3.forward + h * Vector3.right;

        // pass all parameters to the character control script
        m_Character.Move(m_Move, false, m_Jump);
        m_Jump = false;

        RaycastHit rayTop, rayBottom;
        if (Physics.Raycast(new Ray(transform.position, Vector3.up), out rayTop, 1000, LayerMask.GetMask("Barrel")) &&
            Physics.Raycast(new Ray(transform.position, Vector3.down), out rayBottom, 1000, LayerMask.GetMask("Barrel")))
        {
            if (Physics.gravity.y > 0)
            {
                Vector3 targetUp = Vector3.Slerp(Vector3.up, Vector3.down,
                    rayBottom.distance/((rayTop.distance + rayBottom.distance)*0.8f));
                transform.rotation = Quaternion.LookRotation(transform.forward, targetUp);
                m_GravityDone = false;
            }
            else if (!m_GravityDone)
            {
                Vector3 targetUp = Vector3.Slerp(Vector3.down, Vector3.up,
                    rayTop.distance/((rayTop.distance + rayBottom.distance)*0.8f));
                transform.rotation = Quaternion.LookRotation(transform.forward, targetUp);
                m_GravityDone = targetUp == Vector3.up;
                if (m_GravityDone)
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            }
        }

    }


    public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode mode = ForceMode.Impulse)
    {
        if (m_IsInvincible)
            return;
        Debug.Log(gameObject.name + " Explosion");
        explosionForce *= 1f + m_ForceMultiplier;
        m_Character.Rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, mode);
    }

    public void AddImpulseForce(Vector3 force)
    {
        if (m_IsInvincible)
            return;
        force *= 1f + m_ForceMultiplier;
        m_Character.Rigidbody.AddForce(force, ForceMode.Impulse);
    }

    public void SetInvincible(float duration)
    {
        StartCoroutine(InvincibleCoroutine(duration));
    }

    public void SetInvincible(bool b)
    {
        m_IsInvincible = b;
    }

    private IEnumerator InvincibleCoroutine(float duration)
    {
        m_IsInvincible = true;
        yield return new WaitForSeconds(duration);
        m_IsInvincible = false;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (m_animator)
        {
            if (m_ikActive)
            {
                if (m_handL != null)
                {
                    m_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    m_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    m_animator.SetIKPosition(AvatarIKGoal.LeftHand, m_handL.position);
                    m_animator.SetIKRotation(AvatarIKGoal.LeftHand, m_handL.rotation);
                }
                if (m_handR != null)
                {
                    m_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    m_animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    m_animator.SetIKPosition(AvatarIKGoal.RightHand, m_handR.position);
                    m_animator.SetIKRotation(AvatarIKGoal.RightHand, m_handR.rotation);
                }
            }
            else
            {
                m_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                m_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                m_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                m_animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }
        }
    }

    public void AttachHands(Transform handL, Transform handR)
    {
        m_handL = handL;
        m_handR = handR;
        m_ikActive = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        m_animator.Play("Airborne");
        transform.position = handL.position - new Vector3(handL.lossyScale.x, 0, 0);
        transform.LookAt(handL);
        transform.Rotate(Vector3.right, 90.0f);
        transform.parent = handL;
        DisableInput = true;
        Collider c = GetComponent<Collider>();
        c.enabled = false;
    }

    public void DetachHands()
    {
        m_ikActive = false;
        m_handL = null;
        m_handR = null;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        transform.Rotate(Vector3.right, -90.0f);
        transform.parent = null;
        DisableInput = false;
        Collider c = GetComponent<Collider>();
        c.enabled = true;
    }

    public void SetControlable(bool controlable)
    {
        //m_Character.Move(Vector3.zero, false, false);
        GetComponent<Rigidbody>().isKinematic = !controlable;
        GetComponent<Collider>().enabled = controlable;
        GetComponents<MonoBehaviour>().All(b => { b.enabled = controlable; return true; });
    }

    public void Die()
    {
        Hearts--;
        ForceMultiplier = 0;
        if (Hearts > 0)
        {
            m_IsInvincible = true;
            StartCoroutine(invincibleSpawnTimer());
            transform.position = Vector3.zero;
            Debug.Log("KABUUM");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator invincibleSpawnTimer()
    {
        yield return new WaitForSeconds(2f);
        m_IsInvincible = false;
    }

    public void SetMaterial(Material m)
    {
        m_Renderer.material = m;
    }
}
