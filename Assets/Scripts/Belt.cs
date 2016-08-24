using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class Belt : MonoBehaviour {

    private int frame = 0;
    private float range = 0;
    List<Sprite> s;
	// Use this for initialization
	void Start () {
        frame = 0;
        s = new List<Sprite>();
        range = float.Parse(Regex.Split(name, "__")[1]);
        if (range < 0)
        {
            s.Add(Resources.Load<Sprite>("belt_04"));
            s.Add(Resources.Load<Sprite>("belt_03"));
            s.Add(Resources.Load<Sprite>("belt_02"));
            s.Add(Resources.Load<Sprite>("belt_01"));
        }
        else
        {
            s.Add(Resources.Load<Sprite>("belt_05"));
            s.Add(Resources.Load<Sprite>("belt_06"));
            s.Add(Resources.Load<Sprite>("belt_07"));
            s.Add(Resources.Load<Sprite>("belt_08"));

        }
    }
	
	// Update is called once per frame
	void Update () {
        ++frame;
        int rframe = (int)((frame * range) / 10.0);
        rframe %= 4;
        rframe += 4;
        rframe %= 4;
        rframe++;
        GetComponent<SpriteRenderer>().sprite = s[rframe];
        
	}
}
