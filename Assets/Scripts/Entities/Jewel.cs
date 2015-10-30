using UnityEngine;
using System.Collections;

public class Jewel : MonoBehaviour {

    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Player") {            
            ScoreManager.score++;           
            other.GetComponent<Player>().Collect();
            Destroy(this.gameObject);
        }
    }
}
