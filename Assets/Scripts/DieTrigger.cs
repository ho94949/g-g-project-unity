using UnityEngine;
using System.Collections;

public class DieTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static void Die(GameObject g)
    {
        var x = g.transform.position.x;
        var y = g.transform.position.y;
        Debug.Log(g.transform.position.x);
        Debug.Log(g.transform.position.y);
        GameObject objToSpawn = new GameObject("Cool GameObject made from Code");
        objToSpawn.transform.position = g.transform.position;
        objToSpawn.AddComponent<SpriteRenderer>();
        Sprite die = Resources.Load<Sprite>("die");
        Debug.Log(die);
        objToSpawn.GetComponent<SpriteRenderer>().sprite = die;
        Destroy(g);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            Die(collision.gameObject);
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {

    }
}
