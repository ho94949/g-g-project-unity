using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class RoundTrip : MonoBehaviour
{
    public float x;
    public float y;

    Vector2 direction;
    // Use this for initialization
    void Start()
    {
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
    }

    // Update is called once per frame
    void Update()       
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlockMove")
        {
            GetComponent<Rigidbody2D>().velocity *= -1;
        }
    }
}