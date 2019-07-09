using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotRotation : MonoBehaviour
{
    public int botDirection = 0; //0 - SE, 1 - SW, 2 - NW, 3 - NE

    private float xStep, yStep;

    private void Start()
    {
        xStep = gameObject.GetComponent<BotController>().xStep;
        yStep = gameObject.GetComponent<BotController>().yStep;

        SetDirectionOfBotMovement();
    }

    public void SetDirectionOfBotMovement()
    {
        if (botDirection == 0) // SE
        {
            xStep = Mathf.Abs(xStep);
            yStep = Mathf.Abs(yStep) * -1f;
        }
        else if (botDirection == 1) // SW
        {
            xStep = Mathf.Abs(xStep) * -1f;
            yStep = Mathf.Abs(yStep) * -1f;
        }
        else if (botDirection == 2) // NW
        {
            xStep = Mathf.Abs(xStep) * -1f;
            yStep = Mathf.Abs(yStep);
        }
        else if (botDirection == 3) // NE
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
    }
}
