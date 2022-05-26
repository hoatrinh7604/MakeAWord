using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] int score;
    [SerializeField] int highscore;
    public Color[] template = { new Color32(255, 81, 81, 255), new Color32(255, 129, 82, 255), new Color32(255, 233, 82, 255), new Color32(163, 255, 82, 255), new Color32(82, 207, 255, 255), new Color32(170, 82, 255, 255) };

    [SerializeField] int currentTarget = 0;
    [SerializeField] int currentFirst = 0;
    [SerializeField] int currentLast = 0;
    [SerializeField] int theNumberOfNumber = 0;

    private UIController uiController;

    private float time;
    [SerializeField] float timeOfGame;

    [SerializeField] TextAsset data;
    private List<string> listWord;
    private char currentChar;
    private string currentString;

    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIController>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        UpdateSlider();

        if(time < 0)
        {
            GameOver();
        }
    }

    public void ParseData()
    {
        listWord.Clear();
        var textData = data.ToString();
        var arr = textData.Split(',');
        for(int i = 0; i<arr.Length; i++)
        {
            listWord.Add(arr[i]);
        }
    }

    public void UpdateSlider()
    {
        uiController.UpdateSlider(time);
    }

    public void SetSlider()
    {
        uiController.SetSlider(timeOfGame);
    }

    public void OnPressHandle(char value)
    {
        if(value == currentChar)
        {
            UpdateScore();
            NextTurn();
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uiController.GameOver();
    }

    public void UpdateScore()
    {
        score++;
        if(highscore < score)
        {
            highscore = score;
            PlayerPrefs.SetInt("score", highscore);
            uiController.UpdateHighScore(highscore);
        }
        uiController.UpdateScore(score);
    }

    IEnumerator StartAfterTime()
    {
        yield return null;
        NextTurn();
    }
    public void NextTurn()
    {
        // Get random string
        int index = Random.Range(0, listWord.Count);
        currentString = listWord[index];
        while(currentString.Length < 2)
        {
            index = Random.Range(0, listWord.Count);
            currentString = listWord[index];
        }

        // Set missing char
        int indexChar = Random.Range(0, currentString.Length);
        currentChar = currentString[indexChar];

        uiController.UpdateWord(currentString);

        char[] array = currentString.ToCharArray();
        array[indexChar] = '_';
        currentString = new string(array);

        uiController.UpdateNumber(currentString);

        time = timeOfGame;
    }

    public string IntToText(int value)
    {
        switch(value)
        {
            case 0: return "Zero"; break;
            case 1: return "One"; break;
            case 2: return "Two"; break;
            case 3: return "Three"; break;
            case 4: return "Four"; break;
            case 5: return "Five"; break;
            case 6: return "Six"; break;
            case 7: return "Seven"; break;
            case 8: return "Eight"; break;
            case 9: return "Nine"; break;
            case 10: return "Ten"; break;
            case 11: return "Eleven"; break;
            case 12: return "Twelve"; break;
            case 13: return "Thirteen"; break;
            case 14: return "Fourteen"; break;
            case 15: return "Fifteen"; break;
            case 16: return "Sixteen"; break;
            case 17: return "Seventeen"; break;
            case 18: return "Eighteen"; break;
            case 19: return "Nineteen"; break;
            case 20: return "Twenty"; break;
            default: return "Zero"; break;
        }
    }

    public void Reset()
    {
        listWord = new List<string>();
        ParseData();
        Time.timeScale = 1;

        time = timeOfGame;
        SetSlider();
        score = 1;
        uiController.UpdateScore(score);
        uiController.UpdateHighScore(PlayerPrefs.GetInt("score"));

        NextTurn();
    }

}
