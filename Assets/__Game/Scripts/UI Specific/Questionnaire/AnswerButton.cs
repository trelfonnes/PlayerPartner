using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public int answerIndex; // Set this in the inspector for each button
    public Questionnaire questionnaireManager;
    public ButtonPosRandomizer buttonRandomizer;

    public void OnButtonClick()
    {
        questionnaireManager.OnAnswerSelected(answerIndex);
        buttonRandomizer.RandomizeButtonPositions();
    }
}