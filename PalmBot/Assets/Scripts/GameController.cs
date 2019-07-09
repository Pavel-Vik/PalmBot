using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    CommandPanel commandsPanel; // Our current panel

    //public int characterDirection; // Where the character look at

        // Game Objects
    public GameObject tree;
    public GameObject bot;
    private BotRotation botRotationScript;

    public static bool isCommandDone = false;
    public bool plantTreeCommanded = false; // Var for tree planting
    private Vector2 firstBotPos;
    private int firstBotDirection;

    // Start is called before the first frame update
    void Start()
    {
        commandsPanel = CommandPanel.instance; // List of our commands

        firstBotPos = bot.GetComponent<Transform>().position;
        firstBotDirection = bot.GetComponent<BotRotation>().botDirection;
        botRotationScript = bot.GetComponent<BotRotation>();
    }

        // RETRY button
    public void RetryPressed()
    {

            //Reset position of character
        bot.transform.position = firstBotPos;
        bot.GetComponent<BotController>().target = firstBotPos;
        bot.GetComponent<BotRotation>().botDirection = firstBotDirection;
        bot.GetComponent<BotRotation>().SetDirectionOfBotMovement();

        plantTreeCommanded = false; // Reset command

            //Destroy all instantiated trees
        GameObject[] palms = GameObject.FindGameObjectsWithTag("Palm");
        for (int i = 0; i < palms.Length; i++)
            Destroy(palms[i]);
    }
        // PLAY button
    public void PlayPressed()
    {
        StartCoroutine(WaitForCommandFinish());
    }
    
    IEnumerator WaitForCommandFinish()
    {
        //Read commands
        for (int i = 0; i < commandsPanel.commands.Count; i++)
        {
            yield return new WaitUntil(IsCommandFinished); // Wait until past command is finished and then go to next iteration

            //Debug.Log("List:" + commandsPanel.commands[i]);

            // GO COMMAND
            if (commandsPanel.commands[i].name == "Go")
            {
                bot.GetComponent<BotController>().Move();
                isCommandDone = false;
            }
            // PLANT COMMAND
            if (commandsPanel.commands[i].name == "Plant")
            {
                Plant();
                isCommandDone = false;
            }

            // ROTATE LEFT command
            if (commandsPanel.commands[i].name == "RotateLeft")
            {
                //Call method to rotate
                isCommandDone = false;
                botRotationScript.Rotate("left");
                Debug.Log("Rotate Left method is called");
            }

            // ROTATE RIGHT command
            if (commandsPanel.commands[i].name == "RotateRight")
            {
                isCommandDone = false;
                botRotationScript.Rotate("right");
                Debug.Log("Rotate Right method is called");
            }
        }
        Debug.Log("All commands are finished");
    }

    public bool IsCommandFinished()
    {
        if (isCommandDone == true)
            return true;
        else
            return false;
    }

        // Plant a tree
    public void Plant()
    {
        Debug.Log("Plant function");
        if (BotController.isPlaceForTree == true)
            Instantiate(tree, bot.transform.position, Quaternion.identity);
    }
}
