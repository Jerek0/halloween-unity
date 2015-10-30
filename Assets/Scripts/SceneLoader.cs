using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    public float delay = 0f;
    public string sceneToLoad;

    bool listening = false;

	// Use this for initialization
	void Start () {
        StartCoroutine(startListening());
	}
	
	// Update is called once per frame
	void Update () {
	    if(listening && Input.anyKey) {
            Debug.Log("input pressed");
            Application.LoadLevel(sceneToLoad);
        }
	}

    IEnumerator startListening() {
        Debug.Log("start");
        yield return new WaitForSeconds(delay);
        Debug.Log("end");
        listening = true;
    }
}
 