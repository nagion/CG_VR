using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Obstacle : MonoBehaviour {
    public float threshold=200f;
    private Vector3 initialPosition;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MouseDown()
    {
        initialPosition = transform.position;
    }

    public void MouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
        transform.position = pos;
    }

    public void MouseUp()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 pos = Camera.main.WorldToScreenPoint(initialPosition);
        pos.z = Camera.main.nearClipPlane;
        if (Vector3.Distance(pos, mousePos) > threshold)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = initialPosition;
        }
    }
}
