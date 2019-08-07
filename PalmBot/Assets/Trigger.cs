using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool isGroundAhead = true;
    public bool isTileToJump = false;
    public bool isEdgeAhead = false;

    public int floorToCheckForWalking = 1;
    public int floorToCheckForJumping = 2;

    private float xStep, yStep;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate trigger where the bot look to
        xStep = GetComponentInParent<BotController>().xStep;
        yStep = GetComponentInParent<BotController>().yStep;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(xStep, yStep);

        floorToCheckForJumping = floorToCheckForWalking + 1;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (floorToCheckForWalking.ToString() == collision.name)
        {
            isGroundAhead = true;
            isTileToJump = false;
        }

        if (isGroundAhead == false)
            if (floorToCheckForJumping.ToString() == collision.name)
                isTileToJump = true;

        if (collision.name == "0")
            isEdgeAhead = true;

        Debug.Log("Trigger collide floor_: " + collision.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (floorToCheckForWalking.ToString() == collision.name)
            isGroundAhead = false;

        if (floorToCheckForJumping.ToString() == collision.name)
            isTileToJump = false;

        if (collision.name == "0")
            isEdgeAhead = false;
    }
}
