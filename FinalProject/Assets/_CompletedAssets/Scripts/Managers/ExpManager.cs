using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
	public class ExpManager : MonoBehaviour
	{
		public static float exp;        // The player's score.
		//GameObject player; 
		//PlayerLevel playerLevel;
		Slider expSlider;                      // Reference to the Text component.
		
		
		void Awake ()
		{
			// Set up the reference.
			//player = GameObject.FindGameObjectWithTag ("Player");
			expSlider = GetComponent <Slider> ();
			//playerLevel = player.GetComponent <PlayerLevel> ();
			// Reset the score.
			//exp = 0f;
		}
		
		
		void Update ()
		{
			// Set the displayed text to be the word "Score" followed by the score value.
			expSlider.value = exp;
		}
	}
}