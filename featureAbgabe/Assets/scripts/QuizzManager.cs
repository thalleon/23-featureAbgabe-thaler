using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("Text")]
    public QuizQuestion quizText;
    public QuizQuestion quizText2;
    public TMP_Text questionText;

    [Header("Buttons")]
    public Button[] buttons = new Button[4];

    [Header("Canvas Wechsel")]
    public GameObject quizCanvas;
    public GameObject rewardCanvas;
    public GameObject rewardCanvas_2;

    [Header("Video1")]
    public GameObject[] videoCanvases;
    public VideoPlayer[] videoPlayers;
    public Slider part2Slider;

    [Header("Video2")]
    public GameObject videoCanvas_2;
    public UnityEngine.Video.VideoPlayer videoPlayer2;

    [Header("Color")]
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;

    private int currentVideoIndex = 0;
    private QuizQuestion currentQuizText;


    void Start()
    {
        foreach (var canvas in videoCanvases)
            canvas.SetActive(false);

        quizCanvas?.SetActive(false);
        rewardCanvas?.SetActive(false);
        part2Slider?.gameObject.SetActive(false);

        PlayNextPart();

        if (videoPlayer2 != null)
        {
            videoPlayer2.loopPointReached += OnSecondVideoFinished;
        }
    }

    void PlayNextPart()
    {
        if (currentVideoIndex >= videoCanvases.Length)
        {
            quizCanvas?.SetActive(true);
            ApplyQuestionData(quizText);
            return;
        }

        for (int i = 0; i < videoCanvases.Length; i++)
            videoCanvases[i].SetActive(i == currentVideoIndex);

        if (currentVideoIndex == 1)
        {
            VideoPlayer vp = videoPlayers[1];
            if (vp != null)
            {
                part2Slider.gameObject.SetActive(true);
                part2Slider.minValue = 0f;
                part2Slider.maxValue = 1f;
                part2Slider.value = 0f;

                vp.Stop();
                vp.playOnAwake = false;
                vp.Pause();

                vp.frame = 0;
                vp.StepForward();

                part2Slider.onValueChanged.RemoveAllListeners();
                part2Slider.onValueChanged.AddListener((value) =>
                {
                    if (vp.frameCount > 0)
                    {
                        long targetFrame = (long)(value * vp.frameCount);
                        vp.frame = targetFrame;
                        vp.StepForward();
                    }

                    if (value >= 0.99f)
                    {
                        part2Slider.onValueChanged.RemoveAllListeners();
                        part2Slider.gameObject.SetActive(false);
                        videoCanvases[currentVideoIndex].SetActive(false);
                        currentVideoIndex++;
                        PlayNextPart();
                    }
                });
            }
        }

        else
        {
            part2Slider?.gameObject.SetActive(false);

            int vpIndex = currentVideoIndex == 0 ? 0 : 2;
            VideoPlayer vp = videoPlayers[vpIndex];

            if (vp != null)
            {
                vp.loopPointReached += OnVideoEnded;
                vp.Play();
            }
        }
    }


    void OnVideoEnded(VideoPlayer vp)
    {
        vp.loopPointReached -= OnVideoEnded;
        videoCanvases[currentVideoIndex].SetActive(false);
        currentVideoIndex++;
        PlayNextPart();
    }

    public void ApplyQuestionData(QuizQuestion data)
    {
        currentQuizText = data;

        if (questionText != null)
        {
            questionText.text = data.questionText;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == null || i >= data.answers.Length) continue;

            TMP_Text btnText = buttons[i].GetComponentInChildren<TMP_Text>();
            if (btnText != null)
            {
                btnText.text = data.answers[i];
            }

            int index = i;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }

        ApplyButtonColors(data.correctAnswerIndex);
    }

    void ApplyButtonColors(int correctIndex)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == null) continue;

            var colors = buttons[i].colors;

            if (i == correctIndex)
            {
                colors.pressedColor = correctColor;
                colors.selectedColor = correctColor;
            }
            else
            {
                colors.pressedColor = incorrectColor;
                colors.selectedColor = incorrectColor;
            }

            buttons[i].colors = colors;
        }
    }

    void OnAnswerSelected(int selectedIndex)
    {
        if (selectedIndex == currentQuizText.correctAnswerIndex)
        {
            if (quizCanvas != null) quizCanvas.SetActive(false);

            if (currentQuizText == quizText)
            {
                if (rewardCanvas != null) rewardCanvas.SetActive(true);
            }
            else if (currentQuizText == quizText2)
            {
                if (rewardCanvas_2 != null) rewardCanvas_2.SetActive(true);
            }

            SceneManager.LoadScene("ItemScene_01");
        }
        else
        {
            Debug.Log("Falsche Antwort!");
        }
    }

    public void ShowVideoCanvas2()
    {
        Debug.Log("ShowVideoCanvas2() aufgerufen");

        if (rewardCanvas != null)
        {
            Debug.Log("Deaktiviere rewardCanvas");
            rewardCanvas.SetActive(false);
        }

        if (videoCanvas_2 != null)
        {
            videoCanvas_2.SetActive(true);
        }

        if (videoPlayer2 != null)
        {
            videoPlayer2.Play();
        }
    }

    public void ReturnToQuizWithNewQuestion()
    {
        if (videoCanvas_2 != null) videoCanvas_2.SetActive(false);
        if (quizCanvas != null) quizCanvas.SetActive(true);

        if (quizText2 != null)
        {
            ApplyQuestionData(quizText2);
        }
    }

    void OnSecondVideoFinished(UnityEngine.Video.VideoPlayer vp)
    {
        ReturnToQuizWithNewQuestion();
    }
}
