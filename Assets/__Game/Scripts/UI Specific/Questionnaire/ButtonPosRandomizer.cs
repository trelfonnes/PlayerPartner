using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonPosRandomizer : MonoBehaviour
{
    public Button[] answerButtons;
    [SerializeField] int totalQuestions = 8;
    int timesClicked = 0;
    private Vector3[] initialPositions;

    void Start()
    {
        // Store the initial positions of the buttons
        StoreInitialPositions();
    }

    public void RandomizeButtonPositions()
    {
        timesClicked++;
        if (timesClicked < totalQuestions)
        {
            // Shuffle the initial positions array
            ShuffleArray(initialPositions);

            // Apply the shuffled positions to the buttons
            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].GetComponent<RectTransform>().anchoredPosition = initialPositions[i];
            }
        }
    }

    private void StoreInitialPositions()
    {
        initialPositions = new Vector3[answerButtons.Length];

        for (int i = 0; i < answerButtons.Length; i++)
        {
            initialPositions[i] = answerButtons[i].GetComponent<RectTransform>().anchoredPosition;
        }
    }

    private void ShuffleArray(Vector3[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            Vector3 temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
