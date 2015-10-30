using UnityEngine;
using System.Collections;

public class GroundedTrigger : MonoBehaviour {

    public GameObject gameObject;

    ArrayList currentlyColliding; 

	// Use this for initialization
	void Start () {
        currentlyColliding = new ArrayList();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        if(!other.gameObject.GetComponent<Ennemy>() && !other.gameObject.GetComponent<Jewel>()) {
            currentlyColliding.Add(other.gameObject);
            gameObject.SendMessage("OnGroundTouch");
        }       
    }

    void OnTriggerExit2D(Collider2D other) {
        currentlyColliding.Remove(other.gameObject);

        if(currentlyColliding.Count <= 0) {            
            gameObject.SendMessage("OnGroundLeave");
        }            
    }
}
