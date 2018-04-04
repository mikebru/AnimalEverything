using UnityEngine;
using System.Collections;

public class PlanetControl : MonoBehaviour {

	public Rigidbody[] planets;
	public string PlanetTag;

	// Use this for initialization
	void Start () {

		if (PlanetTag == "") {
			planets = GameObject.FindObjectsOfType<Rigidbody> ();
		} 

		else {
			GameObject[] tempplanets = GameObject.FindGameObjectsWithTag(PlanetTag);

			planets = new Rigidbody[tempplanets.Length];

			for (int i = 0; i < planets.Length; i++) {
				planets [i] = tempplanets [i].GetComponent<Rigidbody> ();
			}

		}

		Debug.Log (planets.Length);
	
	}

}
