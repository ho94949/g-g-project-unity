using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    List<Block> blockList; 

	
    
    
    // Use this for initialization
	void Start ()
    {
        blockList = new List<Block>();
        foreach(GameObject g in GameObject.FindObjectsOfType<GameObject>())
        {
            Debug.Log(g);
            if (g.tag == "Wall")
                blockList.Add(new Wall(g));
            if (g.tag == "Player")
                blockList.Add(new Player(g));
        }

    }
	
	// Update is called once per frame
	void Update ()
    {

    }
}
