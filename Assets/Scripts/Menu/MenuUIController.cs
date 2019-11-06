using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPlayclick() {
        SceneManager.LoadScene("1_Game", LoadSceneMode.Single);
    }

    public void OnStartclick()
    {
        SceneManager.LoadScene("1.5_Instruction", LoadSceneMode.Single);
    }

    public void OnBack2MenuClick() {
        SceneManager.LoadScene("0_Menu", LoadSceneMode.Single);
    }
}
