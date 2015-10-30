using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour {

    public GameObject textTime;
    public GameObject textScore;
    public GameObject textEnnemies;

    // Use this for initialization
    void Start () {
        textTime.GetComponent<Text>().text = ScoreManager.time.ToString();
        textScore.GetComponent<Text>().text = ScoreManager.score.ToString();
        textEnnemies.GetComponent<Text>().text = ScoreManager.ennemies.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
