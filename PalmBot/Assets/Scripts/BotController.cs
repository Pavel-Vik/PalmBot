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
    public Transform trigger;
    public LineRenderer line;


    // Movement vars
    [Header("Movement Values")]
    public float movementSpeed = 1f;
    public float xStep = -0.5f;
    public float yStep = 0.25f;
    public float rayDist;
    //[HideInInspector]
    public Vector3 target;
    public static bool isJump = false;

    [Space]
        // Boolean vars
    public bool isDone = false;
    public static bool isPlaceForTree = false;
    public float walkDelay = 1f;

    private Vector2 finishBotPos;
    private Rigidbody2D rbody;
    private bool botCollision2floor = false;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        target = transform.position;
        finishBotPos = new Vector2(2, 4);
        //trigger.GetComponent<Transform>().position = new Vector3(0.5f, -0.25f);
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
            //if (botRenderer.sortingOrder == 1)
            animator.SetBool("isWalking", false);
            GameController.isCommandDone = true;
        }
    }

    private void Update()
    {
        //RaycastHit2D[] rays;
        //rays = Physics2D.RaycastAll(gameObject.transform.position, new Vector2(gameObject.transform.position.x + xStep, gameObject.transform.position.y + yStep), rayDist);

        //line.SetPosition(0, gameObject.transform.position);
        //line.SetPosition(1, new Vector2(gameObject.transform.position.x + xStep, gameObject.transform.position.y + yStep));

        //Debug.Log("COUNT OF RAYCASTS: " + rays.Length);
        //for (int i = 0; i < rays.Length; i++)
        //{
        //    Debug.Log("MOVE RAYS: " + rays[i].collider.name);
        //}

        //RaycastHit2D triggerRay = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + xStep, gameObject.transform.position.y + yStep), new Vector2(gameObject.transform.position.x + xStep * 2, gameObject.transform.position.y + yStep *2), rayDist, botRenderer.sortingOrder);
        //RaycastHit2D botRay = Physics2D.Raycast(gameObject.transform.position, new Vector2(xStep, yStep), rayDist, botRenderer.sortingOrder);

        //Debug.Log("Walk ray1 " + ray1.collider.name);
        //Debug.Log("BOT ray = " + botRay.collider.name);
    }

    public void Move()
    {
        //target = new Vector3(target.x + xStep, target.y + yStep);
        animator.SetBool("isWalking", true);
        //trigger.GetComponent<Transform>().position = new Vector3(xStep, yStep);

        //RaycastHit2D[] rays;
        //rays = Physics2D.RaycastAll(gameObject.transform.position, new Vector2(gameObject.transform.position.x + xStep, gameObject.transform.position.y + yStep), rayDist, botRenderer.sortingOrder);

        //line.SetPosition(0, gameObject.transform.position);
        //line.SetPosition(1, new Vector2(gameObject.transform.position.x + xStep, gameObject.transform.position.y + yStep));

        //Debug.Log("COUNT OF RAYCASTS: " + rays.Length);
        //for (int i = 0; i < rays.Length; i++)
        //{
        //    Debug.Log("MOVE RAYS: " + rays[i].collider.name);
        //}

        //RaycastHit2D triggerRay = Physics2D.Raycast(trigger.GetComponent<Transform>().position, new Vector2(xStep, yStep), rayDist, botRenderer.sortingOrder);
        //RaycastHit2D botRay = Physics2D.Raycast(gameObject.transform.position, new Vector2(xStep, yStep), rayDist, botRenderer.sortingOrder);

        //Debug.Log("Walk ray1 " + ray1.collider.name);
        //Debug.Log("BOT ray = " + botRay.collider.name);


        //if (ray2.collider != null)
        //{
        //if (botRay.collider.name == triggerRay.collider.name)
        //if(rays.Length < 2)
        if (botRenderer.GetComponent<SortingGroup>().sortingOrder < 2)
            target = new Vector3(target.x + xStep, target.y + yStep);
        //}
        //else
        //{
            animator.SetBool("isWalking", true);
        
            //animator.SetTrigger("WalkingTrigger");
            //StartCoroutine(Delay());
        //}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //botCollision2floor = false;
        //if (collision.name == "2_floor")
        //{
        //    botCollision2floor = true;
        //    Debug.Log("TRUE COOLISION 2 floor");
        //}
        //Debug.Log("Collider BOT: " + collision.name);
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
            //Debug.Log("Collider entered");
            target = new Vector2(target.x - xStep, target.y - yStep);
        }

        if (collision.tag == "Green")
        {
            if (botRenderer.GetComponent<SortingGroup>().sortingOrder == 1)
                target = new Vector2(target.x - xStep, target.y - yStep);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Green")
            isPlaceForTree = false;
        //isReadyToPlant = false;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(walkDelay);
        animator.SetBool("isWalking", false);
    }
}
