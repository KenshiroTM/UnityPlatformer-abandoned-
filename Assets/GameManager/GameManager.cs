using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // game manager monitoruje stan gry, w tym wypadku b�dzie to monitorowanie znajdziek i czy level przeszed� kto� czy umar

    public static GameManager Instance;
    
    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    //TODO: narazie publiczne, potem zmiana
    public int currentLevelScore = 0;
    public float levelCompletionTimeInSecs = 0f;

    //wartosci do timera
    private const float tickvalue = 1; //tutaj co ile sekund ma byc tick
    private float delayTime = 0;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this; // to jest instancj� jak nie istnieje ju� taka
            DontDestroyOnLoad(gameObject); // obiekt przechodzi przez inne sceny bez bycia zniszczonym
        }
        else
        {
            Destroy(gameObject); // w innym wypadku zniszcz obiekt bo jest ju� taki co nie musi by� niszczony
        }
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        
        switch (newState) //przy ka�dym zmianie gry odpowiedni handler
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.GameOngoing:
                HandleGameOngoing();
                break;
            case GameState.LevelCompleted:
                HandleLevelCompleted();
                break;
            case GameState.GamePaused:
                HandleGamePaused();
                break;
            case GameState.PlayerLoses:
                HandlePlayerLoses();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null); //rzuca b��dem jak co� sie popsuje
        }
        OnGameStateChanged?.Invoke(newState); // gdy state sie zmienia to zanotuj to w innych plikach .cs dziel�cych t� funkcj�
        //pytajnik po to aby pozwoli� mu nie mie� �adnych funkcji dziel�cych od niego
        Debug.Log(Instance.State.ToString());
    }

    //handlery b�d�ce globalnym zdarzeniem przy zmianie stanu gry
    private void HandleGamePaused()
    {
        Time.timeScale = 0f;
    }

    private void HandleGameOngoing()
    {
        Time.timeScale = 1f;
    }

    private void HandleMainMenu()
    {
        Time.timeScale = 0f;
    }

    private void HandleLevelCompleted()
    {
        Time.timeScale = 0f;
    }

    private void HandlePlayerLoses()
    {
        Time.timeScale = 0f;
    }

    public void ClearLevelProgress()
    {
        currentLevelScore = 0; //ilosc zdobytych diamencikow
        levelCompletionTimeInSecs = 0; //czas przejscia poziomu
    }

    public void TimerTick() // tickowanie do timera w grze
    {
        if(delayTime <= Time.time)
        {
            delayTime = tickvalue + Time.time;
            levelCompletionTimeInSecs++;
        }
    }

}
// MUSI BY� POZA OBIEKTEM, definicje stany gry, czyli czy gracz wygra�, pauzuje lub jest w menu
public enum GameState
{
    MainMenu,
    GameOngoing,
    LevelCompleted,
    GamePaused,
    PlayerLoses
}