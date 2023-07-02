using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{

    public static ExperienceLevelController instance;

    private PlayerController player;

    private void Awake()
    {
        instance = this;
    }

    public int currentExperience;

    public ExpPickUp pickup;

    public List<int> expLevels;
    public int currentLevel = 1, levelCount = 100;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance.GetComponent<PlayerController>(); ;

        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;
        if (currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }

        UIController.instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
    }

    public void LevelUp()
    {
        currentExperience -= expLevels[currentLevel];

        currentLevel++;

        if (currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }

        FindObjectOfType<AudioManager>().Play("LevelUpSound");

        // PlayerController.instance.activeWeapon.LevelUp();
        UIController.instance.levelUpPanel.SetActive(true);

        Time.timeScale = 0f;
        //UIController.instance.levelUpButtons[0].UpdateButtonDisplay(PlayerController.instance.activeWeapon);
        
        UIController.instance.levelUpButtons[0].UpdateButtonDisplay(player.assignedWeapons[0]);

        for(int i = 0; i < player.assignedWeapons.Count; i++)
        {
            UIController.instance.levelUpButtons[i].UpdateButtonDisplay(player.assignedWeapons[i]);
        }
        for (int i = player.unassignedWeapons.Count - 1; i >= 0 ; i--)
        {
            UIController.instance.levelUpButtons[2-i].UpdateButtonDisplay(player.unassignedWeapons[i]);
        }

        PlayerStatController.instance.UpdateDisplay();

        /*UIController.instance.levelUpButtons[1].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[0]);
        UIController.instance.levelUpButtons[2].UpdateButtonDisplay(PlayerController.instance.unassignedWeapons[1]);*/

    }
}
