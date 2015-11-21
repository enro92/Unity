using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	public int x , y , z ;

	void Update () 
	{
		transform.Rotate (new Vector3 (x,y, z) * Time.deltaTime);




	}
}
