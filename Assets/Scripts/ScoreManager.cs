using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour {

    public static int score;
    public static int ennemies;
    public static float time;

    float launchTime;

    public GameObject textScore;
    public GameObject textEnnemies;
    public GameObject textTime;

    void Start() {
        score = 0;
        ennemies = 0;      
        launchTime = Time.realtimeSinceStartup;
        time = 0f;
    }    
	
	// Update is called once per frame
	void Update () {
        time = Mathf.Round((Time.realtimeSinceStartup - launchTime) * 1000f) / 1000f;

        if(textScore) textScore.GetComponent<Text>().text = score.ToString();
        if (textEnnemies) textEnnemies.GetComponent<Text>().text = ennemies.ToString();
        if (textTime) textTime.GetComponent<Text>().text = time.ToString();
    }
}
