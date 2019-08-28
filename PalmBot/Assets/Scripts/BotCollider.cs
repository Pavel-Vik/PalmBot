using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for checking colliding with Green Tile and a Palm
/// </summary>

public class BotCollider : MonoBehaviour
{
    public string colide = null;
    public static bool isGreenTile = false;
    public static bool isPalmHere = false;
    public static string palmNameCollidingWith;
    // Start is called before the first frame update
    void Start()
    {
        isGreenTile = false;
        isPalmHere = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Green")
        {
            Debug.Log("Green Zone entered");
            isGreenTile = true;
        }

        if (collision.tag == "Palm")
        {
            isPalmHere = true;
            colide = collision.name;
            palmNameCollidingWith = collision.name;
        }

        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Green")
            isGreenTile = false;

        if (collision.tag == "Palm")
        {
            isPalmHere = false;
        }

        colide = null;
        palmNameCollidingWith = null;
    }
}
