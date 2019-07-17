using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    CommandPanel commandsPanel; // Our current panel

    //public int characterDirection; // Where the character look at
    public float delay = 0.1f;
    public float jumpDelay = 0.5f;
        // Game Objects
    public GameObject tree;
    public GameObject bot;
    public GameObject botGraphic;

    // OTHER SRIPTS
    private BotRotation botRotationScript;
    private BotJumping botJumpingScript;

    public static bool isCommandDone = false;
    public bool plantTreeCommanded = false; // Var for tree planting
    private Vector2 firstBotPos;
    private int firstBotDirection;
    private int firstBotLayer;

    private int finishedMainCommands = 0;
    private IEnumerator mainCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        commandsPanel = CommandPanel.instance; // List of our commands

        firstBotPos = bot.GetComponent<Transform>().position;
        firstBotDirection = bot.GetComponent<BotRotation>().botDirection;
        firstBotLayer = botGraphic.GetComponent<SpriteRenderer>().sortingOrder;

        // Take other gameobject's scripts
        botRotationScript = bot.GetComponent<BotRotation>();
        botJumpingScript = bot.GetComponent<BotJumping>();
    }

        // RETRY button
    public void RetryPressed()
    {
            //Reset position of character
        bot.transform.position = firstBotPos;
        bot.GetComponent<BotController>().target = firstBotPos;
        bot.GetComponent<BotRotation>().botDirection = firstBotDirection;
        bot.GetComponent<BotRotation>().SetDirectionOfBotMovement();
        botGraphic.GetComponent<SpriteRenderer>().sortingOrder = firstBotLayer;
        StopAllCoroutines();
        botGraphic.GetComponent<Animator>().Rebind();
        bot.GetComponent<BotJumping>().jumped = false;
        BotController.isJump = false;

        plantTreeCommanded = false; // Reset command

            //Destroy all instantiated trees
        GameObject[] palms = GameObject.FindGameObjectsWithTag("Palm");
        for (int i = 0; i < palms.Length; i++)
            Destroy(palms[i]);
    }
        // PLAY button
    public void PlayPressed()
    {
        finishedMainCommands = 0;
        mainCoroutine = ReadCommands(commandsPanel.commands, true, 0);
        StartCoroutine(mainCoroutine);
    }

    #region Commands
    IEnumerator ReadCommands(List<Command> commands, bool isMain, int startIndex)
    {
        //Read commands
        for (int i = startIndex; i < commands.Count; i++)
        {
            yield return new WaitUntil(IsCommandFinished); // Wait until past command is finished and then go to next iteration
            //Debug.Log("List:" + commandsPanel.commands[i]);

            // GO COMMAND
            if (commands[i].name == "Go")
            {
                bot.GetComponent<BotController>().Move();
                isCommandDone = false;
            }
            // PLANT COMMAND
            if (commands[i].name == "Plant")
            {
                yield return new WaitForSeconds(delay);
                Plant();
                isCommandDone = false;
            }

            // ROTATE LEFT command
            if (commands[i].name == "RotateLeft")
            {
                //Call method to rotate
                isCommandDone = false;
                botRotationScript.Rotate("left");
                Debug.Log("Rotate Left method is called");
                yield return new WaitForSeconds(delay);
            }

            // ROTATE RIGHT command
            if (commands[i].name == "RotateRight")
            {
                isCommandDone = false;
                botRotationScript.Rotate("right");
                Debug.Log("Rotate Right method is called");
                yield return new WaitForSeconds(delay);
            }

            // JUMP command
            if (commands[i].name == "Jump")
            {
                isCommandDone = false;
                botJumpingScript.Jump();
                Debug.Log("Jump command");
                //BotController.isJump = true;
                yield return new WaitForSeconds(jumpDelay);
                //yield return new WaitForSeconds(delay);
            }

            // PROC1 command
            if (commands[i].name == "PROC1")
            {
                isCommandDone = false;
                Debug.Log("PROC1 is commanded");
                yield return new WaitForSeconds(delay);
                StartCoroutine(ReadCommands(commandsPanel.commandsProc1, false, 0));
                if (isMain)
                    finishedMainCommands++;
                StopCoroutine(mainCoroutine);
                break;
            }

            // PROC2 command
            if (commands[i].name == "PROC2")
            {
                isCommandDone = false;
                Debug.Log("PROC2 is commanded");
                yield return new WaitForSeconds(delay);
                StartCoroutine(ReadCommands(commandsPanel.commandsProc2, false, 0));
                if (isMain)
                    finishedMainCommands++;
                StopCoroutine(mainCoroutine);
                break;
            }

            if (isMain == true)
                finishedMainCommands++;
        }

        Debug.Log("finishedMainCommands = " + finishedMainCommands + "/ " + commandsPanel.commands.Count);
        
            if (finishedMainCommands < commandsPanel.commands.Count)
            {

                yield return new WaitForSeconds(delay);
                //StartCoroutine(ReadCommands(commandsPanel.commands, true, finishedMainCommands));
                mainCoroutine = ReadCommands(commandsPanel.commands, true, finishedMainCommands);
                StartCoroutine(mainCoroutine);
        }
            Debug.Log("All commands are finished");
    }
    #endregion

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
