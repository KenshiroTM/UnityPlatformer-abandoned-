using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuOptionsController : MonoBehaviour
{
    [Header("Main Menu Script")]
    public MenuMainController mainController;

    [Header("Options Canvas")]
    public GameObject menuOptions;

    [Header("Audio Player")]
    public MenuAudioPlayer menuAudioPlayer;

    [Header("Audio menu buttons")]
    public TMP_Text musicVolume;
    public Slider musicSlider;
    public TMP_Text effectsVolume;
    public Slider effectsSlider;

    [Header("Resolution & framerate & fullscreen")]
    public TMP_Dropdown resolutionSelect;
    public TMP_Dropdown framerateSelect;
    public Toggle vsyncToggle;
    public Toggle toggleFullscreen;

    private SettingsSaver settingsSaver = new SettingsSaver(); // klasa zapisuj¹ca wszystkie dane ustawieñ i menu

    private void OnApplicationQuit() //po wyjsciu z aplikacji zapisz do jsona opcje
    {
        settingsSaver.SaveToJson();
    }

    public void Awake() // gdy element siê za³aduje, Awake() callowane jest zawsze przed Start()
    {
        if (settingsSaver.LoadFromFile() != null) //gdy znajdzie jsona z opcjami
        {
            GameSettings gameSettings = settingsSaver.LoadFromFile();
            //nowy obiekt klasy GameSettings

            ChangeAllValues(); //³adowanie z pliku funkcj¹
            ChangeAllCanvas(gameSettings); //zmiana canvasów w grze
        }
        else if (settingsSaver.LoadFromFile() == null) //gdy nie znajdzie jsona z opcjami
        {
            MakeFirstSettings(); // tworzenie ustawien przy pierwszym odpaleniu gry
            ChangeAllValues();
        }
    }

    public void ChangeToggles(string toggleName)
    {
        switch (toggleName)
        {
            case "Fullscreen":
                if (toggleFullscreen.isOn == true) //zmiana fullscreena
                {
                    Screen.fullScreen = true;
                }
                else
                {
                    Screen.fullScreen = false;
                }
                settingsSaver.isFullscreen = toggleFullscreen.isOn; // zapisanie czy jest fullscreen
                break;
            case "Vsync":
                if (vsyncToggle.isOn == true)
                {// vsyncCount = 1 w³¹cza vsync i mamy fps dostosowane do monitora
                    QualitySettings.vSyncCount = 1;
                    framerateSelect.interactable = false; //wtedy wy³aczamy selecta z frameratem bo nie potrzeba jak vsync robi swoje :)
                }
                else
                {// vsyncCount = 0 to wy³aczenie vsync
                    QualitySettings.vSyncCount = 0;
                    framerateSelect.interactable = true;
                }
                settingsSaver.Vsync = QualitySettings.vSyncCount; //zapisanie vsync
                break;
        }
        settingsSaver.SaveToJson();
    }

    public void ChangeDropdowns(string name)
    {
        switch(name)
        {
            case "Resolution":
                string[] res = resolutionSelect.options[resolutionSelect.value].text.Split('x'); //tak sie pobiera z dropdownu
                int width = int.Parse(res[0]);
                int height = int.Parse(res[1]);
                Screen.SetResolution(width, height, Screen.fullScreen); // ustawianie resolution z opcj¹ fullscreen
                //zapisywanie resolution
                settingsSaver.screenWidth = width;
                settingsSaver.screenHeight = height;
                //zapisanie opcji w kanwasie
                settingsSaver.selectedResolution = resolutionSelect.value;
                break;
            case "Framerate":
                Application.targetFrameRate = int.Parse(framerateSelect.options[framerateSelect.value].text); //pobieranie dropdownu i zmiana framerate
                //zapisanie framerate
                settingsSaver.framerate = Application.targetFrameRate;
                //zapisanie numeru framerate z dropdownu
                settingsSaver.selectedFramerate = framerateSelect.value;
                break;
        }
        settingsSaver.SaveToJson();
    }

    public void ChangeVolume(string volType)
    {
        float volume = 0;
        switch (volType)
        {
            case "Music":
                volume = musicSlider.value * 10 - 80;
                musicVolume.text = (10 * musicSlider.value).ToString() + "%";
                settingsSaver.musicVolume = musicSlider.value; //zapisanie volume muzyki
                break;
            case "Effects":
                volume = effectsSlider.value * 10 - 80;
                effectsVolume.text = (10 * effectsSlider.value).ToString() + "%";
                settingsSaver.effectsVolume = effectsSlider.value; //zapisanie volume efektów (value)
                break;
        }
        menuAudioPlayer.audioMixer.SetFloat(volType, volume); // tutaj sie zapisuje volume w danym kanale audio, patrz audioMixer w projekcie
        settingsSaver.SaveToJson();
    }

    private void ChangeAllValues()
    {   //zmiana wszystkich wartoœci na ³adowaniu z pliku
        float musicVol = settingsSaver.musicVolume * 10 - 80;
        float effectsVol = settingsSaver.effectsVolume * 10 - 80;
        Screen.SetResolution(settingsSaver.screenWidth, settingsSaver.screenHeight, settingsSaver.isFullscreen);
        Application.targetFrameRate = settingsSaver.framerate;
        QualitySettings.vSyncCount = settingsSaver.Vsync;
        menuAudioPlayer.audioMixer.SetFloat("Music", musicVol);
        menuAudioPlayer.audioMixer.SetFloat("Effects", effectsVol);
    }

    private void MakeFirstSettings()
    {
        //ustawianie reszty rozdzielczoœci wzgledem uzywanego komputera
        settingsSaver.isFullscreen = true;
        settingsSaver.screenWidth = Screen.width;
        settingsSaver.screenHeight = Screen.height;
        settingsSaver.framerate = int.Parse(framerateSelect.options[0].text);
        settingsSaver.Vsync = 0;
        settingsSaver.effectsVolume = effectsSlider.value;
        settingsSaver.musicVolume = musicSlider.value;

        
        settingsSaver.SaveToJson();
        GameSettings gameSettings = settingsSaver.LoadFromFile();
        ChangeAllCanvas(gameSettings);

        ChangeAvaliableDropdowns();
    }

    private void ChangeAllCanvas(GameSettings gameSettings)
    {   // zmiana wszystkich canvasów (tak, trzeba wszystkie dropdowny i slidery kodowo ustawiaæ z zapisu pliku)

        ChangeAvaliableDropdowns(); //najpierw zmiana dostepnych toggli

        // zmiana toggli
        toggleFullscreen.isOn = gameSettings.isFullscreen;
        if (gameSettings.Vsync == 1) { vsyncToggle.isOn = true; framerateSelect.interactable = false; }
        
        //tu sie volume na sliderze ustawia
        musicSlider.value = gameSettings.musicVolume;
        effectsSlider.value = gameSettings.effectsVolume;
        
        //tutaj sie wartoœæ wybranego dropdownu ustawia
        framerateSelect.value = gameSettings.selectedFramerate;
        resolutionSelect.value = gameSettings.selectedResolution;
    }

    private void ChangeAvaliableDropdowns()
    {
        //znajdywanie rozdzielczoœci w dropdownie któr¹ ma monitor i usuwanie z dropdownów zbyt du¿ych wartoœci
        for (int i = resolutionSelect.options.Count-1; i >= 0 ; i--)
        {
            string[] res = resolutionSelect.options[i].text.Split('x');
            if (Display.main.systemWidth < int.Parse(res[0])) //jak res monitora jest mniejszy niz res w dropdownie to wywal to
            {
                resolutionSelect.options.RemoveAt(i); //usun jak jest zbyt duza rozdzielczosc na twoj monitor w dropdownie
            }
            else if (Display.main.systemWidth >= int.Parse(res[0]))
            {
                resolutionSelect.value = i; //zmieñ value dropdownu na taki który aktualnie jest
                settingsSaver.selectedResolution = i; //zapisz najwyzszy mozliwy resolution dla monitora
            }
        }
        // to samo co przy resolution
        for (int i = framerateSelect.options.Count-1; i>=0; i--)
        {
            int framerate = int.Parse(framerateSelect.options[i].text);
            if(Screen.currentResolution.refreshRate < framerate) //wywal zbyt duze frameraty dla monitora
            {
                framerateSelect.options.RemoveAt(i);
            }
            else if(Screen.currentResolution.refreshRate >= framerate) //najwyzszy mozliwy framerate dla monitora
            {
                framerateSelect.value = i;
                settingsSaver.selectedFramerate = i;
            }
        }
    }
}