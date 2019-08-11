using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BotJumping : MonoBehaviour
{
    private Vector3 target;
    private float movementSpeed;
    private int botDir;
    private bool canJump = false;
    private bool canJumpDown = false;

    public float changeLayerDelay = 0.2f;
    public float difBetweenLayersY = 0.2f;
    public float difBetweenLayersZ = 0.5f;

    public float jumpChangeTargetDelay = 0.3f;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = gameObject.GetComponent<BotController>().movementSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    public void Jump()
    {
        BotController.isJump = false;
        botDir = gameObject.GetComponent<BotRotation>().botDirection;

        anim.SetTrigger("Jumping");
        StartCoroutine(DelayAndJump());
    }


    IEnumerator DelayAndJump()
    {
        yield return new WaitForSeconds(jumpChangeTargetDelay);

        Vector2 botStep = new Vector2(gameObject.GetComponent<BotController>().xStep, gameObject.GetComponent<BotController>().yStep);
        Vector3 targ = gameObject.GetComponent<BotController>().target;


        // =========== Check trigger info for jumping UP
        if (GetComponentInChildren<Trigger>().isTileToJump == true)
            canJump = true;
        else
            canJump = false;

        // =========== Check trigger info for jumping DOWN
        if (GetComponentInChildren<Trigger>().isTileToJumpDown == true)
            canJumpDown = true;
        else
            canJumpDown = false;

        // JUMPING UP
        if (canJump == true)
        {
            gameObject.GetComponent<BotController>().target = new Vector3(targ.x + botStep.x, targ.y + botStep.y + difBetweenLayersY, targ.z + difBetweenLayersZ);
            BotController.isJump = true;
            gameObject.GetComponentInChildren<Trigger>().floorToCheckForWalking++;
        }

        // JUMPING DOWN
        if (canJumpDown == true)
        {
            //One floor down
            gameObject.GetComponent<BotController>().target = new Vector3(targ.x + botStep.x, targ.y + botStep.y - difBetweenLayersY, targ.z - difBetweenLayersZ);
            BotController.isJumpDown = true;
            gameObject.GetComponentInChildren<Trigger>().floorToCheckForWalking--;
        }
    }
}
