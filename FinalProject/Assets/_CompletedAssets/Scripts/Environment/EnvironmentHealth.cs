using UnityEngine;
using System.Collections;

public class EnvironmentHealth : MonoBehaviour {

	ParticleSystem hitParticles;

	void Awake ()
	{
		hitParticles = GetComponentInChildren <ParticleSystem> ();
	}

	public void TakeDamage (Vector3 hitPoint)
	{
		hitParticles.transform.position = hitPoint;
		hitParticles.Play();
	}

	public void TakeDamageNormal ()
	{
		hitParticles.transform.position = transform.position;
		hitParticles.Play();
	}
}
