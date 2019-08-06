using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRotation : MonoBehaviour
{
    private float xStep, yStep;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        xStep = GetComponentInParent<BotController>().xStep;
        yStep = GetComponentInParent<BotController>().yStep;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(xStep, yStep);
    }
}
