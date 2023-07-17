using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private void Awake()
    {
        instance = this;
    }

    private bool gameActive;
    public float timer;

    public float waitToShowEndScreen = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }
    }

    public void EndLevel()
    {
        gameActive = false;

        StartCoroutine(EndLevelCo());
    }

    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToShowEndScreen);

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);

        UIController.instance.endTimeText.text = minutes.ToString() + " mins " + seconds.ToString("00" + " secs");

        UserScore();
        UIController.instance.levelEndScreen.SetActive(true);


        FindObjectOfType<AudioManager>().Play("LoseSound");

        PlayerController.instance.gameObject.SetActive(false);
    }


    private void UserScore()
    {
        float score = timer;
        User user = null;
        string url = "https://unity-pru-d6912-default-rtdb.asia-southeast1.firebasedatabase.app/users/" + MainMenu.instance.userID + ".json";
        RestClient.Get<User>(url).Then(res =>
        {
            if (res != null)
            {
                user = res;
                if (user.userScore < score)
                {
                    float minutes = Mathf.FloorToInt(score / 60f);
                    float seconds = Mathf.FloorToInt(score % 60);

                    PutToDB(new User(MainMenu.instance.inputName, score));

                    UIController.instance.bestText.text = "Best: " + minutes.ToString() + " mins " + seconds.ToString("00" + " secs");
                }
                else
                {
                    float minutes = Mathf.FloorToInt(user.userScore / 60f);
                    float seconds = Mathf.FloorToInt(user.userScore % 60);

                    PutToDB(new User(MainMenu.instance.inputName, user.userScore));
                    UIController.instance.bestText.text = "Best: " + minutes.ToString() + " mins " + seconds.ToString("00" + " secs");
                }
            }
        }).Catch(err =>
        {
            float minutes = Mathf.FloorToInt(score / 60f);
            float seconds = Mathf.FloorToInt(score % 60);

            PutToDB(new User(MainMenu.instance.inputName, score));

            UIController.instance.bestText.text = "Best: " + minutes.ToString() + " mins " + seconds.ToString("00" + " secs");
        });

    }

    public void PutToDB(User user)
    {

        string url = "https://unity-pru-d6912-default-rtdb.asia-southeast1.firebasedatabase.app/users/" + MainMenu.instance.userID + ".json";
        RestClient.Put(url, user);
    }
}
