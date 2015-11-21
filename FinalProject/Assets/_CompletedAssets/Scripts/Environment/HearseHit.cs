using UnityEngine;
using System.Collections;

public class HearseHit : MonoBehaviour {


	
	
	//Animator anim;                              // Reference to the animator.
	AudioSource envAudio;                     // Reference to the audio source.
	ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
	//CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
	//bool isDead;                                // Whether the enemy is dead.
	//bool isSinking;                             // Whether the enemy has started sinking through the floor.
	
	
	void Awake ()
	{
		// Setting up the references.
		//anim = GetComponent <Animator> ();
		envAudio = GetComponent <AudioSource> ();
		hitParticles = GetComponentInChildren <ParticleSystem> ();
		//capsuleCollider = GetComponent <CapsuleCollider> ();
	
		
		// Setting the current health when the enemy first spawns.
		//currentHealth = startingHealth;
	}

	public void DamageEnv (Vector3 hitPoint)
	{
	
		
		// Play the hurt sound effect.
		envAudio.Play ();
		
	
		
		// Set the position of the particle system to where the hit was sustained.
		hitParticles.transform.position = hitPoint;
		
		// And play the particles.
		hitParticles.Play();
		


	}
}
