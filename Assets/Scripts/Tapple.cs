using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapple : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}


	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Player") {
			GetComponent<Rigidbody> ().isKinematic = false;

		}

	}

}
