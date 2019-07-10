using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotRotation : MonoBehaviour
{
    [Tooltip("0 - DownRight, 1 - DownLeft, 2 - UpLeft, 3 - UpRight")]
    [Range(0, 3)]
    public int botDirection = 0; //0 - DownRight, 1 - DownLeft, 2 - UpLeft, 3 - UpRight
    [Tooltip("How much time this command takes")]
    //public float delay = 1f;

    //public Sprite DownRight;
    //public Sprite UpLeft;

    private float xStep, yStep;

    private void Start()
    {
        xStep = gameObject.GetComponent<BotController>().xStep;
        yStep = gameObject.GetComponent<BotController>().yStep;

        SetDirectionOfBotMovement();
    }

    public void SetDirectionOfBotMovement()
    {
        if (botDirection == 0) // DownRight
        {
            xStep = Mathf.Abs(xStep);
            yStep = Mathf.Abs(yStep) * -1f;
        }
        else if (botDirection == 1) // DownLeft
        {
            xStep = Mathf.Abs(xStep) * -1f;
            yStep = Mathf.Abs(yStep) * -1f;
        }
        else if (botDirection == 2) // UpLeft
        {
            xStep = Mathf.Abs(xStep) * -1f;
            yStep = Mathf.Abs(yStep);
        }
        else if (botDirection == 3) // UpRight
        {
            xStep = Mathf.Abs(xStep);
            yStep = Mathf.Abs(yStep);
        }


        // Change values
        gameObject.GetComponent<BotController>().xStep = xStep;
        gameObject.GetComponent<BotController>().yStep = yStep;
    }

    public void Rotate(string rotationDir)
    {
        // ROTATE RIGHT
        if (rotationDir == "right")
        {
            Debug.Log("Rotate Right");
            botDirection++;
        }
        // ROTATE LEFT
        else if (rotationDir == "left")
        {
            Debug.Log("Rotate Left");
            botDirection--;
        }

        // Fix bot direction from 0 to 3
        if (botDirection > 3)
            botDirection = 0;
        if (botDirection < 0)
            botDirection = 3;

        GameController.isCommandDone = true;

        SetDirectionOfBotMovement();
        //StartCoroutine(Delay());
    }

    //IEnumerator Delay()
    //{
    //    yield return new WaitForSeconds(delay);
    //    GameController.isCommandDone = true;
    //}
}
