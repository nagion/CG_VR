using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_fallable : MonoBehaviour {
    //public Rigidbody plane;
    public Material normalColor;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

   public void MouseOver() {
      //  if (Input.GetMouseButtonDown(0)){
            // Whatever you want it to do.
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Color c = GetComponent<Renderer>().material.color;
            c.a = 1.0f;
            GetComponent<Renderer>().material.color = c;
            tag = "Untagged";
      //  }
    }
}
