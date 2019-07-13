using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotJumping : MonoBehaviour
{
    private Vector3 target;
    private float xStep, yStep, movementSpeed;
    private bool isJumping = false;

    public Animator anim;
    public GameObject trigger;
    public SpriteRenderer botRenderer;



    // Start is called before the first frame update
    void Start()
    {
        //anim = gameObject.GetComponent<Animator>();
        movementSpeed = gameObject.GetComponent<BotController>().movementSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (isJumping == true)
        //    transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);

        //if (transform.position == target)
        //    isJumping = false;
    }

    public void Jump()
    {
        anim.SetTrigger("Jumping");
        Vector2 botStep = new Vector2(gameObject.GetComponent<BotController>().xStep, gameObject.GetComponent<BotController>().yStep);
        float dist = 0.5f;
        RaycastHit2D ray = Physics2D.Raycast(gameObject.transform.position, botStep, dist);

        Vector3 targ = gameObject.GetComponent<BotController>().target;

        if (ray.collider != null)
        {
            //trigger.GetComponent<Transform>().position = botDirection;
            gameObject.GetComponent<BotController>().target = new Vector3(targ.x + botStep.x * 1.6f, targ.y + botStep.y * 1.6f);
            botRenderer.sortingOrder++;

            Debug.Log("Raycast collider name: " + ray.collider.name);
            Debug.Log("Raycast collider dist: " + ray.distance);
            //if we have a block before and sorting layer +1 then we set target
        }

        //xStep = gameObject.GetComponent<BotController>().xStep;
        //yStep = gameObject.GetComponent<BotController>().yStep;
        //anim.SetBool("isJumping", false);
        //target = new Vector3(target.x + xStep, target.y + yStep);
        //isJumping = true;
    }
}
