using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    [SerializeField] private GameObject emptySpace = null;
    private Camera _camera;
    [SerializeField] private Camera _cameraTarget;
    [SerializeField] private TilesScript[] tiles;
    private int emptySpaceIndex = 15;
    void Start()
    {
        _camera = Camera.main;
        Shuffle();
    }

 
    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        //    if (hit)
        //    {
        //        if (Vector2.Distance(emptySpace.transform.position, hit.transform.position) < 60)
        //        {
        //            Vector2 lastEmptyPosition = emptySpace.transform.position;
        //            TilesScript thisTile = hit.transform.GetComponent<TilesScript>();
        //            emptySpace.transform.position = thisTile.transform.position;
        //            thisTile.targetPosition = lastEmptyPosition;
        //            int tileIndex = findIndex(thisTile);
        //            tiles[emptySpaceIndex] = tiles[tileIndex];
        //            tiles[tileIndex] = null;
        //            emptySpaceIndex = tileIndex;
        //        }
        //    }
        //}
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                // Перевірка чи було клацнуто на не пусті клітини
                if (hit.transform != null && hit.transform.gameObject != null)
                {
                    // Перевірка чи не пуста клітина була клацнута
                    if (hit.transform.gameObject != emptySpace)
                    {
                        if (Vector2.Distance(emptySpace.transform.position, hit.transform.position) < 60f)
                        {
                            Vector2 lastEmptyPosition = emptySpace.transform.position;
                            TilesScript thisTile = hit.transform.GetComponent<TilesScript>();
                            emptySpace.transform.position = thisTile.transform.position;
                            thisTile.targetPosition = lastEmptyPosition;
                            int tileIndex = findIndex(thisTile);
                            tiles[emptySpaceIndex] = tiles[tileIndex];
                            tiles[tileIndex] = null;
                            emptySpaceIndex = tileIndex;
                        }
                    }
                }
            }
        }
        int correctTiles = 0;
        foreach (var a in tiles)
        {
            if(a != null)
            {
                if (a.inRightPlace)
                {
                    correctTiles++;
                }
            }
        }
        if (correctTiles == tiles.Length - 1)
        {
            Debug.Log("YOU WON");
        }
    }
    public void Shuffle()
    {
        //if (emptySpaceIndex != 15)
        //{
        //    var tileOn15LastPos = tiles[15].targetPosition;
        //    tiles[15].targetPosition = emptySpace.transform.position;
        //    emptySpace.transform.position = tileOn15LastPos;
        //    tiles[emptySpaceIndex] = tiles[15];
        //    tiles[15] = null;
        //    emptySpaceIndex = 15;
        //}
        //int inversion;
        for (int i = 0; i <= 14; i++)
        {
            var lastPos = tiles[i].targetPosition;
            int randomIndex = Random.Range(0, 14);
            tiles[i].targetPosition = tiles[randomIndex].targetPosition;
            tiles[randomIndex].targetPosition = lastPos;
            var tile = tiles[i];
            tiles[i] = tiles[randomIndex];
            tiles[randomIndex] = tile;
        }
    }
    public int findIndex(TilesScript ts)
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    public int GetInversions()
    {
        int inversionsSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            int thisTileInversion = 0;
            for (int j = 0; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInversion++;
                    }
                }
            }
            inversionsSum += thisTileInversion;
        }   
        return inversionsSum;
    }
}
