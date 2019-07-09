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
    public GameObject character;

    public static bool isCommandDone = false;
    public bool plantTreeCommanded = false; // Var for tree planting
    private Vector2 firstPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        commandsPanel = CommandPanel.instance; // List of our commands

        // CharacterMove.cs
        firstPlayerPos = character.GetComponent<Transform>().position;
    }

        // RETRY button
    public void RetryPressed()
    {

            //Reset position of character
        character.transform.position = firstPlayerPos;
        character.GetComponent<CharacterController>().target = firstPlayerPos;

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
                character.GetComponent<CharacterController>().Move();
                isCommandDone = false;
            }
            // PLANT COMMAND
            if (commandsPanel.commands[i].name == "Plant")
            {
                Plant();
                isCommandDone = false;
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

        Instantiate(tree); //!!!!!!!!!!   SET POSITION !!!!!!!!!!!!!!
    }
}
