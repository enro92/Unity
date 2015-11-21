using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace CompleteProject
{
	public class PlayerCollectItem : MonoBehaviour
	{
	
		public int scoreValue = 1;
		public int healthPoint; 
		public int expPoint; 
		//public int currentHealth; 
		public static int score;
		public AudioClip audioCollect;
		private AudioSource audioSource;
		private int count;
		//public float restartDelay = 10f;   
		
		float restartTimer; 
		GameObject player; 
		PlayerHealth playerHealth; 
		PlayerLevel playerLevel;

		void Awake ()
		{
			// Setting up the references.
			player = GameObject.FindGameObjectWithTag ("Player");
			audioSource = GetComponent <AudioSource> ();
			playerHealth = player.GetComponent <PlayerHealth> ();
			playerLevel = player.GetComponent <PlayerLevel> ();
		}
		
		void OnTriggerEnter(Collider other) 
		{
			if (other.gameObject.CompareTag ("Pick Up"))
			{
				
				audioSource.PlayOneShot(audioCollect);
				other.gameObject.SetActive (false);
				ScoreManager.score += scoreValue;
				
			}
			
			else if (other.gameObject.CompareTag ("HpItem"))
			{
				audioSource.PlayOneShot (audioCollect);
				other.gameObject.SetActive (false);
				playerHealth.Recovery(healthPoint);
				
				
			}
			
			else if (other.gameObject.CompareTag ("ExpItem"))
			{
				audioSource.PlayOneShot (audioCollect);
				other.gameObject.SetActive (false);
				playerLevel.ExpUpItem(expPoint);
			}
			
		}

	}
	
}