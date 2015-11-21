using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
	public class PlayerCollectItem2 : MonoBehaviour {
		
		//public float speed;
		public Text countText;
		public Text winText;
		
		
		public AudioClip audioCollect;
		private AudioSource audioSource;
		//private Rigidbody rb;
		private int count;
		public float restartDelay = 10f;   
		float restartTimer; 
		GameObject player; 
		PlayerHealth playerHealth; 
		PlayerLevel playerLevel;
		public int healthPoint; 
		public int expPoint; 
		public int currentHealth; 

		void Awake ()
		{
			// Setting up the references.
			player = GameObject.FindGameObjectWithTag ("Player");
			audioSource = GetComponent <AudioSource> ();
			playerHealth = player.GetComponent <PlayerHealth> ();
			playerLevel = player.GetComponent <PlayerLevel> ();
		}

		void Start ()
		{
			
			//rb = GetComponent<Rigidbody>();
			count = 0;
			SetCountStageTwoText ();
			winText.text = "";
			
		}
		
		void SetCountStageOneText ()
		{
			countText.text = count.ToString () + "/" + "10";
			if (count >= 5)
			{
				winText.text = "Quest Complete!";
			}
		}
		
		void SetCountStageTwoText ()
		{
			countText.text = count.ToString () + "/" + "15";
			if (count >= 10)
			{
				winText.text = "Quest Complete!";
			}
		}
		
	
		void OnTriggerEnter(Collider other) 
		{
			if (other.gameObject.CompareTag ("Pick Up"))
			{
				
				audioSource.PlayOneShot(audioCollect);
				other.gameObject.SetActive (false);
				count = count + 1;
				SetCountStageTwoText ();
				
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
		
		
		
		
		void Update()
		{
	
			if (count >= 15) 
			{
				restartTimer += Time.deltaTime;
				
				if (restartTimer >= restartDelay)
				{
					Application.LoadLevel ("Stage4");
				}
				
				
			}
			
		}
	}
	
}