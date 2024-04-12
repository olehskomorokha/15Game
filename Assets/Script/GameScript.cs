using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    [SerializeField] private GameObject emptySpace = null;
    private Camera _camera;
    [SerializeField] private Camera _cameraTarget;
    [SerializeField] private TilesScript[] tiles = null;
    private int emptySpaceIndex = 15;
    private bool _isFinished;
    [SerializeField] private GameObject endPanel = null, newRecordText = null;
    [SerializeField] Text endPanelTimerText = null, bestTimeText = null;
    [SerializeField] Text OperationText;
    public int operation;
    void Start()
    {
        _camera = Camera.main;
        Shuffle();
    }

 
    void Update()
    {
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
                            operation++;
                            OperationText.text = "Крок : " + operation.ToString();
                        }
                    }
                }
            }
        }
        if (!_isFinished)
        {
            int correctTiles = 0;
            foreach (var a in tiles)
            {
                if (a != null)
                {
                    if (a.inRightPlace)
                    {
                        correctTiles++;
                    }
                }
            }
            if (correctTiles == tiles.Length - 1)
            {
                _isFinished = true;
                endPanel.SetActive(true);
                var a = GetComponent<TimeScript>();
                a.StopTimer();
                endPanelTimerText.text = (a.minutes < 10 ? "0" : "") + a.minutes + ":" + (a.seconds < 10 ? "0" : "") + a.seconds;
                int bestTime;
                if (PlayerPrefs.HasKey("bestTime"))
                {
                    bestTime = PlayerPrefs.GetInt("bestTime");
                }
                else
                {
                    bestTime = 999999;
                }
                int playerTime = a.minutes * 60 + a.seconds;
                if (playerTime < bestTime) 
                {
                    newRecordText.SetActive(true);
                    PlayerPrefs.SetInt("bestTime", playerTime);
                }
                else
                {
                    int minutes = bestTime / 60;
                    int seconds = bestTime - minutes * 60;
                    bestTimeText.text = (minutes < 10 ? "0" : "") + minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
                    bestTimeText.transform.parent.gameObject.SetActive(true);
                }
            }
        }
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject GameMenu;
    public void StopGame() 
    {
        pauseMenu.SetActive(true);
        GameMenu.SetActive(false);
        var timer = GetComponent<TimeScript>();
        if (timer != null)
            timer.StopTimer();
    }
    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        GameMenu.SetActive(true);
        var timer = GetComponent<TimeScript>();
        timer.ContinueTimer();

    }
    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Shuffle()
    {
        if (emptySpaceIndex != 15)
        {
            var tileOn15LastPos = tiles[15].targetPosition;
            tiles[15].targetPosition = emptySpace.transform.position;
            emptySpace.transform.position = tileOn15LastPos;
            tiles[emptySpaceIndex] = tiles[15];
            tiles[15] = null;
            emptySpaceIndex = 15;
        }
        int invertion;
        do
        {
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
            invertion = GetInversions();
            Debug.Log("Shuffle" + invertion);
        } while (invertion % 2 != 0);
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
        for (int i = 0; i < tiles.Length - 1; i++)
        {
            int thisTileInversion = 0;
            for (int j = i + 1; j < tiles.Length; j++)
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
