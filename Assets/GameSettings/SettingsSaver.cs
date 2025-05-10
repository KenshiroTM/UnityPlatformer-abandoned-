using System.IO;
using UnityEngine;

public class SettingsSaver
{   //w³asnoœci te same co w GameSettings
    public bool isFullscreen;
    public int Vsync;

    public int screenWidth;
    public int screenHeight;
    public int framerate;

    public int selectedFramerate;
    public int selectedResolution;

    public float musicVolume;
    public float effectsVolume;

    public void SaveToJson()
    {
        //zapisywanie do obiektu i potem do pliku w gamesettings
        GameSettings gameSettings = new GameSettings(); // tworzy nowy obiekt gameSettings

        //dodaje wartoœci z SettingsSaver do GameSettings
        gameSettings.isFullscreen = isFullscreen;
        gameSettings.Vsync = Vsync;

        //do zmiany na selected opcje
        gameSettings.screenWidth = screenWidth;
        gameSettings.screenHeight = screenHeight;
        gameSettings.framerate = framerate;

        //czytaæ value w sliderze!
        gameSettings.musicVolume = musicVolume;
        gameSettings.effectsVolume = effectsVolume;

        gameSettings.selectedResolution = selectedResolution;
        gameSettings.selectedFramerate = selectedFramerate;

        //zapisywanie pliku w json ze zmiennej GameSettings
        string json = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.dataPath + "/GameSettings.json", json);
    }

    public GameSettings LoadFromFile()
    {
        string path = Application.dataPath + "/GameSettings.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path); // pobieranie œcie¿ki pliku

            GameSettings gameSettings = JsonUtility.FromJson<GameSettings>(json); // tworzenie obiektu z wyczytanego gameSettings

            isFullscreen = gameSettings.isFullscreen;
            Vsync = gameSettings.Vsync;

            screenHeight = gameSettings.screenHeight;
            screenWidth = gameSettings.screenWidth;
            framerate = gameSettings.framerate;

            selectedFramerate = gameSettings.selectedFramerate;
            selectedResolution = gameSettings.selectedResolution;

            musicVolume = gameSettings.musicVolume;
            effectsVolume = gameSettings.effectsVolume;

            return gameSettings;
        }
        return null;
    }
}
