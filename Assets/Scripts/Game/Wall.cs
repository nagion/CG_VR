using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    GameObject tower;
    GameObject player;
    GameObject[] sides;
    Vector3 platformSize;
	bool holding;

    float prevPos;
	// Use this for initialization
	void Start () {
        tower = gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        sides = tower.GetComponent<SceneGenerator>().getSides();
        platformSize = tower.GetComponent<SceneGenerator>().platformSize;
		Input.multiTouchEnabled = true;
		holding = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount >= 2) {
			if (Input.touches[0].phase == TouchPhase.Began) {
				holding = true;
			} else if (Input.touches[0].phase == TouchPhase.Moved) {
				holding = true;
				float delta = Input.touches[0].deltaPosition.x / Screen.width;
				tower.transform.Rotate (0, delta * -180, 0);
			}
		}
		else if(holding){
			holding = false;
			int frontSide = 0;
			for(int i=1; i<4; ++i)
			{
				if(sides[i].transform.right.x > sides[frontSide].transform.right.x)
				{
					frontSide = i;
				}
			}
			tower.transform.eulerAngles = -sides[frontSide].transform.localEulerAngles;
			player.transform.rotation = sides [frontSide].transform.rotation;
			Vector3 pos = sides[frontSide].transform.InverseTransformPoint(player.transform.position);
			Vector3 dir = sides[frontSide].transform.InverseTransformDirection(player.transform.forward);
			if (pos.x < platformSize.z && pos.x > -platformSize.z)
			{
				pos.x = -dir.z * platformSize.x / 2;
				player.transform.position = sides[frontSide].transform.TransformPoint(pos);
			}
		}
	}

	/*
    private void OnMouseDown()
    {
        prevPos = Input.mousePosition.x /  Screen.width;
        prevRotation = tower.transform.eulerAngles;
    }

    private void OnMouseDrag()
    {
        float nowPos = Input.mousePosition.x / Screen.width;
        tower.transform.Rotate(0, (nowPos - prevPos) * -180, 0);
        prevPos = nowPos;
    }

    private void OnMouseUp()
    {
        int frontSide = 0;
        for(int i=1; i<4; ++i)
        {
            if(sides[i].transform.right.x > sides[frontSide].transform.right.x)
            {
                frontSide = i;
            }
        }
        tower.transform.eulerAngles = -sides[frontSide].transform.localEulerAngles;
        player.transform.Rotate(prevRotation - tower.transform.eulerAngles);
        Vector3 pos = sides[frontSide].transform.InverseTransformPoint(player.transform.position);
        Vector3 dir = sides[frontSide].transform.InverseTransformDirection(player.transform.forward);
        if (pos.x < platformSize.z && pos.x > -platformSize.z)
        {
            pos.x = -dir.z * platformSize.x / 2;
            player.transform.position = sides[frontSide].transform.TransformPoint(pos);
        }
    }
    */
}
