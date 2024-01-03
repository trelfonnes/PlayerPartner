using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSelect : MonoBehaviour
{
    public TextMeshProUGUI femaleSelectText;
    public TextMeshProUGUI maleSelectText;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

    string question;
    int totalPoints;
    private void Start()
    {
        question = "Who do you want to play as?";
        StartQuestion();
    }
    public void OnAnswerSelected(int answerIndex)
    {
        if(answerIndex <= 1)
        {
            totalPoints = 1;
        }
        else
        {
            totalPoints = 2;
        }
        DeterminePlayerType();
    }
    void StartQuestion()
    {
        DisplayQuestion();
    }
    void DisplayQuestion()
    {
        questionText.text = question;
        femaleSelectText.text = "Character One";
        maleSelectText.text = "Character Two";
    }
    void DeterminePlayerType()
    {
        PlayerType result;
        if(totalPoints == 1)
        {
            result = PlayerType.Female;
        }
        else
        {
            result = PlayerType.Male;
        }
        Debug.Log(result);
        ES3.Save("chosenPlayer", result);
        SceneLoaderUtility sceneManager = new SceneLoaderUtility();
        sceneManager.LoadScene("PartnerSelectScene");
    }

}
