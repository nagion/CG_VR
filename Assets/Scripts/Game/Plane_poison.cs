using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_poison : MonoBehaviour {


    public Material poisonColor1;
    public Material poisonColor2;
    public Material normalColor;
    public GameObject poisonEffect;
    public GameObject poisonInstance;

    int tapCount = 3;
	// Use this for initialization
	void Start () {
		poisonInstance = (GameObject) Instantiate(poisonEffect, gameObject.transform.position, gameObject.transform.rotation, transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MouseOver()
    {
      //  if (Input.GetMouseButtonDown(0))
      //  {
            tapCount--;
            Debug.Log(tapCount);
            if (tapCount == 2)
                this.GetComponent<Renderer>().material = poisonColor1;
            if (tapCount == 1)
                this.GetComponent<Renderer>().material = poisonColor2;
            if (tapCount == 0)
            {
                this.GetComponent<Renderer>().material = normalColor;
                this.tag = "Untagged";
                Destroy(poisonInstance);
            }
  //      }
    }
}
