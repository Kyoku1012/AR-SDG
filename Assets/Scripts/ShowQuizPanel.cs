using UnityEngine;

public class ShowQuizPanel : MonoBehaviour
{
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private GameObject exploreButton;

    public void OpenQuiz()
    {
        //Debug.LogError("ShowQuizPanel: Clicked.", this);
            
        if (quizPanel == null)
        {
            Debug.LogError("ShowQuizPanel: Quiz Panel is not assigned.", this);
            return;
        }


        quizPanel.SetActive(true);

        if (exploreButton != null)
        {
            exploreButton.SetActive(false);
        }

    }
}
