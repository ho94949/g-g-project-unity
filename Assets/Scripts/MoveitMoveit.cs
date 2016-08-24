using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;


public class MoveitMoveit : MonoBehaviour {

    // Use this for initialization
    public int jumpCount
    {
        get; set;
    }
    public int frame
    {
        get;
        private set;
    }
    public Sprite spriteDisplay
    {
        get;
        set;
    }
    public int spriteDisplayPriority
    {
        get;
        set;
    }
    private Sprite defaultSpriteDisplay;
    private string MoveDoorName = "";
    private bool onDoor = false;
    public List<GameObject> currentCollision
    {
        get;
        private set;
    }
    public List<GameObject> currentCollisionUpDirection
    {
        get;
        private set;
    }
    public List<GameObject> currentCollisionDownDirection
    {
        get;
        private set;
    }
    public List<GameObject> currentCollisionLeftDirection
    {
        get;
        private set;
    }
    public List<GameObject> currentCollisionRightDirection
    {
        get;
        private set;
    }
    private List<Part> partList
    {
        get;
        set;
    }
    void Start ()
    {
        frame = 0;
        jumpCount = 0;
        spriteDisplayPriority = 0;
        defaultSpriteDisplay = Resources.Load<Sprite>("robot_standing_01");
        spriteDisplay = defaultSpriteDisplay;
        currentCollision = new List<GameObject>();
        currentCollisionUpDirection = new List<GameObject>();
        currentCollisionDownDirection = new List<GameObject>();
        currentCollisionLeftDirection = new List<GameObject>();
        currentCollisionRightDirection = new List<GameObject>();
        partList = new List<Part>();
        partList.Add(MoveRight.Instance);
    }

    void UpdateDoor()
    {
        if(Input.GetAxisRaw("Submit") > 0.5 && onDoor)
        {
            SceneManager.LoadScene(MoveDoorName);
        }
    }
    
    void UpdateJumpCount()
    {

    }
    void UpdateDeath()
    {
        if (
            currentCollisionUpDirection.Count > 0 && currentCollisionDownDirection.Count > 0 ||
            currentCollisionRightDirection.Count > 0 && currentCollisionLeftDirection.Count > 0
        )
        {
            DieTrigger.Die(this.GetComponent<GameObject>());
        }
    }

    bool OnIce()
    {
        bool flag = false;
        foreach(GameObject g in currentCollisionUpDirection)
        {
            if (g.tag == "WallIce")
                flag = true;
        }
        if (!flag) return flag;
        if (GetComponent<Rigidbody2D>().velocity.x == 0) return false;
        return true;
    }

    void Update ()
    {
        ++frame;
        UpdateDoor();
        UpdateJumpCount();
        UpdateDeath();
        spriteDisplayPriority = 0;

        bool isOnIce = OnIce();
        float xVelocity = GetComponent<Rigidbody2D>().velocity.x;
        if (isOnIce) jumpCount = 999;
        

        this.GetComponent<SpriteRenderer>().sprite = spriteDisplay;
        foreach (Part x in partList)
            x.MovePlayer(this);

        foreach(GameObject g in currentCollisionUpDirection)
        {
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            Rigidbody2D rigidbody2D = g.GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
                GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x + rigidbody2D.velocity.x, velocity.y );
            if(g.gameObject.name.StartsWith("belt"))
            {
                string[] words = Regex.Split(g.gameObject.name, "__");
                GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x + float.Parse(words[1]), velocity.y);
            }
        }

        if (isOnIce)
            GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, GetComponent<Rigidbody2D>().velocity.y);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            MoveDoorName = collision.gameObject.name.Substring(5);
            onDoor = true;
        }
        if(collision.gameObject.tag == "Part")
        {
            Player p = Player.Instance;
            partList.Add(PartManager.getPart(collision.gameObject.name));
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

        currentCollision.Add(collision.gameObject);

        //meeting at signle point
        if (collision.contacts.Length < 2) return;
        Vector2 vector2 = (collision.contacts[0].point - collision.contacts[1].point) ;
        if (System.Math.Abs(vector2.x) < 2e-2 && System.Math.Abs(vector2.y) < 2e-2)
            return;

        if(collision.gameObject.tag.StartsWith("Wall") )
        {
            Vector2 normalVector = collision.contacts[0].normal;
            if (normalVector == Vector2.up)
            {
                currentCollisionUpDirection.Add(collision.gameObject);
                jumpCount = 0;
            }
            if (normalVector == Vector2.down)
                currentCollisionDownDirection.Add(collision.gameObject);
            if (normalVector == Vector2.left)
                currentCollisionLeftDirection.Add(collision.gameObject);
            if (normalVector == Vector2.right)
                currentCollisionRightDirection.Add(collision.gameObject);
        }
        
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        currentCollision.Remove(collision.gameObject);
        int upCnt = currentCollisionUpDirection.Count;
        currentCollisionUpDirection.Remove(collision.gameObject);
        int upCnt2 = currentCollisionUpDirection.Count;
        if (upCnt > 0 && upCnt2 == 0 && jumpCount == 0) jumpCount = 1;
        currentCollisionDownDirection.Remove(collision.gameObject);
        currentCollisionLeftDirection.Remove(collision.gameObject);
        currentCollisionRightDirection.Remove(collision.gameObject);
        
    }
    
}