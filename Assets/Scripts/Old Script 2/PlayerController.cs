using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public int frame
    {
        get; private set;
    }
    public Sprite spriteDisplay;
    public int spriteDisplayPriority;
    private Sprite defaultSpriteDisplay;
    public Vector2 position;
    
    public List<Part> partList
    {
        get; private set;
    }
    public List<Part> disabledPartList
    {
        get; private set;
    }

    public Vector2 velocity
    {
        get; set;
    }

    public enum CollisionDirection
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }

    public int jumpCount;
    public bool isJump;

    private float acceleration = 9.8f;
    public float gravityDirection = 1.0f;
    private bool onDoor;
    private string moveDoorName;

    // Use this for initialization
    void Start ()
    {
        defaultSpriteDisplay = Resources.Load<Sprite>("robot_standing_01");
        partList = new List<Part>();
        partList.Add(PartRight.Instance);
        disabledPartList = new List<Part>();
        velocity = new Vector2(0.0f, 0.0f);
        position = new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y);
        jumpCount = 0;
        spriteDisplay = defaultSpriteDisplay;
        acceleration = 9.8f;
        gravityDirection = 1.0f;
    }
	

    void CheckDoor()
    {
        if(onDoor && Input.GetAxisRaw("Submit")>0.5)
        {
            SceneManager.LoadScene(moveDoorName);
        }
    }


	// Update is called once per frame
	void Update ()
    {
        ++frame;

       
        spriteDisplayPriority = 0;

        bool existUpDirection = false;
        bool existDownDirection = false;
        bool existLeftDirection = false;
        bool existRightDirection = false;
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>().Where(_ => _.tag.StartsWith("Wall")))
        {
            CollisionDirection dir = CheckWallCollision(obj.GetComponent<BoxCollider2D>(), true);
            if (
                dir == CollisionDirection.Up && gravityDirection > 0 ||
                dir == CollisionDirection.Down && gravityDirection < 0 
                )
                jumpCount = 0;
            if (dir == CollisionDirection.Up) existUpDirection = true;
            if (dir == CollisionDirection.Down) existDownDirection = true;
            if (dir == CollisionDirection.Left) existLeftDirection = true;
            if (dir == CollisionDirection.Right) existRightDirection = true;
            if (dir != CollisionDirection.None)
            {
                Debug.Log(obj);
                Debug.Log(dir);
            }
        }
        if (existUpDirection && existDownDirection || existLeftDirection && existRightDirection)
            RoundManager.Die();
        if (
            (
                (!existUpDirection && gravityDirection > 0) ||
                (!existDownDirection && gravityDirection < 0) 
            )

            && jumpCount == 0) jumpCount = 1;


        foreach (Part p in partList)
            p.MovePlayer(this);

        //if (existLeftDirection)
        //    velocity = new Vector2(Math.Min(0.0f,velocity.x), velocity.y);

        //if (existRightDirection)
        //    velocity = new Vector2(Math.Max(0.0f, velocity.x), velocity.y);

        //Deprecated but (possibly) working code

        velocity -= new Vector2(0, acceleration * gravityDirection) * Time.deltaTime;
        Vector2 velocityToAdd = new Vector2(0.0f, 0.0f);
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>().Where(_ => _.tag.StartsWith("Wall")))
        {

            CollisionDirection dir = CheckWallCollision(obj.GetComponent<BoxCollider2D>(), true);
            if (
                dir == CollisionDirection.Up && gravityDirection > 0 ||
                dir == CollisionDirection.Down && gravityDirection < 0 
            )
            {
                Rigidbody2D rigidbody2D = obj.GetComponent<Rigidbody2D>();
                if(rigidbody2D)
                    velocityToAdd += rigidbody2D.velocity;
                if (obj.tag == "WallBelt")
                    velocityToAdd += new Vector2(obj.GetComponent<Belt>().velocity * gravityDirection, 0.0f);
            }
        }
        
        position += new Vector2(velocity.x + velocityToAdd.x, velocity.y + Math.Min(0.0f, velocityToAdd.y - 0.1f * gravityDirection)) * Time.deltaTime;
        GetComponent<Transform>().position = new Vector3(position.x, position.y);


        GetComponent<SpriteRenderer>().sprite = spriteDisplay;

        isJump = Input.GetAxisRaw("Jump") > 0.5;

        CheckDoor();
    }

    CollisionDirection CheckWallCollision(Collider2D collision, bool velocityMove)
    {
        if (!collision.gameObject.tag.StartsWith("Wall"))
            throw new ArgumentException();

        //fuck
        Vector3 _wallPosition = collision.gameObject.GetComponent<Transform>().position;
        Vector2 wallPosition = new Vector2(_wallPosition.x, _wallPosition.y);
        Vector2 wallSize = collision.gameObject.GetComponent<BoxCollider2D>().size;

        Vector2 wallLeftDown = wallPosition - wallSize * 0.5f;
        Vector2 wallRightUp = wallPosition + wallSize * 0.5f;

        //Vector3 _playerPosition = GetComponent<Transform>().position;
        //new Vector2(_playerPosition.x, _playerPosition.y);
        Vector2 playerPosition = position;
        Vector2 playerSize = GetComponent<BoxCollider2D>().size;

        Vector2 playerLeftDown = playerPosition - playerSize * 0.5f;
        Vector2 playerRightUp = playerPosition + playerSize * 0.5f;


        Vector2 CollisionRightUp = new Vector2(Math.Min(playerRightUp.x, wallRightUp.x), Math.Min(playerRightUp.y, wallRightUp.y));
        Vector2 CollisionLeftDown = new Vector2(Math.Max(playerLeftDown.x, wallLeftDown.x), Math.Max(playerLeftDown.y, wallLeftDown.y));

        if (CollisionLeftDown.x > CollisionRightUp.x) return CollisionDirection.None;
        if (CollisionLeftDown.y > CollisionRightUp.y) return CollisionDirection.None;
        bool up = CollisionRightUp.y == wallRightUp.y;
        bool down = CollisionLeftDown.y == wallLeftDown.y;
        bool left = CollisionLeftDown.x == wallLeftDown.x;
        bool right = CollisionRightUp.x == wallRightUp.x;

        float dx = CollisionRightUp.x - CollisionLeftDown.x;
        float dy = CollisionRightUp.y - CollisionLeftDown.y;
        if ((dx < 0.02f && dy < 0.02f)) return PlayerController.CollisionDirection.None;
        if (up && left)
        {
            if (dx+0.001f > dy) left = false;
            else up = false;
        }
        if (up && right)
        {
            if (dx + 0.001f > dy) right = false;
            else up = false;
        }
        if (down && left)
        {
            if (dx + 0.001f > dy) left = false;
            else down = false;
        }
        if (down && right)
        {
            if (dx + 0.001f > dy) right = false;
            else down = false;
        }
        bool isBox = collision.gameObject.tag.StartsWith("WallBox");
        bool isBoxDown = isBox;
        bool isBoxUp = isBox;
        bool isBoxRight = isBox;
        bool isBoxLeft = isBox;


        if (isBox)
            isBoxDown = !collision.gameObject.GetComponent<BoxController>().existDownDirection;
        if (isBox)
            isBoxUp = !collision.gameObject.GetComponent<BoxController>().existUpDirection;
        if (isBox)
            isBoxRight = !collision.gameObject.GetComponent<BoxController>().existRightDirection;
        if (isBox)
            isBoxLeft = !collision.gameObject.GetComponent<BoxController>().existLeftDirection;

        //Debug.Log(isBoxRight);
        if (up)
            {
                if (!isBoxUp)
                {
                    if (velocityMove) velocity = new Vector2(velocity.x, Math.Max(0.0f, velocity.y));
                    if (gravityDirection > 0)
                        position = new Vector2(position.x, wallRightUp.y + playerSize.y * 0.5f);
                    else
                        position = new Vector2(position.x, wallRightUp.y + playerSize.y * 0.52f);

                    //this came from up
                    return CollisionDirection.Up;
                }
                else
                {

                Vector2 sp = collision.gameObject.GetComponent<BoxController>().velocity;
                Vector2 pos = collision.gameObject.GetComponent<BoxController>().position;
                Vector2 psz = collision.gameObject.GetComponent<BoxCollider2D>().size;
                if (velocityMove) collision.gameObject.GetComponent<BoxController>().velocity = new Vector2(sp.x, Math.Min(0.0f, sp.y));
                    if (gravityDirection > 0)
                        collision.gameObject.GetComponent<BoxController>().position = new Vector2(pos.x, CollisionLeftDown.y - psz.y * 0.52f);
                    else
                        collision.gameObject.GetComponent<BoxController>().position = new Vector2(pos.x, CollisionLeftDown.y - psz.y * 0.5f);

                    //this came from down
                    return CollisionDirection.Up;
                }
        {
            if(velocityMove) velocity = new Vector2(velocity.x, Math.Max(0.0f,velocity.y) );
            if(gravityDirection > 0)
                position = new Vector2(position.x, wallRightUp.y + playerSize.y * 0.49f);
            else
                position = new Vector2(position.x, wallRightUp.y + playerSize.y * 0.52f);

            //this came from up
            return CollisionDirection.Up;

        }
        if (down)
        {
            if (velocityMove) velocity = new Vector2(velocity.x, Math.Min(0.0f, velocity.y));
            if(gravityDirection>0)
                position = new Vector2(position.x, wallLeftDown.y - playerSize.y * 0.52f);
            else
                position = new Vector2(position.x, wallLeftDown.y - playerSize.y * 0.5f);

            //this came from down
            return CollisionDirection.Down;
        }
        if (left)
        {
            //this came from left
            if (velocityMove) velocity = new Vector2(Math.Min(0.0f, velocity.x), velocity.y);
            position = new Vector2(wallLeftDown.x - playerSize.x * 0.5f, position.y);
            return CollisionDirection.Left;
        }
        if (right)
        {
            if (velocityMove) velocity = new Vector2(Math.Max(0.0f, velocity.x), velocity.y);
            position = new Vector2(wallRightUp.x + playerSize.x * 0.5f, position.y);
            return CollisionDirection.Right;
            //this came from right;
        }

            }
            if (down)
            {
                if(!isBoxDown)
                {
                    if (velocityMove) velocity = new Vector2(velocity.x, Math.Min(0.0f, velocity.y));
                    if (gravityDirection > 0)
                        position = new Vector2(position.x, wallLeftDown.y - playerSize.y * 0.52f);
                    else
                        position = new Vector2(position.x, wallLeftDown.y - playerSize.y * 0.5f);

                    //this came from down
                    return CollisionDirection.Down;
                }
                else
                {

                    Vector2 sp = collision.gameObject.GetComponent<BoxController>().velocity;
                    Vector2 pos = collision.gameObject.GetComponent<BoxController>().position;
                    Vector2 psz = collision.gameObject.GetComponent<BoxCollider2D>().size;
                    if (velocityMove) collision.gameObject.GetComponent<BoxController>().velocity = new Vector2(sp.x, Math.Max(0.0f, sp.y));
                    if (gravityDirection > 0)
                        collision.gameObject.GetComponent<BoxController>().position = new Vector2(pos.x, CollisionRightUp.y + psz.y * 0.5f);
                    else
                        collision.gameObject.GetComponent<BoxController>().position = new Vector2(pos.x, CollisionRightUp.y + psz.y * 0.52f);

                    //this came from up
                    return CollisionDirection.Down;
                }
            }
            if (left)
            {
                if (!isBoxLeft)
                {
                    //this came from left
                    if (velocityMove) velocity = new Vector2(Math.Min(0.0f, velocity.x), velocity.y);
                    position = new Vector2(wallLeftDown.x - playerSize.x * 0.5f, position.y);
                    return CollisionDirection.Left;
                }
                else
                {
                    Vector2 sp = collision.gameObject.GetComponent<BoxController>().velocity;
                    Vector2 pos = collision.gameObject.GetComponent<BoxController>().position;
                    Vector2 psz = collision.gameObject.GetComponent<BoxCollider2D>().size;

                    if (velocityMove) collision.gameObject.GetComponent<BoxController>().velocity = new Vector2(Math.Max(0.0f, sp.x), sp.y);
                    collision.gameObject.GetComponent<BoxController>().position = new Vector2(CollisionRightUp.x + psz.x * 0.5f, pos.y);
                    return CollisionDirection.Left;
                }
            }
            if (right)
            {
                if (!isBoxRight)
                {
                    if (velocityMove) velocity = new Vector2(Math.Max(0.0f, velocity.x), velocity.y);
                    position = new Vector2(wallRightUp.x + playerSize.x * 0.5f, position.y);
                    return CollisionDirection.Right;
                    //this came from right;
                }
                else
            {
                Vector2 sp = collision.gameObject.GetComponent<BoxController>().velocity;
                Vector2 pos = collision.gameObject.GetComponent<BoxController>().position;
                Vector2 psz = collision.gameObject.GetComponent<BoxCollider2D>().size;

                if (velocityMove) collision.gameObject.GetComponent<BoxController>().velocity = new Vector2(Math.Min(0.0f, sp.x), sp.y);
                    collision.gameObject.GetComponent<BoxController>().position = new Vector2(CollisionLeftDown.x - psz.x * 0.5f, pos.y);
                    return CollisionDirection.Right;
                }
            }
        return CollisionDirection.None;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
            onDoor = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            moveDoorName = collision.gameObject.GetComponent<DoorController>().SceneName;
            onDoor = true;
        }
    }
    public void AddPart(Part part)
    {
        List<int> rm = new List<int>();
        for (int i = 0; i < partList.Count; i++)
        {
            if (partList[i].Type() == part.Type())
            {
                disabledPartList.Add(partList[i]);
                partList.Remove(partList[i]);
            }
        }
        for (int i = 0; i < disabledPartList.Count; i++)
        {
            if (disabledPartList[i].Name() == part.Name())
            {
                disabledPartList.Remove(disabledPartList[i]);
            }
        }
        partList.Add(part);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Part")
        {
            Part part = PartManager.getPart(collision.gameObject.GetComponent<PartIndicator>().Type);
            AddPart(part);
            Destroy(collision.gameObject);
        }
    }
}
