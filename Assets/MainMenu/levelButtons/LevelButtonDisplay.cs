using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButtonDisplay : MonoBehaviour
{
    public Button levelButtonDisplay;
    public TMP_Text levelNumberText;

    public MenuLevelsController menuLevelsController;

    private void Awake()
    {
        if (levelButtonDisplay == null)
        {
            levelButtonDisplay = GetComponent<Button>();
        }

    }

    public void setBtnName(string name)
    {
        levelNumberText.text = name;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelNumberText.text);
    }
}
