using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStateType
{
    None,
    Ready,
    Playing,
    Menu,
    MenuOut,
    GameOver,
    StageClear,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake() => Instance = this;

    GameStateType preGameState;
    GameStateType m_GameState;
    public GameStateType GameState
    {
        get => m_GameState;
        set
        {
            if (m_GameState == value)
                return;

            switch (value)
            {
                case GameStateType.Ready:
                case GameStateType.Playing:
                    Time.timeScale = 1;
                    break;
                case GameStateType.Menu:
                    preGameState = m_GameState;
                    Time.timeScale = 0;
                    break;
                case GameStateType.MenuOut:
                    value = preGameState;
                    Time.timeScale = 1;
                    break;
                case GameStateType.GameOver:
                case GameStateType.StageClear:
                    Time.timeScale = 0;
                    break;
            }
            print($"GameState : {m_GameState} -> {value}, TimeScale : {Time.timeScale}");
            m_GameState = value;
        }
    }


    [SerializeField] int readyTime = 3;
    IEnumerator Start()
    {
        GameState = GameStateType.Ready;
        yield return new WaitForSeconds(readyTime);
        GameState = GameStateType.Playing;
    }

    void Update()
    {
        if (GameState == GameStateType.GameOver)
            return;

        Menu();
        IsGameOver();
    }

    void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            MenuUI.Instance.VolumeButton();
    }

    float gameOverDistance = 20;
    void IsGameOver()
    {
        // 게임 오버 조건
        // 1. 캐릭터가 멀어지면
        if (Vector2.Distance(Camera.main.transform.position
            , Player.Instance.transform.position) > gameOverDistance)
        {
            GameState = GameStateType.GameOver;
            GameOverAndClearUI.Instance.ShowUI(GameStateType.GameOver);
        }
    }

    internal void StageEnd()
    {
        GameState = GameStateType.StageClear;
        GameOverAndClearUI.Instance.ShowUI(GameStateType.StageClear);
    }

    internal void ReStartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void AddScore(int value)
    {
        //ScoreUI.Instance.AddScore(value);
    }
}
