using System.Collections;
using System.Collections.Generic;
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
    private ItemBase m_CurrentItem;
    public ItemBase CurrentItem
    {
        get { return m_CurrentItem; }
        set { m_CurrentItem = value; }
    }

    private void Start()
    {
        GameManager.Instance.Players.Add(this);
        m_Player = m_UseKeyboardInput ? ReInput.players.GetPlayer(4) : ReInput.players.GetPlayer(GameManager.Instance.Players.Count-1);

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            m_Jump = m_Player.GetButtonDown("Jump");    
        }
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = m_Player.GetAxis("Horizontal");
        float v = m_Player.GetAxis("Vertical");
        //bool crouch = Input.GetKey(KeyCode.C);

        // we use world-relative directions in the case of no main camera
        m_Move = v*Vector3.forward + h*Vector3.right;

        // pass all parameters to the character control script
        m_Character.Move(m_Move, false, m_Jump);
        m_Jump = false;
    }
}
