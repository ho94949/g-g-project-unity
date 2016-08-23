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
            var x = collision.gameObject.transform.position.x;
            var y = collision.gameObject.transform.position.y;
            Debug.Log(collision.gameObject.transform.position.x);
            Debug.Log(collision.gameObject.transform.position.y);
            GameObject objToSpawn = new GameObject("Cool GameObject made from Code");
            objToSpawn.transform.position = collision.gameObject.transform.position;
            objToSpawn.AddComponent<SpriteRenderer>();
            Sprite die = Resources.Load<Sprite>("die");
            Debug.Log(die);
            objToSpawn.GetComponent<SpriteRenderer>().sprite = die;
            Destroy(collision.gameObject);
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {

    }
}
