using UnityEngine;
using System.Collections;

public class FromAToB : MonoBehaviour {

    public float StartX = 0;
    public float EndX = 0;

    public bool loop = true;

    public float speed = 1;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < EndX) {
            transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
        } else {
            transform.position = new Vector3(StartX, transform.position.y, transform.position.z);
        }
	}
}
