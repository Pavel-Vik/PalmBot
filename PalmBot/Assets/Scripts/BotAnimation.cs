﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is for showing bot direction and set information into the Animator
/// </summary>

public class BotAnimation : MonoBehaviour
{
    private int botDirection;
    private Vector3 botScale;
    private Animator anim;
    // Start is called before the first frame update

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        botScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        botDirection = gameObject.GetComponentInParent<BotRotation>().botDirection;

        if (botDirection == 0) // DownRight
        {
            // Multiply the player's x local scale by -1.
            botScale.x = Mathf.Abs(botScale.x);
            anim.SetBool("isBack", false);
        }
        else if (botDirection == 1) // DownLeft
        {
            botScale.x = Mathf.Abs(botScale.x) * -1;
            anim.SetBool("isBack", false);
        }
        else if (botDirection == 2) // UpLeft
        {
            botScale.x = Mathf.Abs(botScale.x) * -1;
            anim.SetBool("isBack", true);
        }
        else if (botDirection == 3) // UpRight
        {
            botScale.x = Mathf.Abs(botScale.x);
            anim.SetBool("isBack", true);
        }

        transform.localScale = botScale;
    }
}
