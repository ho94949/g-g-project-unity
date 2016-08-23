using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class RoundTrip : MonoBehaviour
{
    Vector2 direction;
    // Use this for initialization
    void Start()
    {
        string[] words = Regex.Split(this.name, "__");

        //direction = new Vector2(float.Parse(words[1]), float.Parse(words[2]));
        GetComponent<Rigidbody2D>().velocity = new Vector2(float.Parse(words[1]), float.Parse(words[2]));
        Debug.Log(direction);
    }

    // Update is called once per frame
    void Update()       
    {
        //Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        //GetComponent<Rigidbody2D>().velocity = direction;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlockMove")
        {
            //direction *= -1;
            GetComponent<Rigidbody2D>().velocity *= -1;
        }
    }
}