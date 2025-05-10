using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject ongoingGameUI;
    public GameObject pauseMenuUI;
    public GameObject playerLosesUI;
    public GameObject levelCompletedUI;
    public Text scoreAmount;
    public Text timeAmount;
    public Text scoreToGet;
    public Text timeToBeat;

    public Image[] stars;

    public Text timerDisplay;

    int scoretoget;
    public int timetobeat;

    private int completionStars;
    bool levelCompleted;

    public void Start()
    {
        scoretoget = 0;
        completionStars = 0;
        scoretoget = GameObject.FindGameObjectsWithTag("Collectible").Length; //branie wszystkiech obiektów z danym tagiem
        levelCompleted = false;

        GameManager.Instance.UpdateGameState(GameState.GameOngoing); //zmiana state na ongoing
        GameManager.Instance.ClearLevelProgress(); //clearowanie progresji wczesniejszego poziomu (czas i score)
    }

    void Update()
    {
        //gdy gamestate bêdzie LevelCompleted to poka¿ UI
        if (GameManager.Instance.State == GameState.LevelCompleted && levelCompleted == false)
        {
            levelCompleted = true; //aby sie raz wykona³ levelCompleted

            scoreAmount.text = GameManager.Instance.currentLevelScore.ToString(); //przypisanie ile zdobyto diamencikow do score
            timeAmount.text = GameManager.Instance.levelCompletionTimeInSecs.ToString(); // czas w ktorym przeszlo sie gre

            //pokazywanie ile trzeba zrobiæ aby dostaægwiazdki
            scoreToGet.text = "/" + scoretoget;
            timeToBeat.text = "/" + timetobeat.ToString();

            //komplecja gwiazdek
            completionStars++;
            if(scoretoget == GameManager.Instance.currentLevelScore) { completionStars++; }
            if(timetobeat >= GameManager.Instance.levelCompletionTimeInSecs) { completionStars++; }

            //kolorowanie gwiazdek zale¿nie od wykonanych wymagañ
            for(int i = 0; i < completionStars; i++)
            {
                stars[i].color = new UnityEngine.Color(255, 255, 0);
            }

            //pokazanie UI
            levelCompletedUI.SetActive(true);
            ongoingGameUI.SetActive(false);
        }
        else //jak level nie jest completed to umo¿liw aktywacje menu
        {
            if (Input.GetButtonDown("PauseMenu")) // zdarzenie po wciœniêciu ESC
            {
                pauseMenuUI.SetActive(!pauseMenuUI.activeSelf); //zmien aktywacje menu

                if (pauseMenuUI.activeSelf == false) //gdy nie jest w³¹czone menu to daj ongoing
                {
                    GameManager.Instance.UpdateGameState(GameState.GameOngoing); //aktualizacja stanu gry

                }
                else
                {
                    GameManager.Instance.UpdateGameState(GameState.GamePaused);
                }
            }
        }

        if(GameManager.Instance.State == GameState.PlayerLoses)
        {
            playerLosesUI.SetActive(true);
            ongoingGameUI.SetActive(false);
        }

        if(GameManager.Instance.State == GameState.GameOngoing) //tutaj timer co sekunde do liczenia czasu przejscia gry
        {
            GameManager.Instance.TimerTick(); // TICK CO SEKUNDE
            
            //tutaj konwersja sekund na minuty i sekundy
            int absoluteTime = Convert.ToInt32(Math.Floor(GameManager.Instance.levelCompletionTimeInSecs));
            int minutes = absoluteTime / 60;
            int seconds = absoluteTime % 60;
            string timerString = "";
            if(seconds >= 60) { timerString = minutes.ToString()+":"+seconds.ToString(); }
            else { timerString = seconds.ToString(); }

            //dopisanie do displayu czasu
            timerDisplay.text = timerString;
        }
    }

    public void NextLevel()
    {
        //WIP giga duzo roboty tu bedzie ;)
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //start aktualnego poziomu od nowa
    }

    public void Resume() // dla buttona RESUME w pauseUI
    {
        GameManager.Instance.UpdateGameState(GameState.GameOngoing);
    }

    public void LoadMenu() //dla buttona MAINMENU w pauseUI
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame() // dla EXIT w pauseUI
    {
        Application.Quit();
    }
}
