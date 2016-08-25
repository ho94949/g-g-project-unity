using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class Belt : MonoBehaviour {

    private int frame = 0;
    public float velocity = 0;
    List<Sprite> s;
	// Use this for initialization
	void Start () {
        frame = 0;
        s = new List<Sprite>();
        if (velocity < 0)
        {
            s.Add(Resources.Load<Sprite>("belt_01"));
            s.Add(Resources.Load<Sprite>("belt_02"));
            s.Add(Resources.Load<Sprite>("belt_03"));
            s.Add(Resources.Load<Sprite>("belt_04"));
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
        int rframe = (int)((frame * velocity) / 10.0);
        /*Debug.Log(rframe);
        rframe %= 4;
        rframe += 4;
        rframe %= 4;
        Debug.Log(rframe);
        GetComponent<SpriteRenderer>().sprite = s[rframe];*/
        if (rframe < 0)
        {
            rframe = -rframe;
        }
        rframe %= 4;
        GetComponent<SpriteRenderer>().sprite = s[rframe];

    }
}
