using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesScript : MonoBehaviour
{
    public Vector3 targetPosition;
    /// for color check
    private Vector3 correctPosition;
    private SpriteRenderer _sprite;
    public int number;

    public bool inRightPlace;
    void Awake()
    {
        targetPosition = transform.position;
        /// for color check
        correctPosition = transform.position;
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);
        /// for color check
        float errorMargin = 30f;
        if (Vector3.Distance(targetPosition, correctPosition) < errorMargin)
        {
            _sprite.color = Color.green;
            inRightPlace = true;
        }
        else
        {
            _sprite.color = Color.white;
            inRightPlace = false;
        }

    }

 
}
