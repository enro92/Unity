using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
    public class EnemyHealth : MonoBehaviour
    {
        public int startingHealth = 100;            // The amount of health the enemy starts the game with.
        public int currentHealth;                   // The current health the enemy has.
        public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
        public int scoreValue = 10;   
		public float expValue = 10f;
        public AudioClip deathClip;  
		//public Transform damageTransform;
		//public GameObject damagedTextPrefeb;
		//public TextMesh textMesh;


        Animator anim , textAnim;                              // Reference to the animator.
        AudioSource enemyAudio;                     // Reference to the audio source.
        ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
        CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
        bool isDead;                                // Whether the enemy is dead.
        bool isSinking;  
		PlayerLevel playerLevel; 
		GameObject player; 

//
//		void Start ()
//		{
//			
//			//rb = GetComponent<Rigidbody>();
//
//		
//			damageTextMesh.text = "";
//
//		}


        void Awake ()
        {
            // Setting up the references.
            anim = GetComponent <Animator> ();
			//textAnim = GetComponentInChildren <Animator> ();
            enemyAudio = GetComponent <AudioSource> ();
            hitParticles = GetComponentInChildren <ParticleSystem> ();
            capsuleCollider = GetComponent <CapsuleCollider> ();
			//textMesh = GetComponent <TextMesh> ();
			player = GameObject.FindGameObjectWithTag ("Player");
			playerLevel = player.GetComponent <PlayerLevel> ();
			//canvas = GameObject.FindGameObjectWithTag ("EnemyCanvas");
			//textAnim = canvas.GetComponent <Animator> ();
            // Setting the current health when the enemy first spawns.
            currentHealth = startingHealth;
        }


        void Update ()
        {


            // If the enemy should be sinking...
            if(isSinking)
            {
                // ... move the enemy down by the sinkSpeed per second.
                transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }


        public void TakeDamage (int amount, Vector3 hitPoint)
        {
            // If the enemy is dead...
            if (isDead)
			{
				// ... no need to take damage so exit the function.
				return;
			}
            // Play the hurt sound effect.
            enemyAudio.Play ();

            // Reduce the current health by the amount of damage sustained.
            currentHealth -= amount;



            // Set the position of the particle system to where the hit was sustained.
            hitParticles.transform.position = hitPoint;

            // And play the particles.
            hitParticles.Play();


            // If the current health is less than or equal to zero...
            if(currentHealth <= 0)
            {
                // ... the enemy is dead.
                Death ();
            }
        }


		public void TakeDamageByPunch(int amount)
		{
			// If the enemy is dead...
			if (isDead) 
				// ... no need to take damage so exit the function.
				return;

			// Play the hurt sound effect.
			enemyAudio.Play ();
			
			// Reduce the current health by the amount of damage sustained.
			currentHealth -= amount;
			playerLevel.ExpUp(expValue);
			// Set the position of the particle system to where the hit was sustained.
			hitParticles.transform.position = transform.position;


			// And play the particles.
			hitParticles.Play();




			// If the current health is less than or equal to zero...
			if(currentHealth <= 0)
			{
				// ... the enemy is dead.
				Death ();

			}
		}
	


		public void DamageEnv (Vector3 hitPoint)
		{

			// Set the position of the particle system to where the hit was sustained.
			hitParticles.transform.position = hitPoint;
			
			// And play the particles.
			hitParticles.Play();
			

		}

        void Death ()
        {
            // The enemy is dead.
            isDead = true;

            // Turn the collider into a trigger so shots can pass through it.
            capsuleCollider.isTrigger = true;

            // Tell the animator that the enemy is dead.
            anim.SetTrigger ("Dead");

            // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
            enemyAudio.clip = deathClip;
            enemyAudio.Play ();
        }


        public void StartSinking ()
        {
            // Find and disable the Nav Mesh Agent.
            GetComponent <NavMeshAgent> ().enabled = false;

            // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
            GetComponent <Rigidbody> ().isKinematic = true;

            // The enemy should no sink.
            isSinking = true;

            // Increase the score by the enemy's score value.
            ScoreManager.score += scoreValue;


            // After 2 seconds destory the enemy.
            Destroy (gameObject, 2f);
        }

//		void ShowDamagedText(string text)
//		{
//			GameObject temp = Instantiate (damagedTextPrefeb) as GameObject;
//			RectTransform tempRect = temp.GetComponent<RectTransform> ();
//			temp.transform.SetParent (transform.FindChild ("EnemyCanvas"));
//			tempRect.transform.localPosition =  damagedTextPrefeb.transform.localPosition;
//			tempRect.transform.localScale =  damagedTextPrefeb.transform.localScale;
//			tempRect.transform.localRotation=  damagedTextPrefeb.transform.localRotation;
//			//temp.GetComponent<Text> ().text = text;
//			textAnim.SetTrigger ("Hit");
//			Destroy (temp, 2);
//
//		}


		
		
		
	

    }
}