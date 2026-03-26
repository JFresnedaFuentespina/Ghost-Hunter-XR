using System.Collections;
using Meta.XR.MRUtilityKit;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject canvas;
    public TextMeshProUGUI message;    
    public GhostSpawner ghostSpawner;
    public GhostCounterDeaths ghostCounterDeaths;
    public OrbSpawner orbSpawner;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(ghostCounterDeaths.deathsCounter >= ghostSpawner.maxGhosts)
        {
            ShowVictory();
        }

        if(orbSpawner.orbsAlive.Count == 0 && orbSpawner.orbsSpawned)
        {
            ShowDefeat();
        }
    }

    public void ShowCanvas()
    {
        Debug.Log("SHOWING CANVAS END");
        canvas.SetActive(true);
    }

    public void ShowDefeat()
    {
        Debug.Log("GAME OVER");
        ShowCanvas();
        message.text = "Game Over!\nCargando...";
        StartCoroutine(RestartGame());
    }

    public void ShowVictory()
    {
        Debug.Log("VICTORY!");
        ShowCanvas();
        message.text = "Victory!\nCargando...";
        StartCoroutine(RestartGame());
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
