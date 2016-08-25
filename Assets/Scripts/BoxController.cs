using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class BoxController : MonoBehaviour {

    // Use this for initialization
    public Vector2 position;
    public float gravityDirection = 1.0f;
    public float acceleration = 9.8f;
    public Vector2 velocity;
    void Start () {
        position = new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y);
        velocity = new Vector2(0.0f, 0.0f);
    }


    public bool existUpDirection = false;
    public bool existDownDirection = false;
    public bool existLeftDirection = false;
    public bool existRightDirection = false;

    // Update is called once per frame
    void Update () {
        gravityDirection = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().gravityDirection;

        existUpDirection = false;
        existDownDirection = false;
        existLeftDirection = false;
        existRightDirection = false;
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>().Where(_ => _.tag.StartsWith("Wall")))
        {
            if (this== obj.GetComponent<BoxController>()) continue;
            PlayerController.CollisionDirection dir = CheckWallCollision(obj.GetComponent<BoxCollider2D>(), true);
            if (dir == PlayerController.CollisionDirection.Up) existUpDirection = true;
            if (dir == PlayerController.CollisionDirection.Down) existDownDirection = true;
            if (dir == PlayerController.CollisionDirection.Left) existLeftDirection = true;
            if (dir == PlayerController.CollisionDirection.Right) existRightDirection = true;
        }

        velocity -= new Vector2(0, acceleration * gravityDirection) * Time.deltaTime;
        Debug.Log(velocity);
        position += velocity * Time.deltaTime;
        GetComponent<Transform>().position = new Vector3(position.x, position.y);
    }   


    PlayerController.CollisionDirection CheckWallCollision(Collider2D collision, bool velocityMove)
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

        if (CollisionLeftDown.x > CollisionRightUp.x) return PlayerController.CollisionDirection.None;
        if (CollisionLeftDown.y > CollisionRightUp.y) return PlayerController.CollisionDirection.None;
        bool up = CollisionRightUp.y == wallRightUp.y;
        bool down = CollisionLeftDown.y == wallLeftDown.y;
        bool left = CollisionLeftDown.x == wallLeftDown.x;
        bool right = CollisionRightUp.x == wallRightUp.x;

        float dx = CollisionRightUp.x - CollisionLeftDown.x;
        float dy = CollisionRightUp.y - CollisionLeftDown.y;
        if (up && left)
        {
            if (dx > dy) left = false;
            else up = false;
        }
        if (up && right)
        {
            if (dx > dy) right = false;
            else up = false;
        }
        if (down && left)
        {
            if (dx > dy) left = false;
            else down = false;
        }
        if (down && right)
        {
            if (dx > dy) right = false;
            else down = false;
        }
        if (up)
        {
            if (velocityMove) velocity = new Vector2(velocity.x, Math.Max(0.0f, velocity.y));
            if (gravityDirection > 0)
                position = new Vector2(position.x, wallRightUp.y + playerSize.y * 0.5f);
            else
                position = new Vector2(position.x, wallRightUp.y + playerSize.y * 0.52f);

            //this came from up
            return PlayerController.CollisionDirection.Up;

        }
        if (down)
        {
            if (velocityMove) velocity = new Vector2(velocity.x, Math.Min(0.0f, velocity.y));
            if (gravityDirection > 0)
                position = new Vector2(position.x, wallLeftDown.y - playerSize.y * 0.52f);
            else
                position = new Vector2(position.x, wallLeftDown.y - playerSize.y * 0.5f);

            //this came from down
            return PlayerController.CollisionDirection.Down;
        }
        if (left)
        {
            //this came from left
            if (velocityMove) velocity = new Vector2(Math.Min(0.0f, velocity.x), velocity.y);
            position = new Vector2(wallLeftDown.x - playerSize.x * 0.5f, position.y);
            return PlayerController.CollisionDirection.Left;
        }
        if (right)
        {
            if (velocityMove) velocity = new Vector2(Math.Max(0.0f, velocity.x), velocity.y);
            position = new Vector2(wallRightUp.x + playerSize.x * 0.5f, position.y);
            return PlayerController.CollisionDirection.Right;
            //this came from right;
        }
        return PlayerController.CollisionDirection.None;
    }
}
