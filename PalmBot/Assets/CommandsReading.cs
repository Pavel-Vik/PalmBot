using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CommandsReading : MonoBehaviour
{
    CommandPanel commandsPanel; // Our current panel

    public int characterDirection; // Where the character look at
    private Tilemap characterTile; // Character's tile
    private Vector2 firstCharacterPos; // First position of the character
    private Vector2 nextCharacterPos;

    private bool ready = false;

    public GameObject tree;

    public float movementSpeed = 0;

    private int step = 0;


    // Start is called before the first frame update
    void Start()
    {
        commandsPanel = CommandPanel.instance;

        characterTile = gameObject.GetComponent<Tilemap>();

        firstCharacterPos = new Vector2(characterTile.tileAnchor.x, characterTile.tileAnchor.y); // Remember first position of the character
        //nextCharacterPos = firstCharacterPos;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (ready == true)
        //    characterTile.tileAnchor = new Vector2(characterTile.tileAnchor.x, Mathf.Lerp(firstCharacterPos.y, nextCharacterPos.y, movementSpeed * Time.deltaTime));
        ////for (int i = 0; i < 10; i ++)
        //if (characterTile.tileAnchor.y > nextCharacterPos.y)
        //{
        //    characterTile.tileAnchor = new Vector2(nextCharacterPos.x, nextCharacterPos.y - movementSpeed);
        //    movementSpeed += 0.1f;
        //}
    }

    public void RetryPressed()
    {
        ready = false;
        step = 0;
        characterTile.tileAnchor = new Vector2(firstCharacterPos.x, firstCharacterPos.y); // Set the character where it was

        //Destroy all instantiated trees
        GameObject[] palms = GameObject.FindGameObjectsWithTag("Palm");
        for (int i = 0; i < palms.Length; i++)
            Destroy(palms[i]);
    }

    public void PlayPressed()
    {
        ready = true;
        for (int i = 0; i < commandsPanel.commands.Count; i++)
        {
            //Debug.Log("List:" + commandsPanel.commands[i]);
            if (commandsPanel.commands[i].name == "Go")
            {
                //if (ready == true)
                //{
                if (step < 2)
                {
                    step++;
                    GoForward();
                }
                //    StartCoroutine(Waiting());
                //}
            }
            if (commandsPanel.commands[i].name == "Plant")
                if (step == 2)
                    Plant();
        }
    }

    public void GoForward()
    {
        nextCharacterPos = new Vector2(characterTile.tileAnchor.x, characterTile.tileAnchor.y - 1);
       
        characterTile.tileAnchor = new Vector2(characterTile.tileAnchor.x, characterTile.tileAnchor.y - 1); // Change position of tile of the character 
        // Move it right
        Debug.Log("GoForward function");
    }

    public void Plant()
    {
        Debug.Log("Plant function");
        Instantiate(tree);
    }
}
