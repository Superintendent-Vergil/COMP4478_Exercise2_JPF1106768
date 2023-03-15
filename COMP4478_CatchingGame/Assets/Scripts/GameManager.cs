using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Variables
    
    //Public
    public static GameManager Instance;
    
    public GameState state;
    public static int score;
    public static event Action<GameState> OnGameStateChanged;
    public AudioSource musicBG, musicGameOver;
    
    //Private
    [SerializeField] private GameObject UI_gamePlaying, UI_gameOver, collectableSpawner;
    [SerializeField] private TMP_Text finalScoreText;
    private PlayerController _player;

    private void Awake()
    {
        //Initialize variables
        Instance = this;
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        Application.targetFrameRate = 60; //Set target framerate
        UpdateGameState(GameState.GameStart); //Set game state to start
    }

    public void UpdateGameState(GameState newState) //Updates the game state
    {
        state = newState;
        switch (newState)
        {
            case GameState.GameStart:
                GameStart();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnGameStateChanged?.Invoke(newState);
    }

    void GameStart() //Game Started
    {
        //Turn on game objects
        _player.gameObject.SetActive(true);
        collectableSpawner.SetActive(true);
        //Update UI
        UI_gamePlaying.SetActive(true);
        UI_gameOver.SetActive(false);
        
        score = 0; //Reset score
        Time.timeScale = 1; //Reset timescale
        musicBG.Play(); //Play music
    }
    
    void GameOver() //Ends the game
    {
        //Disable objects
        _player.gameObject.SetActive(false);
        collectableSpawner.SetActive(false);
        //Update UI
        UI_gamePlaying.SetActive(false);
        UI_gameOver.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
        
        Time.timeScale = 0; //Stop game time
        //Play game over music
        musicGameOver.PlayDelayed(0.5f);
        musicBG.Stop();
    }

    public void RestartGame() //Restarts the game by reloading the scene
    {
        SceneManager.LoadScene("Game Scene");
    }
}

public enum GameState //Game states
{
    GameStart,
    GameOver
}
