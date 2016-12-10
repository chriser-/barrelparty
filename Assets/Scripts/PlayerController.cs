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
    private int m_HealthPoints = 100;
    public int HealthPoints
    {
        get { return m_HealthPoints; }
        set { m_HealthPoints = Mathf.Clamp(value, 0, 100); }
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

    private void Start()
    {
        GameManager.Instance.Players.Add(this);
        m_Player = m_UseKeyboardInput ? ReInput.players.GetPlayer(4) : ReInput.players.GetPlayer(GameManager.Instance.Players.Count-1);

        m_Barrel = FindObjectOfType<BarrelRotateController>();
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
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
        m_Move = v*Vector3.forward + h*Vector3.right;

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
            else if(!m_GravityDone)
            {
                Vector3 targetUp = Vector3.Slerp(Vector3.down, Vector3.up,
                    rayTop.distance / ((rayTop.distance + rayBottom.distance) * 0.8f));
                transform.rotation = Quaternion.LookRotation(transform.forward, targetUp);
                m_GravityDone = targetUp == Vector3.up;
                if(m_GravityDone)
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            }

        }
        
    }
}
