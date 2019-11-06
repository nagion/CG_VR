using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_rotate : MonoBehaviour {

    Vector3 prePos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        prePos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        Vector3 pos = Input.mousePosition;
        Vector3 origin = Camera.main.WorldToScreenPoint(transform.position);
        float angle = Vector3.Angle(prePos-origin, pos-origin)*1.5f;
        if(Vector3.Cross(prePos - origin, pos - origin).z < 0)
        {
            angle *= -1;
        }
        transform.Rotate(angle, 0, 0);
        prePos = pos;
    }

    private void OnMouseUp()
    {
        if(transform.up.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(180, 0, 0);
        }
    }
}
