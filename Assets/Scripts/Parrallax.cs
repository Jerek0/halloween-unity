using UnityEngine;
using System.Collections;

public class Parrallax : MonoBehaviour {

    public GameObject relativeTo;
    public float speedFactor;

    Vector3 initialPos;

	// Use this for initialization
	void Start () {
        initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {        
	    transform.position = new Vector3(initialPos.x + (relativeTo.transform.position.x * speedFactor), initialPos.y + (relativeTo.transform.position.y * speedFactor), transform.position.z);
	}
}
