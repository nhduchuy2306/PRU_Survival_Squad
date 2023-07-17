using TMPro;
using Proyecto26;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Toggle themeToggle;
    public string inputName = "USER";
    //input field object
    public TMP_InputField tmpInputField;
    public string userID;

    public void Start()
    {
        float volumeValue = PlayerPrefs.GetFloat("Volume", 0.5f);
        int sfxToggleValue = PlayerPrefs.GetInt("SFX", 1);
        int themeToggleValue = PlayerPrefs.GetInt("Theme", 1);

        Debug.Log("Volume: " + volumeValue);
        Debug.Log("SFX: " + sfxToggleValue);
        Debug.Log("Theme: " + themeToggleValue);

        if (sfxToggleValue == 1)
        {
            sfxToggle.isOn = true;
            FindObjectOfType<AudioManager>().ActiveSoundEffects();
        }
        else
        {
            sfxToggle.isOn = false;
            FindObjectOfType<AudioManager>().DeActiveAllSoundEffects();
        }

        if (themeToggleValue == 1)
        {
            themeToggle.isOn = true;
            FindObjectOfType<AudioManager>().Play("ThemeSound");
        }
        else
        {
            themeToggle.isOn = false;
            FindObjectOfType<AudioManager>().Stop("ThemeSound");
        }

        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;

        userID = SystemInfo.deviceUniqueIdentifier;
        GetUser();
        tmpInputField.onEndEdit.AddListener(TextMeshUpdated);
    }

    private void GetUser()
    {
        User user = new();
        string url = "https://unity-pru-d6912-default-rtdb.asia-southeast1.firebasedatabase.app/users/" + userID + ".json";
        RestClient.Get<User>(url).Then(res =>
        {
            if (res != null)
            {
                user = res;
                inputName = user.userName.Trim();
                tmpInputField.text = inputName;
            }
        }).Catch(err =>
        {
            Debug.Log("New user");
        });
    }

    public void TextMeshUpdated(string text)
    {
        inputName = text;
    }

    public void StartGame()
    {
        Debug.Log("start: " + inputName);
        SceneManager.LoadSceneAsync("Main");
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit");
    }

    public void SetOnOffSfxToggle()
    {
        if (sfxToggle.isOn)
        {
            PlayerPrefs.SetInt("SFX", 1);
            sfxToggle.isOn = true;
            FindObjectOfType<AudioManager>().ActiveSoundEffects();
        }
        else
        {
            PlayerPrefs.SetInt("SFX", 0);
            sfxToggle.isOn = false;
            FindObjectOfType<AudioManager>().DeActiveAllSoundEffects();
        }
    }

    public void SetOnOffThemeToggle()
    {
        if (themeToggle.isOn)
        {
            PlayerPrefs.SetInt("Theme", 1);
            themeToggle.isOn = true;
            FindObjectOfType<AudioManager>().Play("ThemeSound");
        }
        else
        {
            PlayerPrefs.SetInt("Theme", 0);
            themeToggle.isOn = false;
            FindObjectOfType<AudioManager>().Stop("ThemeSound");
        }
    }


    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SaveSettings()
    {
        float volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volume);

        if (sfxToggle.isOn)
        {
            PlayerPrefs.SetInt("SFX", 1);
        }
        else
        {
            PlayerPrefs.SetInt("SFX", 0);
        }

        if (themeToggle.isOn)
        {
            PlayerPrefs.SetInt("Theme", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Theme", 0);
        }
    }
}
