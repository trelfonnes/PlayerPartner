using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Questionnaire : MonoBehaviour
{

    public TextMeshProUGUI[] answerTexts; 
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

   [SerializeField] private int totalPoints = 0;
    private int questionIndex = 0;

    private string[] questions = {
        "Where do you feel most at home?", 
        "You discover a mysterious artifact. What's your first reaction?",
        "In battle, what would you choose?",
        "How do you handle tense situations?",
        "What do you like to do for fun?", 
        "A locked door blocks your path. What do you do?", 
        "When faced with the unknown, what's your instinct?", 
        "What kind of companion appeals to you the most?" };

    private string[] answers = {
    "Dense Forests", //answer options for question 1
    "Hot Deserts",
    "Bustling Cities",
    "Gentle OceanSides",

    "Keep a safe distance", //answers for question 2
    "Snatch it quickly",
    "Analyze it carefully",
    "Consult others for advice",

     "Bow and arrows", //answer options for question 3
    "Sword or axe",
    "Magic or spells",
    "Shield or armor",

     "Blend into the crowd", //answer options for question 4
    "Defend yourself",
    "Stay focused and serious",
    "Remain calm and ask questions",

     "Relax in a quiet place", //answer options for question 5
    "Improve personal qualities",
    "Learn something new",
    "Spend time with friends",

     "Wait for help", //answer options for question 6
    "Bust it open",
    "Search for a key",
    "Go a different way",

     "Test the waters", //answer options for question 7
    "Charge forward",
    "Plan meticulously",
    "Collaborate with others",

     "Naive and dependent", //answer options for question 8
    "Fierce and powerful",
    "Wise and knowledgeable",
    "Loyal and supportive",

    // ... other answers
};

    private int[][] answerPoints = {
   new int[] { 1, 1, 1, 1, 1, 1, 1, 1 },  // Axolotl
   new int[] { 4, 4, 4, 4, 4, 4, 4, 4},  //dino
    new int[] { 3, 3, 3, 3, 3, 3, 3, 3 }, // bear
   new int[] { 2, 2, 2, 2, 2, 2, 2, 2 },  // Rabbit
    };

    public void OnAnswerSelected(int answerIndex)
    {
        int categoryPoints = answerPoints[answerIndex][questionIndex];
        totalPoints += categoryPoints;

        questionIndex++;

        if (questionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {

           DeterminePartnerType();
        }
    }
    private void Start()
    {
        StartQuestionnaire();
    }
    public void StartQuestionnaire()
    {
        DisplayQuestion();
    }
    private void DisplayQuestion()
    {
        questionText.text = questions[questionIndex];

        // Get the array of answer points for the current question
        int startIndex = questionIndex * 4;
        // Display the multiple-choice answers
        for (int i = 0; i < answerTexts.Length; i++)
        {
            // Set the text of each answer button based on the answer points
            answerTexts[i].text = $"{answers[startIndex + i]}";
        }
    }
    private void DeterminePartnerType()
    {
        PartnerType result;

        if (totalPoints < 14)
            result = PartnerType.AxelOne;
        else if (totalPoints >= 14 && totalPoints <= 19)
            result = PartnerType.BearOne;
        else if (totalPoints > 19 && totalPoints <= 25)
            result = PartnerType.RabbitOne;
        else
            result = PartnerType.DinoOne;


        ES3.Save("chosenPartner", result);
        SceneLoaderUtility sceneManager = new SceneLoaderUtility();
        sceneManager.LoadScene("SandBoxScene");
        //load to the next scene;
        // Pass the result to your game manager for initialization
        // GameManager.Instance.Initialize(result);
    }
}
