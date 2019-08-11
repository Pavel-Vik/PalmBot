using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BotController : MonoBehaviour
{   
    [Header("Game Objects")]
    public GameObject gameController;
    public GameObject tree;
    public Animator animator;
    public GameObject botRenderer;


    // Movement vars
    [Header("Movement Values")]
    public float movementSpeed = 1f;
    public float jumpSpeed = 2f;
    public float xStep = -0.5f;
    public float yStep = 0.25f;
    //[HideInInspector]
    public Vector3 target;
    public static bool isJump = false;
    public static bool isJumpDown = false;

    [Space]
        // Boolean vars
    public bool isDone = false;
    public float walkDelay = 1f;

    private Vector3 finishBotPos;
    private Rigidbody2D rbody;
    private float botSpeed;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        target = transform.position;
        finishBotPos = new Vector3(2, 4);
        //trigger.GetComponent<Transform>().position = new Vector3(0.5f, -0.25f);
        //gameObject.GetComponent<BotRotation>().botDirection;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Conditions for animation and movement
        if (transform.position != target)
        {
            if (isJump)
                botSpeed = jumpSpeed;
            else if (isJumpDown)
                botSpeed = jumpSpeed;
            else
                botSpeed = movementSpeed;

            
            //animator.SetBool("isWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, target, botSpeed * Time.deltaTime);
        }
        else
        {
            isJump = false;
            isJumpDown = false;
            animator.SetBool("isWalking", false);
            GameController.isCommandDone = true;
        }
    }

    private void Update()
    {
    }

    public void Move()
    {
        // Check trigger info
        if (GetComponentInChildren<Trigger>().isGroundAhead == true)
        {
            animator.SetBool("isWalking", true);
            target = new Vector3(target.x + xStep, target.y + yStep, target.z);
        }
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(walkDelay);
        animator.SetBool("isWalking", false);
    }
}
