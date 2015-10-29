using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public GameObject follow;

    public float minX = 0;
    public float maxX = 0;
    public float minY = 0;
    public float maxY = 0;

    float _newX;
    float _newY;

    float _diffX;
    float _diffY;

    // Use this for initialization
    void Start () {
	    
	}
	 
	// Update is called once per frame
	void Update () {      
        if(!follow.GetComponent<Player>().IsDead) {
            _diffX = follow.transform.position.x - transform.position.x;
            _diffY = follow.transform.position.y - transform.position.y;

            _newX = transform.position.x + (_diffX * 0.05f);
            _newY = transform.position.y + (_diffY * 0.05f);

            if (_newX < minX) _newX = minX;
            else if (_newX > maxX) _newX = maxX;

            if (_newY < minY) _newY = minY;
            else if (_newY > maxY) _newY = maxY;

            transform.position = new Vector3(_newX, _newY, transform.position.z);
        }        
	}
}
