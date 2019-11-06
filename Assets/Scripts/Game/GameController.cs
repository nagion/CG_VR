using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text ScoreText;
    int startTime;
    private bool isCoroutineExecuting = false;

    // Use this for initialization
    void Start () {
        startTime = (int)Time.realtimeSinceStartup;
		
	}
	
	// Update is called once per frame
	void Update () {
        int score = (int)Time.realtimeSinceStartup - startTime;
        ScoreText.text = score.ToString();
        PlayerPrefs.SetInt("Score", score);

        // Gameover
        if (PlayerMove.instance.isDead) {
            CameraController.instance.target = null;
            StartCoroutine(ExecuteAfterTime(1));
        }
    }

    IEnumerator ExecuteAfterTime(float time) {
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        isCoroutineExecuting = false;
        SceneManager.LoadScene("2_GameOver");
    }
}
