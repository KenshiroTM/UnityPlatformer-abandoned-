using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevelsController : MonoBehaviour
{
    [Header("Menu Scripts")]
    public MenuMainController mainController;

    [Header("Levels Object")]
    public GameObject menuLevels;

    [Header("Grid with levels (panel)")]
    public Transform levelsGrid;

    [Header("Button prefab")]
    public LevelButtonDisplay btnPrefab;

    [Header("List of levels (scene names must match exactly)")]
    public List<string> levelSceneNames = new List<string> { "1" };

    public void Awake()
    {
        GetAllLevels();
    }

    public void StartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GetAllLevels()
    {
        foreach (string sceneName in levelSceneNames)
        {
            LevelButtonDisplay lvBtn = Instantiate(btnPrefab, levelsGrid);
            lvBtn.setBtnName(sceneName);
            lvBtn.menuLevelsController = this;
        }
    }
}
