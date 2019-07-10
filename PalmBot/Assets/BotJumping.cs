using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotJumping : MonoBehaviour
{
    private Vector3 target;
    private float xStep, yStep, movementSpeed;
    private bool isJumping = false;

    private Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
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
        anim.SetBool("isJumping", true);
        //xStep = gameObject.GetComponent<BotController>().xStep;
        //yStep = gameObject.GetComponent<BotController>().yStep;

        //target = new Vector3(target.x + xStep, target.y + yStep);
        //isJumping = true;
    }
}
