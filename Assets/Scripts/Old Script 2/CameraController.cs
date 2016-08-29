using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {

    // Use this for initialization
    public float minx;
    public float maxx;
    public float miny;
    public float maxy;
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p)
        {
            Vector2 mpos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
            float halfHeight = GetComponent<Camera>().orthographicSize;
            float halfWidth = halfHeight * GetComponent<Camera>().aspect;
            GetComponent<Transform>().position = new Vector3(Math.Min(maxx - halfWidth, Math.Max(minx + halfWidth, mpos.x)), Math.Min(maxy - halfHeight, Math.Max(miny + halfHeight, mpos.y)), -10);
            //GetComponent<Transform>().position
        }
    }
}
