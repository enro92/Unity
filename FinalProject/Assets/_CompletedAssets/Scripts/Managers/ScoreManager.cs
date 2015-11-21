using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;        // The player's score.
		public Text questCompleteText;
		public string totalScoreText;
		public string nextStage = "Stage1";
		public int totalScore;
		public float restartDelay = 10f;   
		float restartTimer; 
		//GameObject audio;

        Text text;                      // Reference to the Text component.
		//AudioSource questCompleteAudio;  
	

		void Awake ()
        {
            // Set up the reference.
            text = GetComponent <Text> ();
			//audio = GameObject.FindGameObjectWithTag ("QuestAudio");
			//questCompleteAudio = audio.GetComponent <AudioSource> ();
            // Reset the score.
            score = 0;
        }

		void Start ()
		{
			questCompleteText.text = "";
		}

        void Update ()
        {
            // Set the displayed text to be the word "Score" followed by the score value.
			text.text = score + "/" + totalScoreText;

			if (score >= totalScore) 
			{
				questCompleteText.text = "Quest Complete!";
				//questCompleteAudio.Play();
				restartTimer += Time.deltaTime;
				
				if (restartTimer >= restartDelay)
				{
					Application.LoadLevel (nextStage);
				}
       		 }
    	}
	}
}