using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
    public class PlayerHealth : MonoBehaviour
    {
		public float startingHealth = 100;	                            // The amount of health the player starts the game with.
		public float currentHealth;                                   // The current health the player has.
        public Slider healthSlider;                                 // Reference to the UI's health bar.
        public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
        public AudioClip deathClip , hurtClip , healCilp;                                 // The audio clip to play when the player dies.
        public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
		public float restartDelay = 10f;   

		float restartTimer;

        Animator anim;                                              // Reference to the Animator component.
        AudioSource playerAudio;                                    // Reference to the AudioSource component.
        //PlayerMovement playerMovement;                              // Reference to the player's movement.
        //PlayerShooting playerShooting;
	
		ThirdPersonCharacter thirdPersonCharacter;
		//GameObject rightToes;
		//GameObject leftHandItem;
		PlayerPunch playerPunch; 
		PlayerKick playerKick;
		PlayerLevel playerLevel;
		//ParticleSystem hitParticles;  
		bool isDead;                                                // Whether the player is dead.
        bool damaged;                                               // True when the player gets damaged.
		bool punch0 ;
		bool kickLow ;
		bool crouch ;

        void Awake ()
        {
            // Setting up the references.
            anim = GetComponent <Animator> ();
            playerAudio = GetComponent <AudioSource> ();
			//hitParticles = GetComponentInChildren <ParticleSystem> ();
			thirdPersonCharacter = GetComponentInChildren <ThirdPersonCharacter> ();
			playerLevel = GetComponent <PlayerLevel> ();
			playerPunch = GetComponentInChildren <PlayerPunch> ();
			playerKick  = GetComponentInChildren <PlayerKick> ();
            // Set the initial health of the player.
            currentHealth = startingHealth;
        }


        void Update ()
        {
            // If the player has just been damaged...
            if(damaged)
            {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
				//hitParticles.Play();
            }
            // Otherwise...
            else
            {
                // ... transition the colour back to clear.
                damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            // Reset the damaged flag.
            damaged = false;
        }

		public void Recovery(float amount)
		{
	

			if (currentHealth < healthSlider.maxValue) {

				currentHealth += amount;
				healthSlider.value = currentHealth;

				playerAudio.clip = healCilp;
				playerAudio.Play ();
				
				damaged = false;

			} 

			else 
			{
				//currentHealth = healthSlider.maxValue;
				healthSlider.value = healthSlider.maxValue;
				playerAudio.clip = healCilp;
				playerAudio.Play ();
			
				damaged = false;
			}


		}

		public void TakeDamage (float amount)
        {
            // Set the damaged flag so the screen will flash.
            damaged = true;

            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            // Set the health bar's value to the current health.
            healthSlider.value = currentHealth;

            // Play the hurt sound effect.
			playerAudio.clip = hurtClip;
            playerAudio.Play ();
			anim.SetTrigger("Damaged");
	
			
			// .. if it reaches the restart delay...

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if(currentHealth <= 0 && !isDead)
            {
                // ... it should die.
                Death ();
            }
        }


        void Death ()
        {
            // Set the death flag so this function won't be called again.
            isDead = true;
			playerAudio.clip = deathClip;
			playerAudio.Play ();
			anim.SetTrigger ("Die");
			thirdPersonCharacter.enabled = false;
			playerLevel.enabled = false;
			playerPunch.enabled = false;
			playerKick.enabled = false;

			//anim.SetBool ("Punch0", !punch0);
			//anim.SetBool("KickLow", !kickLow);
			//anim.SetBool("Crouch", !crouch);

            
            // Turn off the movement and shooting scripts.
			//thirdPersonCharacter.disableEffect ();

        }


		public void SetMaxHP ()
		{
			currentHealth = healthSlider.maxValue;
		}

        public void RestartLevel ()
        {
            // Reload the level that is currently loaded.
            Application.LoadLevel (Application.loadedLevel);
        }

		public void IncreaseMaxHP ()
		{
			healthSlider.maxValue = healthSlider.maxValue * 1.2f;
		}
    }
}