using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    [SerializeField] private GameObject emptySpace;
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
    }

 
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (Vector2.Distance(emptySpace.transform.position, hit.transform.position) < 60)
                {
                    Vector2 lastEmptyPosition = emptySpace.transform.position;
                    emptySpace.transform.position = hit.transform.position;
                    hit.transform.position = lastEmptyPosition;
                }
            }
        }
    }
}
