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

    private Player m_Player;
    [SerializeField] private bool m_UseKeyboardInput = false;

    [SerializeField]
    private Func<IEnumerator> m_CurrentItem;
    public Func<IEnumerator> CurrentItem
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

    private BarrelRotateController m_Barrel;
    private bool m_GravityDone = true;

    private Animator m_animator;
    private bool m_ikActive = false;
    private Transform m_handL;
    private Transform m_handR;

    private void Start()
    {
        GameManager.Instance.Players.Add(this);
        m_Player = m_UseKeyboardInput ? ReInput.players.GetPlayer(4) : ReInput.players.GetPlayer(GameManager.Instance.Players.Count - 1);

        m_Barrel = FindObjectOfType<BarrelRotateController>();
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();

        m_animator = GetComponent<Animator>();
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
            StartCoroutine(m_CurrentItem());
            //m_CurrentItem = null;
        }
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
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
                    rayBottom.distance / ((rayTop.distance + rayBottom.distance) * 0.8f));
                transform.rotation = Quaternion.LookRotation(transform.forward, targetUp);
                m_GravityDone = false;
            }
            else if (!m_GravityDone)
            {
                Vector3 targetUp = Vector3.Slerp(Vector3.down, Vector3.up,
                    rayTop.distance / ((rayTop.distance + rayBottom.distance) * 0.8f));
                transform.rotation = Quaternion.LookRotation(transform.forward, targetUp);
                m_GravityDone = targetUp == Vector3.up;
                if (m_GravityDone)
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            }

        }

    }


    public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode mode = ForceMode.Impulse)
    {
        explosionForce *= 1f + m_ForceMultiplier;
        m_Character.Rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, mode);
    }

    public void AddImpulseForce(Vector3 force)
    {
        force *= 1f + m_ForceMultiplier;
        m_Character.Rigidbody.AddForce(force, ForceMode.Impulse);
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
}
