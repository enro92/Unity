using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace CompleteProject.Characters.ThirdPerson
//namespace CompleteProject.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
		//public AudioClip audioJump;
		//private AudioSource audioSource;
		private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
		private Animator animator;
		private bool m_punch; 
		private bool m_kickLow;
		bool punch0 ;
		bool kickLow ;
		bool crouch ;
		bool damaged; 
		PlayerLevel playerLevel;
	
		void Awake()
		{
			playerLevel = GetComponent<PlayerLevel>();
		}

		private void Start()
        {
			animator = GetComponent<Animator>();

            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
//			if (!m_punch)
//			{
//				m_punch = Input.GetButtonDown("Fire1");
//				animator.SetBool("Punch0", m_punch);
//			}
//			if (!m_kickLow)
//			{
//				m_punch = Input.GetButtonDown("Fire2");
//				animator.SetBool("KickLow", m_kickLow);
//			}
			punch0 = Input.GetButton ("Fire1");
			//animator.SetBool("Damaged", !damaged);
			animator.SetBool("Punch0", punch0);

			if(playerLevel.currentLevel >=9)
			{
				kickLow = Input.GetButton ("Fire2");
				//animator.SetBool("Damaged", !damaged);
				animator.SetBool("KickLow", kickLow);
				if (Input.GetButton ("Fire1") && Input.GetButton ("Fire2")) 
				{
					animator.SetBool("Punch0", !punch0);
					animator.SetBool("KickLow", !kickLow);
					
				}
			}

		
	

			if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");

            }




        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            crouch = Input.GetKey(KeyCode.C);
		


            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump );
			//m_Character.attack (punch);
            m_Jump = false;
        }



    }
}
