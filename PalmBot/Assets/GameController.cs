using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    CommandPanel commandsPanel; // Our current panel

    //public int characterDirection; // Where the character look at

        // Game Objects
    //public GameObject tree;
    public GameObject character;

    public bool plantTreeCommanded = false; // Var for tree planting
    private Vector2 firstPlayerPos;

    //Kostyli
    private int step = 0;

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
        step = 0; //KOSTYL!!!

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
            //Read commands
        for (int i = 0; i < commandsPanel.commands.Count; i++)
        {
            //Debug.Log("List:" + commandsPanel.commands[i]);
            if (commandsPanel.commands[i].name == "Go")
            {
                step++;
                character.GetComponent<CharacterController>().Move();
            }
            if (commandsPanel.commands[i].name == "Plant")
            {
                if (step == 2) // KOSTYL!!!
                    Plant();
            }
        }
    }

        // Plant a tree
    public void Plant()
    {
        Debug.Log("Plant function");
        plantTreeCommanded = true;
        //if (character.GetComponent<CharacterController>().isReadyToPlant == true)
        //Instantiate(tree);
    }
}
