using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections;
namespace CompleteProject
{
public class PlayerPunch : MonoBehaviour 
	{
		public float timeBetweenPunch = 0.15f; 
		public int damagePerShot = 20;
		private int totalDamagePerShot;

		//public float range = 100f;                      // The distance the gun can fire.
		BoxCollider boxCollider;

		float timer;                                    // A timer to determine when to fire.
	
		AudioSource punchAudio;                           // Reference to the audio source.
		//Light gunLight;                                 // Reference to the light component.
		//public Light faceLight;								// Duh
		float effectsDisplayTime = 0.1f;                // The proportion of the timeBetweenBullets that the effects will display for.
		Animation anim;
	

	
		


		void Awake ()
		{
			//punchParticles = GetComponent<ParticleSystem> ();
			punchAudio = GetComponent<AudioSource> ();
			boxCollider = GetComponent<BoxCollider> ();
			//anim = GetComponent<Animation> ();

		
		}

		void Update ()
		{
			// Add the time since Update was last called to the timer.
			timer += Time.deltaTime;
			
			#if !MOBILE_INPUT

		 
				// If the Fire1 button is being press and it's time to fire...
			if (Input.GetButton ("Fire1") && !Input.GetButton ("Fire2") && timer >= timeBetweenPunch && Time.timeScale != 0)
			{
					timer = 0f;
					boxCollider.enabled = true;

			}
		
				#else
			// If there is input on the shoot direction stick and it's time to fire...
			if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
			{
				// ... shoot the gun
				Shoot();
			}
			
			
				#endif

			
				if (timer >= timeBetweenPunch * effectsDisplayTime) {
					DisableEffects ();
				}
			
				// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
	
		}


		
		public void DisableEffects ()
		{
			boxCollider.enabled = false;
		}

		public void IncreasePunchDamage ()
		{
			damagePerShot = damagePerShot + 1;
		}

		void OnTriggerEnter(Collider other) 
		{
			if (other.gameObject.CompareTag ("Enemy"))
			{

				punchAudio.Play ();
				EnemyHealth enemyHealth = other.gameObject.GetComponent <EnemyHealth> ();
				EnvironmentHealth environmentHealth = other.gameObject.GetComponent <EnvironmentHealth> ();
			

				if (enemyHealth != null) {
					// ... the enemy should take damage.
					totalDamagePerShot = damagePerShot;
					enemyHealth.TakeDamageByPunch(totalDamagePerShot);
				} 
			
			else if (other.gameObject.CompareTag ("Environment"))
				{

				
				if(environmentHealth != null)
				{
					environmentHealth.TakeDamageNormal();
					
				}
			}
			
		}
	}
}
		
		
	
}