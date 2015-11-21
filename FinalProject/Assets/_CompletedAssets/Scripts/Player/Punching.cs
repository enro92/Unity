using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
	public class Punching : MonoBehaviour
	{
		public int damagePerShot = 20;                  // The damage inflicted by each bullet.
		public float timeBetweenPunch = 0.15f;        // The time between each shot.
		public float range = 100f;                      // The distance the gun can fire.
		
		
		float timer;                                    // A timer to determine when to fire.
		Ray punchRay;                                   // A ray from the gun end forwards.
		RaycastHit punchHit;                            // A raycast hit to get information about what was hit.
		int targetMask;                              // A layer mask so the raycast only hits things on the shootable layer.
		//ParticleSystem punchParticles; 
		LineRenderer punchLine;                           // Reference to the line renderer.
		AudioSource punchAudio;                           // Reference to the audio source.
		//Light gunLight;                                 // Reference to the light component.
		//public Light faceLight;								// Duh
		float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

		Animation anim;
		
		void Awake ()
		{
			// Create a layer mask for the Shootable layer.
			targetMask = LayerMask.GetMask ("Shootable");
			
			// Set up the references.
			//punchParticles = GetComponent<ParticleSystem> ();
			punchLine = GetComponent <LineRenderer> ();
			punchAudio = GetComponent<AudioSource> ();
			//gunLight = GetComponent<Light> ();
			//anim = GetComponent<Animation> ();
			//faceLight = GetComponentInChildren<Light> ();
		}
		
		
		void Update ()
		{
			// Add the time since Update was last called to the timer.
			timer += Time.deltaTime;
			
			#if !MOBILE_INPUT
			// If the Fire1 button is being press and it's time to fire...
			if(Input.GetButton ("Fire1") && timer >= timeBetweenPunch && Time.timeScale != 0)
			{
				// ... shoot the gun.
				//anim.Play ();
				Punch ();
			}

			if(Input.GetButton ("Fire2") && timer >= timeBetweenPunch && Time.timeScale != 0)
			{
				// ... shoot the gun.
				//anim.Play ();
				Punch ();
			}
			#else
			// If there is input on the shoot direction stick and it's time to fire...
			if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
			{
				// ... shoot the gun
				Shoot();
			}


			#endif


			
			// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
			if(timer >= timeBetweenPunch * effectsDisplayTime)
			{
				// ... disable the effects.
				DisableEffects ();
			}
		}
		
		
		public void DisableEffects ()
		{
			// Disable the line renderer and the light.
			punchLine.enabled = false;
			//faceLight.enabled = false;
			//gunLight.enabled = false;
			
		}
		
		
		void Punch ()
		{
			// Reset the timer.
			timer = 0f;
			
			// Play the gun shot audioclip.
			punchAudio.Play ();
			
			// Enable the lights.
			//gunLight.enabled = true;
			//faceLight.enabled = true;
			
			// Stop the particles from playing if they were, then start the particles.
			//punchParticles.Stop ();
			//punchParticles.Play ();
			
			
			// Enable the line renderer and set it's first position to be the end of the gun.
			punchLine.enabled = true;
			//transform.position = new Vector3(0, 0, 0);
			punchLine.SetPosition (0, transform.position);

			// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
			//transform.position = new Vector3(0, 0, 0);
			punchRay.origin = transform.position;
			punchRay.direction = transform.forward;
			
			// Perform the raycast against gameobjects on the shootable layer and if it hits something...
			Debug.DrawRay (punchRay.origin, punchRay.direction*range , Color.blue);
			if (Physics.Raycast (punchRay, out punchHit, range, targetMask)) {
				// Try and find an EnemyHealth script on the gameobject hit.
				EnemyHealth enemyHealth = punchHit.collider.GetComponent <EnemyHealth> ();
				EnvironmentHealth environmentHealth = punchHit.collider.GetComponent <EnvironmentHealth> ();
				
				// If the EnemyHealth component exist...
				if (enemyHealth != null) {
					// ... the enemy should take damage.
					enemyHealth.TakeDamage (damagePerShot, punchHit.point);
				} 
				
				
				if(environmentHealth != null)
				{
					environmentHealth.TakeDamage(punchHit.point);
					
				}
				// Set the second position of the line renderer to the point the raycast hit.
				punchLine.SetPosition (1, punchHit.point);
			}
			else
			{
				// ... set the second position of the line renderer to the fullest extent of the gun's range.
				punchLine.SetPosition (1, punchRay.origin + punchRay.direction * range);
				//punchLine.SetPosition (2, punchRay.origin + punchRay.direction * range);
				//punchLine.SetPosition (3, punchRay.origin + punchRay.direction * range);
			}
		}
		
	}
}