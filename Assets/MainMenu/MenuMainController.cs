using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMainController : MonoBehaviour
{
    [Header("Menu Scripts")]
    public MenuOptionsController menuOptionsController;
    public MenuLevelsController menuLevelsController;

    [Header("Main Menu Canvas")]
    public GameObject menuMain;

    public void Start() //jak jest start() to wtedy dzia³a audio normalnie w edytorze
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
        menuOptionsController.Awake();
    }

    //swapowanie widocznoœci Interfejsu Options i MainMenu
    public void OptionsSwap()
    {
        menuOptionsController.menuOptions.SetActive(!menuOptionsController.menuOptions.activeSelf);
        menuMain.SetActive(!menuMain.activeSelf);
    }

    //swapowanie na level selection
    public void LevelsSwap()
    {
        menuLevelsController.menuLevels.SetActive(!menuLevelsController.menuLevels.activeSelf);
        menuMain.SetActive(!menuMain.activeSelf);
    }

    //wychodzenie z gry
    public void ExitGame()
    {
        Application.Quit();
    }
}
