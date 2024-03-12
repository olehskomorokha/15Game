using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float step = 0.1f;
    public float distance = 60f;
    private Vector2 startPosition;
    private Vector2 endPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
    }

    void MoveUp()
    {
        endPosition = startPosition + new Vector2(0, distance);
        transform.position = endPosition;
        startPosition = endPosition;
    }

    void MoveDown()
    {
        endPosition = startPosition - new Vector2(0, distance);
        transform.position = endPosition;
        startPosition = endPosition;
    }

    void MoveLeft()
    {
        endPosition = startPosition - new Vector2(distance, 0);
        transform.position = endPosition;
        startPosition = endPosition;
    }

    void MoveRight()
    {
        endPosition = startPosition + new Vector2(distance, 0);
        transform.position = endPosition;
        startPosition = endPosition;
    }
}
