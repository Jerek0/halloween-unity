using UnityEngine;
using System.Collections;

public class GameObjectActiver : MonoBehaviour {

    public GameObject gameObject;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Player>()) {                                    
            gameObject.SetActive(true);
        }
    }
}
