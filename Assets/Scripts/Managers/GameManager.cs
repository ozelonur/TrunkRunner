using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private PlayerController player;
    private CanvasManager canvasManager;

    public Action gameOver;
    public Action gameComplete;

    public int BulletCount { get => PlayerPrefs.GetInt(Constants.BULLET_COUNT, 0); set => PlayerPrefs.SetInt(Constants.BULLET_COUNT, value); }
    public int DiamondCount { get => PlayerPrefs.GetInt(Constants.DIAMOND_COUNT, 0); set => PlayerPrefs.SetInt(Constants.DIAMOND_COUNT, value); }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        player = PlayerController.Instance;
        canvasManager = CanvasManager.Instance;

    }

    public void OnClick()
    {
        if (player.CurrentGameMode == GameMode.Start)
        {
            player.CurrentGameMode = GameMode.Playing;
            canvasManager.TapText.gameObject.SetActive(false);
        }
        else if (player.CurrentGameMode == GameMode.Dead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
