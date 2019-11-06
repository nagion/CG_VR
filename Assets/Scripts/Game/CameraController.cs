using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public static CameraController instance;
    public GameObject target = null;
    // Use this for initialization
    void Start () {
        instance = this;
	}

    // Update is called once per frame
    void Update() {
        if (target != null) {
            Vector3 pos = transform.position;
            pos.y = target.transform.position.y;
            transform.position = pos;
        }
    }
}
