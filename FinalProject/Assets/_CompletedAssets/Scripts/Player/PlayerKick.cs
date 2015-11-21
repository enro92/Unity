using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections;
namespace CompleteProject
{
	public class PlayerKick : MonoBehaviour 
	{
		public float timeBetweenKick = 0.15f; 
		public int damagePerShot = 20;   
		private int totalDamagePerShot;
		//public float timeBetweenPunch = 0.15f;        // The time between each shot.
		//public float range = 100f;                      // The distance the gun can fire.
		BoxCollider boxCollider;
		
		float timer;                                    // A timer to determine when to fire.
		ParticleSystem kickParticle ;
		AudioSource kickAudio;                           // Reference to the audio source.
		//Light gunLight;                                 // Reference to the light component.
		//public Light faceLight;								// Duh
		float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
		Animation anim;
		
		void Awake ()
		{

			kickParticle = GetComponentInChildren<ParticleSystem> ();
			
			kickAudio = GetComponent<AudioSource> ();
			boxCollider = GetComponent<BoxCollider> ();
			//anim = GetComponent<Animation> ();
			
		}
		
		void Update ()
		{
			// Add the time since Update was last called to the timer.
			timer += Time.deltaTime;
			
			#if !MOBILE_INPUT
			// If the Fire1 button is being press and it's time to fire...
			if (Input.GetButton ("Fire2") && !Input.GetButton ("Fire1") && timer >= timeBetweenKick && Time.timeScale != 0) 
			{
				timer = 0f;
				boxCollider.enabled = true;
				kickParticle.Play();
				
				
			}
			#else
			// If there is input on the shoot direction stick and it's time to fire...
			if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
			{
				// ... shoot the gun
				Shoot();
			}
			
			
			#endif
			
			
			if(timer >= timeBetweenKick * effectsDisplayTime)
			{
				boxCollider.enabled = false;
			}
			
			// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
			
		}

		
		public void DisableEffects ()
		{
			boxCollider.enabled = false;
		}
		
		public void IncreaseKickDamage ()
		{
			damagePerShot = damagePerShot + 1;
		}
		
		
		
		void OnTriggerEnter(Collider other) 
		{
			if (other.gameObject.CompareTag ("Enemy"))
			{
				
				kickAudio.Play ();
				EnemyHealth enemyHealth = other.gameObject.GetComponent <EnemyHealth> ();
				EnvironmentHealth environmentHealth = other.gameObject.GetComponent <EnvironmentHealth> ();
				
				
				if (enemyHealth != null) {
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