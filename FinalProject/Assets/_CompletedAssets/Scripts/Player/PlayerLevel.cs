using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
	public class PlayerLevel : MonoBehaviour {
		public static float exp;  
		public float startingLevel = 0f;                           
		public float currentLevel;
		public float startingExp = 0f;                           
		public float currentExp;
		public Slider expSlider;
		public Text levelText;
		public AudioClip levelUpClip;
		public Image skillImage;
		private int count;



		Animator animator;  

		// Reference to the Animator component.
		AudioSource playerAudio;  
		//PlayerMovement playerMovement;  
		PlayerHealth playerHealth;
		PlayerKick playerKick;
		PlayerPunch playerPunch; 
//		ThirdPersonCharacter thirdPersonCharacter;
		//int groundedStateHash = Animator.StringToHash("Base Layer.Grounded");

		//ParticleSystem levelUpParticles;  
	
		bool isDead;                                                // Whether the player is dead.
		bool damaged;   

		// True when the player gets damaged

	

		void Awake ()
		{
			// Setting up the references.
			//animator = GetComponent <Animator> ();

			playerAudio = GetComponent <AudioSource> ();
			playerHealth = GetComponent <PlayerHealth> ();
			//playerKick = GetComponent <PlayerMovement> ();
			//thirdPersonCharacter = GetComponent <ThirdPersonCharacter> ();
			playerPunch = GetComponentInChildren<PlayerPunch> ();
			playerKick  = GetComponentInChildren <PlayerKick> ();
			//levelUpParticles = GetComponentInChildren <ParticleSystem> ();
			// Set the initial health of the player.
			currentLevel = startingLevel;
			expSlider.minValue = startingExp;
			currentExp = expSlider.minValue;
		}

		void Start ()
		{
			count = 1;
			SetLevelText();
	
		}
	
		void Update ()
		{
			// If the player has just been damaged...
			if(currentExp >= expSlider.maxValue)
			{
			
				LevelUp();
				if(currentLevel >= 9)
				{
					Level10();
				}
			}


			
		
			damaged = false;
		}

		public void ExpUp(float amount)
		{
			currentExp += amount;
			expSlider.value = currentExp;
			damaged = false;
		}

		public void ExpUpItem (float amount)
		{
		
			currentExp += amount;
			expSlider.value = currentExp;
			damaged = false;

		}

		public void ExpDown(float amount)
		{
			currentExp -= amount;
			expSlider.value = currentExp;
		}

		public void LevelUp()
		{

			playerAudio.PlayOneShot(levelUpClip);
			//levelUpParticles.Play();
			//animator.SetBool("LevelUp", true);

			expSlider.maxValue = expSlider.maxValue * 1.2f;
			currentExp = expSlider.minValue;	
			playerHealth.IncreaseMaxHP();
			playerHealth.SetMaxHP();
			count = count + 1;
			SetLevelText ();
			currentLevel += 1;
			playerPunch.IncreasePunchDamage ();
			playerKick.IncreaseKickDamage ();
		
		}
		public void Level10()
		{
			playerKick.enabled = true;
			skillImage.color = Color.white;
		}


		void SetLevelText ()
		{
			levelText.text = "Level " + count.ToString ();

		}

	}
}