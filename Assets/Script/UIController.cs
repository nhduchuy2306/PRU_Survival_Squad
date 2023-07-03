using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    public Slider explvlSlider;
    public TMP_Text expLevelText;

    public LevelUpSelectionButton[] levelUpButtons;

    public GameObject levelUpPanel;

    public TMP_Text coinText;

    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay ;

    public TMP_Text timeText;

    public GameObject levelEndScreen;
    public TMP_Text endTimeText;
    public GameObject pauseMenu;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Toggle themeToggle;

    // Start is called before the first frame update
    void Start()
    {
        float volumeValue = PlayerPrefs.GetFloat("Volume", 0.5f);
        int sfxToggleValue = PlayerPrefs.GetInt("SFX", 1);
        int themeToggleValue = PlayerPrefs.GetInt("Theme", 1);
        FindObjectOfType<AudioManager>().Stop("ThemeSound");

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
            FindObjectOfType<AudioManager>().Play("InGameSound");
        }
        else
        {
            themeToggle.isOn = false;
            FindObjectOfType<AudioManager>().Stop("InGameSound");
        }

        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }

    // Update is called once per frame
    void Update()
    {
        ShowPauseScreen();
    }

    public void UpdateExperience(int currentExp, int levelExp, int currentLvl)
    {
        explvlSlider.maxValue = levelExp;
        explvlSlider.value = currentExp;

        expLevelText.text = "Level: " + currentLvl;

    }

    public void UpdateCoins()
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();
        SkipLevelUp();
    }

    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickupRange()
    {
        PlayerStatController.instance.PurchasePickupRange();
        SkipLevelUp();
    }
    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);

        timeText.text = "Time: " + minutes + ":" + seconds.ToString("00");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void ShowPauseScreen()
    {
        // Press escape to pause
        if(Input.GetKeyDown(KeyCode.Escape) && !levelEndScreen.activeInHierarchy)
        {
            Debug.Log("Pause");
            if(pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                Debug.Log("Pause no active");
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void ResumeInPauseScreen()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitInPauseScreen()
    {
        SceneManager.LoadScene("MainMenu");
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
