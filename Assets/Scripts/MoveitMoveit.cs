using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;


public class MoveitMoveit : MonoBehaviour {

    // Use this for initialization
    public int jumpCount
    {
        get; set;
    }
    public int frame;
    public Sprite spriteDisplay;
    public int spriteDisplayPriority;
    public Sprite defaultSpriteDisplay;
    string MoveDoorName = "";
    bool onDoor = false;
    int framecount = 0;
	void Start ()
    {
        frame = 0;
        jumpCount = 0;
        spriteDisplayPriority = 0;
        defaultSpriteDisplay = Resources.Load<Sprite>("robot_standing_01");
        spriteDisplay = defaultSpriteDisplay;
    }


    void UpdateDoor()
    {
        if(Input.GetAxisRaw("Submit") > 0.5 && onDoor)
        {
            SceneManager.LoadScene(MoveDoorName);
        }
    }

    public List<GameObject> curCol = new List<GameObject>();
    public List<GameObject> curColupdir = new List<GameObject>();
    public List<GameObject> curColleftdir = new List<GameObject>();
    public List<GameObject> curColrightdir = new List<GameObject>();

    // Update is called once per frame
    void Update ()
    {
        UpdateDoor();

        //spriteDisplay = defaultSpriteDisplay;
        spriteDisplayPriority = 0;

        Player p = Player.Instance;
        if (p.P1 != null) p.P1.MovePlayer(this);
        if (p.P2 != null) p.P2.MovePlayer(this);
        if (p.P3 != null) p.P3.MovePlayer(this);
        //Debug.Log(++framecount);
        if (curColupdir.Count==0 && jumpCount==0) jumpCount = 1;
        //Debug.Log(jumpCount);
        ++frame;
        this.GetComponent<SpriteRenderer>().sprite = spriteDisplay;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            MoveDoorName = collision.gameObject.name.Substring(5);
            onDoor = true;
        }
        if(collision.gameObject.tag == "Part1")
        {
            Player p = Player.Instance;
            p.P1 = PartManager.getPart(collision.gameObject.name);
            Debug.Log(collision.gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Part2")
        {
            Player p = Player.Instance;
            p.P2 = PartManager.getPart(collision.gameObject.name);
            Debug.Log(collision.gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Part3")
        {
            Player p = Player.Instance;
            p.P3 = PartManager.getPart(collision.gameObject.name);
            Debug.Log(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            onDoor = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        curCol.Add(collision.gameObject);
        if (collision.contacts.Length < 2) return;
        Vector2 vector2 = (collision.contacts[0].point - collision.contacts[1].point) ;
        Debug.Log(System.String.Format("{0} {1}", collision.contacts[0].point.x, collision.contacts[0].point.y));
        Debug.Log(System.String.Format("{0} {1}", collision.contacts[1].point.x, collision.contacts[1].point.y));
        Debug.Log(collision.contacts[0].normal);
        Debug.Log(collision.contacts[1].normal);
        Debug.Log(collision.gameObject);
        if (System.Math.Abs(vector2.x) < 2e-2 && System.Math.Abs(vector2.y) < 2e-2)
            return;
        Debug.Log(System.Math.Abs(vector2.x));
        Debug.Log(System.Math.Abs(vector2.y));
        if (collision.contacts[0].normal.y > (1 - 2e-2) && collision.gameObject.tag == "Wall")
        {
            curColupdir.Add(collision.gameObject);
            jumpCount = 0;
            Debug.Log(jumpCount);
        }
        if (collision.contacts[0].normal.x > (1 - 2e-2) && collision.gameObject.tag == "Wall")
        {
            curColrightdir.Add(collision.gameObject);
        }
        if (collision.contacts[0].normal.x < -(1 - 2e-2) && collision.gameObject.tag == "Wall")
        {
            curColleftdir.Add(collision.gameObject);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        curCol.Remove(collision.gameObject);
        curColupdir.Remove(collision.gameObject);
        curColleftdir.Remove(collision.gameObject);
        curColrightdir.Remove(collision.gameObject);
    }
    
}