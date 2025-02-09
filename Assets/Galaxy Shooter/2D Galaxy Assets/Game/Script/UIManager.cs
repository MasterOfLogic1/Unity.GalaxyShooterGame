using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //array variable
    public Sprite[] lives;
    public Text ScoreText;

    public int Score;

    public Image LivesImageDisplay;

    public GameObject TitleScreen;

    private TMP_Dropdown dropdown;

    void Start()
    {
        dropdown = transform.Find("DropdownCanvas").Find("Dropdown").GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        dropdown.enabled = true;
        List<string> gameLevel = new List<string>() { "easy", "medium", "hard" };
        // Create a list of TMP_Dropdown.OptionData
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        // Populate options list
        foreach (var level in gameLevel)
        {
            options.Add(new TMP_Dropdown.OptionData(level));
        }
        // Add options to the dropdown
        dropdown.AddOptions(options);
        // Refresh the shown value (called only once)
        dropdown.RefreshShownValue();

    }




    //communicate lives
    public void Updatelives(int currentLives)
    {
        LivesImageDisplay.sprite = lives[currentLives];
    }
    // score

    public void UpdateScore()
    {
        Score += 10;

        ScoreText.text = "Score: " + Score;
    }
 
    public void ShowTitleScreen()
    {
        TitleScreen.SetActive(true);
        dropdown.gameObject.SetActive(true);
    }

    public void HideTitleScreen()
    {
        TitleScreen.SetActive(false);
        dropdown.gameObject.SetActive(false);
        ScoreText.text = "Score: "; 
    }



}
