using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CommandsReading : MonoBehaviour
{
    CommandPanel commandsPanel; // Our current panel

    public int characterDirection; // Where the character look at
    private Tilemap characterTile; // Character's tile
    private Vector3 firstCharacterPos; // First position of the character

    public GameObject tree;


    // Start is called before the first frame update
    void Start()
    {
        commandsPanel = CommandPanel.instance;

        characterTile = gameObject.GetComponent<Tilemap>();

        firstCharacterPos = new Vector2(characterTile.tileAnchor.x, characterTile.tileAnchor.y); // Remember first position of the charactor
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RetryPressed()
    {
        characterTile.tileAnchor = new Vector2(firstCharacterPos.x, firstCharacterPos.y); // Set the character where it was
        //Destroy all instantiated trees
    }

    public void PlayPressed()
    {
        for (int i = 0; i < commandsPanel.commands.Count; i++)
        {
            //Debug.Log("List:" + commandsPanel.commands[i]);
            if (commandsPanel.commands[i].name == "Go")
                GoForward();
            if (commandsPanel.commands[i].name == "Plant")
                Plant();
        }
    }

    public void GoForward()
    {
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
