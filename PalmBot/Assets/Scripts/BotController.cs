using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{   
    [Header("Game Objects")]
    public GameObject gameController;
    public GameObject tree;
    public Animator animator;
    public SpriteRenderer botRenderer;


    // Movement vars
    [Header("Movement Values")]
    public float movementSpeed = 1f;
    public float xStep = -0.5f;
    public float yStep = 0.25f;
    public float rayDist;
    //[HideInInspector]
    public Vector3 target;

    [Space]
        // Boolean vars
    public bool isDone = false;
    public static bool isPlaceForTree = false;

    private Vector2 finishBotPos;
    Rigidbody2D rbody;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        target = transform.position;
        finishBotPos = new Vector2(2, 4);
        //gameObject.GetComponent<BotRotation>().botDirection;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //CLEAN IT=========
        //if (Input.GetKeyDown(KeyCode.Space))
        //    Move();
        //==================

        // Conditions for animation and movement
        if (transform.position != target)
        {
            //animator.SetBool("isWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
            GameController.isCommandDone = true;
        }
    }

    public void Move()
    {
        //target = new Vector3(target.x + xStep, target.y + yStep);
        animator.SetBool("isWalking", true);

        
        RaycastHit2D ray = Physics2D.Raycast(gameObject.transform.position, new Vector2(xStep, yStep), rayDist, botRenderer.sortingOrder);
        if (ray.collider != null)
        {
            Debug.Log("Walk ray name " + ray.collider.name);
            target = new Vector3(target.x + xStep, target.y + yStep);
        }
        else
        {
            animator.SetBool("isWalking", true);
            //animator.SetTrigger("WalkingTrigger");
            StartCoroutine(Delay());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Green")
        {
            Debug.Log("Green Zone entered");
            isPlaceForTree = true;
            //if (gameController.GetComponent<GameController>().plantTreeCommanded == true)
                //Instantiate(tree);
        }
        if (collision.tag == "Collider")
        {
            Debug.Log("Collider entered");
            target = new Vector2(target.x - xStep, target.y - yStep);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlaceForTree = false;
        //isReadyToPlant = false;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(10f);
    }
}
