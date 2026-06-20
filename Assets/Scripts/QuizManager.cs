using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class QuizQuestion
{
    public string quiz;
    public bool answer;
}



[System.Serializable]
public class QuizData
{
    public List<QuizQuestion> questions;
}

public class QuizManager : MonoBehaviour
{
    public TMP_Text quizText;

    public Button trueButton;
    public Button falseButton;
    public Button nextButton;

    public GameObject quizPanel;
    public GameObject exploreButton;

    private QuizData quizData;
    private int currentIndex = 0;

    public void ClickTrue()
{
    CheckAnswer(true);
}

public void ClickFalse()
{
    CheckAnswer(false);
}

public void NextQuestion()
{
    currentIndex++;
    ShowQuestion();
}

public void BackToExplore()
{
    quizPanel.SetActive(false);
    exploreButton.SetActive(true);
}

    void Start()
    {
        LoadQuizFromJson();

        nextButton.gameObject.SetActive(false);

        trueButton.onClick.AddListener(() => CheckAnswer(true));
        falseButton.onClick.AddListener(() => CheckAnswer(false));
        nextButton.onClick.AddListener(NextQuestion);

        ShowQuestion();
    }

    void LoadQuizFromJson()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "quiz.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            quizData = JsonUtility.FromJson<QuizData>(json);
        }
        else
        {
            Debug.LogError("quiz.json not found!");
        }
    }

    void ShowQuestion()
    {
        if (currentIndex < quizData.questions.Count)
        {
            quizText.text = quizData.questions[currentIndex].quiz;

            trueButton.gameObject.SetActive(true);
            falseButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            quizText.text = "Congrats! You Finished!";

            trueButton.gameObject.SetActive(false);
            falseButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
    }

    void CheckAnswer(bool userAnswer)
    {
        bool correctAnswer = quizData.questions[currentIndex].answer;

        if (userAnswer == correctAnswer)
        {
            quizText.text = "You are Correct! " + (currentIndex + 1) + "/" + quizData.questions.Count 
            +"\n " + quizData.questions[currentIndex].quiz + " is " + correctAnswer;
        }
        else
        {
            quizText.text = "Wrong Answer! " + (currentIndex + 1) + "/" + quizData.questions.Count
            +"\n " + quizData.questions[currentIndex].quiz + " is " + correctAnswer;
        }

        trueButton.gameObject.SetActive(false);
        falseButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }

    // void NextQuestion()
    // {
    //     currentIndex++;
    //     ShowQuestion();
    // }
}