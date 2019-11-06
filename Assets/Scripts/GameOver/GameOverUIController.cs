using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour {
    public Text scoreText, recordText;
    public GameObject newrecord;

	// Use this for initialization
	void Start () {
        newrecord.SetActive(false);
        int s = PlayerPrefs.GetInt("Score");
        scoreText.text = s.ToString();
        int r = PlayerPrefs.GetInt("Record");
        recordText.text = r.ToString();
        if (s>r)
        {
            r = s;
            recordText.text = r.ToString();
            PlayerPrefs.SetInt("Record",r);
            newrecord.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
    }
}
