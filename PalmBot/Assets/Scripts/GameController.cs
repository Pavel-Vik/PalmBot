using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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

    [Header("Finish Info")]
    public int plantedPalmsCount = 0;
    public bool isLevelCompleted = false;

    [Header("LEVEL SETTINGS")]
    public int levelSectionID = 1;
    public int greenTilesCount = 1;

    // OTHER SRIPTS
    private BotRotation botRotationScript;
    private BotJumping botJumpingScript;

    public static bool isCommandDone = false;
    private bool plantTreeCommanded = false; // Var for tree planting
    private Vector3 firstBotPos;
    private int firstBotDirection;
    private int firstBotLayer;

    private int finishedMainCommands = 0;
    private IEnumerator mainCoroutine;
    private int palmId = 0;

    // Start is called before the first frame update
    void Start()
    {
        commandsPanel = CommandPanel.instance; // List of our commands

        firstBotPos = bot.GetComponent<Transform>().position;
        firstBotDirection = bot.GetComponent<BotRotation>().botDirection;
        firstBotLayer = bot.GetComponentInChildren<Trigger>().floorToCheckForWalking;

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
        botGraphic.GetComponent<SortingGroup>().sortingOrder = firstBotLayer;
        StopAllCoroutines();
        botGraphic.GetComponent<Animator>().Rebind();
        BotController.isJump = false;
        bot.GetComponentInChildren<Trigger>().floorToCheckForWalking = firstBotLayer;
        gameObject.GetComponent<ManagerUI>().ShowNextLevelButton(false);

        isLevelCompleted = false;
        plantedPalmsCount = 0;
        palmId = 0;
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

        if (BotCollider.isGreenTile == true && BotCollider.isPalmHere == false)
        {
            GameObject newPalm;
            newPalm = Instantiate(tree, bot.transform.position, Quaternion.identity);
            plantedPalmsCount++;

            newPalm.name = "Palm" + palmId.ToString();
            palmId++;

            // Last palm planted
            if (plantedPalmsCount == greenTilesCount)
                isLevelCompleted = true;
            else
                isLevelCompleted = false;

            // WIN CHECKING
            if (isLevelCompleted == true)
            {
                gameObject.GetComponent<ManagerUI>().ShowNextLevelButton(true);
                StopAllCoroutines();
            }
        }

        else if (BotCollider.isGreenTile == true && BotCollider.isPalmHere == true)
        {
            GameObject palm = GameObject.Find(BotCollider.palmNameCollidingWith);
            Destroy(palm);
            plantedPalmsCount--;
        }
    }
}
