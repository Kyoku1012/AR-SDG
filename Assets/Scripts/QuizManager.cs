using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Collections;
using UnityEngine.Networking;


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

    public TreeSpawnManager treeSpawnManager;
    private int correctCount = 0;

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

    // void Start()
    // {
    //     LoadQuizFromJson();

    //     nextButton.gameObject.SetActive(false);

    //     // trueButton.onClick.AddListener(() => CheckAnswer(true));
    //     // falseButton.onClick.AddListener(() => CheckAnswer(false));
    //     // nextButton.onClick.AddListener(NextQuestion);

    //     ShowQuestion();
    // }


    void Start()
    {
    nextButton.gameObject.SetActive(false);
    StartCoroutine(LoadQuizFromJson());
    }

    // void LoadQuizFromJson()
    // {
    //     string path = Path.Combine(Application.streamingAssetsPath, "quiz.json");

    //     if (File.Exists(path))
    //     {
    //         string json = File.ReadAllText(path);
    //         quizData = JsonUtility.FromJson<QuizData>(json);
    //     }
    //     else
    //     {
    //         Debug.LogError("quiz.json not found!");
    //     }
    // }
    
    IEnumerator LoadQuizFromJson()
{
    string path = Path.Combine(Application.streamingAssetsPath, "quiz.json");

    UnityWebRequest request = UnityWebRequest.Get(path);
    yield return request.SendWebRequest();

    if (request.result != UnityWebRequest.Result.Success)
    {
        Debug.LogError("Failed to load quiz.json: " + request.error);
        yield break;
    }

    quizData = JsonUtility.FromJson<QuizData>(request.downloadHandler.text);

    if (quizData == null || quizData.questions == null)
    {
        Debug.LogError("Quiz data is empty or JSON format is wrong.");
        yield break;
    }

    ShowQuestion();
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
            correctCount++;

            quizText.text = "<color=#4CAF50>You are Correct! </color> " + (currentIndex + 1) + "/" + quizData.questions.Count 
            +"\n \n " + quizData.questions[currentIndex].quiz + ": " + correctAnswer;

        
                    treeSpawnManager.PlantOneTree();
                
            }
        else
        {
            quizText.text = "<color=#F44336>Wrong Answer! </color>" + (currentIndex + 1) + "/" + quizData.questions.Count
            +"\n \n " + quizData.questions[currentIndex].quiz + ": " + correctAnswer;
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