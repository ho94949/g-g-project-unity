using UnityEngine;
using System.Collections;

public class DieTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            RoundManager.Die();
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {

    }
}
