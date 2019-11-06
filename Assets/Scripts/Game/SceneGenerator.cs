using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGenerator : MonoBehaviour {
    [System.Serializable]
    public struct Trap
    {
        public GameObject gameObject;
        public float probability;
    }

    public GameObject player;
    public GameObject wall;

    public GameObject BasicPlane;
    public Trap[] traps;
    public GameObject BasicPlatform;

    public float bufferHeight = 50;
    public int stepsPerFloor = 5;
    public float turnProbability = .92f;
    public float generateNewObjectInterval = 10f;

    float width;
    Vector3 currentPos;
    float currentWallY;
    Queue<GameObject> objects;

    float currentTime;
    int direction = 1;

    // constant
    public Vector3 planeSize = new Vector3(2f, 0.5f, 2f);
    public Vector3 platformSize = new Vector3(2f, 0.5f, 4f);
    GameObject[] sides;
    int currentSide;

    // Use this for initialization
    void Start () {
        width = stepsPerFloor * planeSize.z + platformSize.z;
        wall.transform.localScale = new Vector3(width + platformSize.z, width + platformSize.z, width + platformSize.z);

        sides = new GameObject[]
        {
            new GameObject("1"),
            new GameObject("2"),
            new GameObject("3"),
            new GameObject("4")
        };
        sides[0].transform.parent = transform;
        sides[0].transform.localPosition = new Vector3(-width / 2, 0, 0);
        sides[0].transform.localEulerAngles = new Vector3(0, 0, 0);

        sides[1].transform.parent = transform;
        sides[1].transform.localPosition = new Vector3(0, 0, width / 2);
        sides[1].transform.localEulerAngles = new Vector3(0, 90, 0);

        sides[2].transform.parent = transform;
        sides[2].transform.localPosition = new Vector3(width / 2, 0, 0);
        sides[2].transform.localEulerAngles = new Vector3(0, 180, 0);

        sides[3].transform.parent = transform;
        sides[3].transform.localPosition = new Vector3(0, 0, -width / 2);
        sides[3].transform.localEulerAngles = new Vector3(0, 270, 0);
        
        currentSide = 0;
        currentPos = new Vector3(-platformSize.x/2, 0, -width/2);

        objects = new Queue<GameObject>();
        currentTime = Time.realtimeSinceStartup;
        currentWallY = 13;
        GenerateWall();
        currentWallY = 0;

        Vector3 playerPos = sides[currentSide].transform.TransformPoint(currentPos);
        player.transform.SetPositionAndRotation(playerPos, sides[currentSide].transform.rotation);
        // initial first stairs and platform
        GameObject obj = CreateNextObj(BasicPlatform, new Vector3(0, -1, 0));
        obj.transform.GetChild(1).tag = "Untagged";
    }
	
	// Update is called once per frame
	void Update () {
        while(currentPos.y > Camera.main.transform.position.y - bufferHeight)
        {
            GeneratePlaneSet();
        }

        while (currentWallY > Camera.main.transform.position.y - bufferHeight)
        {
            GenerateWall();
        }

        while (objects.Count > 0 && objects.Peek().transform.position.y > Camera.main.transform.position.y + bufferHeight)
        {
            Destroy(objects.Dequeue());
        }
    }

    public GameObject[] getSides()
    {
        return sides;
    }

    private GameObject GetRandomPlane()
    {
        float r = Random.value;
        foreach(Trap trap in traps)
        {
            if(r < trap.probability)
            {
                return trap.gameObject;
            }
            r -= trap.probability;
        }
        return BasicPlane;
    }

    void GeneratePlaneSet() {
        GameObject obj;
        obj = CreateNextObj(BasicPlane, new Vector3(0, -planeSize.y, direction* (platformSize.z + planeSize.z) / 2));
        for (int i = 1; i < stepsPerFloor; i++) {
            obj = CreateNextObj(GetRandomPlane(), new Vector3(0, -planeSize.y, direction * planeSize.z));
        }
        if(Random.value < turnProbability)
        {
            obj = CreateNextObj(BasicPlatform, new Vector3(0, -planeSize.y, direction * (platformSize.z + planeSize.z) / 2));
            obj.transform.GetChild(1).tag = "Untagged";
            obj = CreateNextObj(BasicPlatform, new Vector3(direction * platformSize.x, 0, 0));
            obj.transform.GetChild(1).tag = "Untagged";

            currentPos = sides[currentSide].transform.TransformPoint(currentPos);
            currentSide = (currentSide + direction + 4)%4;
            currentPos = sides[currentSide].transform.InverseTransformPoint(currentPos);
            currentPos.z -= platformSize.x/2;
            currentPos.x -= direction * platformSize.x / 2;
        }
        else
        {
            obj = CreateNextObj(BasicPlatform, new Vector3(0, -planeSize.y, direction * (platformSize.z + planeSize.z) / 2));
            obj.transform.GetChild(1).tag = "turn_right";
            obj = CreateNextObj(BasicPlatform, new Vector3(direction * platformSize.x, 0, 0));
            obj.transform.GetChild(1).tag = "turn_right";
            direction *= -1;
        }
    }

    void GenerateWall()
    {
        GameObject tmp = Instantiate(wall, new Vector3(0, currentWallY, 0), transform.rotation, transform);
        objects.Enqueue(tmp);
        currentWallY -= wall.transform.localScale.y;
    }

    GameObject CreateNextObj(GameObject obj, Vector3 transform) {
        Vector3 nextPos = sides[currentSide].transform.TransformPoint(currentPos + transform);
        GameObject tmp = Instantiate(obj, nextPos, sides[currentSide].transform.rotation, sides[currentSide].transform);
        objects.Enqueue(tmp);
        currentPos += transform;
        return tmp;
    }
}
