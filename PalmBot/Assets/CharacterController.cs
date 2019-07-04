using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{       // Game objects
    public GameObject gameController;
    public GameObject tree;

        // Movement vars
    public float movementSpeed = 1f;
    public float xStep = -0.5f;
    public float yStep = 0.25f;
    public Vector2 target;

    Rigidbody2D rbody;
    public bool isReadyToPlant = false;

    private Vector2 finishCharacterPos;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        target = transform.position;
        finishCharacterPos = new Vector2(2, 4);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Move();

        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
    }

    public void Move()
    {
        target = new Vector2(target.x + xStep, target.y + yStep);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Green")
        {
            Debug.Log("Green Zone entered");
            if (gameController.GetComponent<GameController>().plantTreeCommanded == true)
                Instantiate(tree);
        }
        if (collision.tag == "Collider")
        {
            Debug.Log("Collider entered");
            target = new Vector2(target.x - xStep, target.y - yStep);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isReadyToPlant = false;
    }
}
