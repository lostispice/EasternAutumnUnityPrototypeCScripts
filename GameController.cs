using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // TODO - Used in messages, imported via PlayerPrefs. Currently not yet implemented.
    //[SerializeField] string playerName;

    // Value from 0 - 3 
    [SerializeField] int difficulty;

    // Gameplay Targets. Need to write a method that sets targets based on difficulty/session id.
    [SerializeField] int targetMin;
    [SerializeField] int targetComm;
    [SerializeField] int targetAward;
    [SerializeField] int score;

    // Used to update target sheet textboxes displayed to player at start of session
    public TextMeshProUGUI targetMinText;
    public TextMeshProUGUI targetCommText;
    public TextMeshProUGUI targetAwardText;

    // Used to generate country playset for the given difficulty level
    IDictionary<int, List<string>> nations = new Dictionary<int, List<string>>();

    // UI text written on mail item
    public TextMeshProUGUI mailAddress;

    // Used to generate a random challenge and then checked against the player's answer
    int challenge;
    int answer;

    // Life-counter, this could be modifable in the options menu?
    [SerializeField] int lifeCount;

    // Used to manage the Messages (life counter) panel
    public GameObject buttonUnread;
    public GameObject buttonRead;
    public TextMeshProUGUI lifeMessages;
    bool firstMessage = true;
    int messageCount;

    // Timer value, measured in seconds.
    // TODO - countdownTimer may be difficulty-dependant. Currently uses placeholder fixed value
    [SerializeField] float countdownTimer = 60;
    bool pauseTimer = true;

    // Used to end the game session
    public GameObject gameplayUI;
    public GameObject endUI;
    public TextMeshProUGUI endMessage;

    // Start is called before the first frame update
    void Start()
    {
        ResetValues();
        GeneratePlayset();
        SessionTargets();
    }

    // Update is called once per frame
    void Update()
    {
        GameTimer();
    }

    // Resets the gameplay counters to default values. Cross-reference with player modifiers?
    public void ResetValues()
    {
        targetMin = 5;
        targetComm = 10;
        targetAward = 20;
        lifeCount = 3;
        score = 0;
    }

    // Generates the level's country playset, using 1996 city names. Special non-latin characters simplified to latin characters.
    // Non-Soviet dissolution/reunification nations like East Germany, Czechoslovakia & Yugoslavia are omitted for gameplay simplicity. Could be introduced in later versions.
    public void GeneratePlayset()
    {
        // Import settings from main menu
        difficulty = PlayerPrefs.GetInt("difficulty");
        nations.Add(1, new List<string> { "Tirana", "Durres", "Vlore" });                   // Albania
        nations.Add(2, new List<string> { "Sofia", "Plovdiv", "Varna" });                   // Bulgaria
        nations.Add(3, new List<string> { "Budapest", "Debrecen", "Miskolc" });             // Hungary
        nations.Add(4, new List<string> { "Warsaw", "Krakow", "Lodz" });                    // Poland
        nations.Add(5, new List<string> { "Bucharest", "Cluj-Napoca", "Timisoara" });       // Romania
        nations.Add(6, new List<string> { "Moscow", "Saint Petersburg", "Novosibirsk" });   // Russia
        nations.Add(7, new List<string> { "Yerevan", "Gyumri", "Vanadzor" });               // Armenia
        nations.Add(8, new List<string> { "Baku", "Ganja", "Sumqayit" });                   // Azerbaijan
        nations.Add(9, new List<string> { "Minsk", "Gomel", "Grodno" });                    // Belarus
        nations.Add(10, new List<string> { "Tallinn", "Tartu", "Narva" });                  // Estonia
        nations.Add(11, new List<string> { "Tbilisi", "Batumi", "Kutaisi" });               // Georgia
        nations.Add(12, new List<string> { "Almaty", "Karaganda", "Akmola" });              // Kazakhstan
        nations.Add(13, new List<string> { "Bishkek", "Osh", "Jalal-Abad" });               // Kyrgzstan
        nations.Add(14, new List<string> { "Riga", "Daugavpils", "Liepaja" });              // Latvia
        nations.Add(15, new List<string> { "Vilnius", "Kaunas", "Klaipeda" });              // Lithuania
        nations.Add(16, new List<string> { "Chisinau", "Balti", "Tiraspol" });              // Moldova (Update from console protoype: Bender replced with Tiraspol)
        nations.Add(17, new List<string> { "Dushanbe", "Khujand", "Kulob" });               // Tajikistan
        nations.Add(18, new List<string> { "Ashgabat", "Turkmenabat", "Dasoguz" });         // Turkmenistan
        nations.Add(19, new List<string> { "Kyiv", "Kharkiv", "Odessa" });                  // Ukraine
        nations.Add(20, new List<string> { "Tashkent", "Samarkand", "Namangan" });          // Uzbekistan
    }

    // Populates the target sheet
    public void SessionTargets()
    {
        targetMinText.text = targetMin.ToString();
        targetCommText.text = targetComm.ToString();
        targetAwardText.text = targetAward.ToString();
    }

    // Generates a new mail item
    public void NewMailRequested()
    {
        countryRandomiser();
        mailAddress.text = "To: PO Box #" + Random.Range(1, 1000) + "\n" + AddressGenerator();
    }

    // Randomised generation of the mail item's address
    public void countryRandomiser()
    {
        if (difficulty == 0)
        {
            // Lowest difficulty [0] only uses countries #1 - 6
            challenge = Random.Range(1, 7);
        }
        else
        {
            challenge = Random.Range(1, nations.Count + 1);
        }
    }

    // Used to retrieve the country's city name
    public string AddressGenerator()
    {
        // Diifficulty determines how many cities are generated per nation. [0 & 1] = 1, [2] = 2, [3] = 3
        if (difficulty == 0)
        {
            return nations[challenge][0];

        }
        else
        {
            return nations[challenge][difficulty - 1];
        }
        
    }

    // Stores the player's togglebox answer in the send sheet
    public void AnswerReceiver(int checkbox)
    {
        answer = checkbox;
    }

    // Called when player confirms their answer by pressing the Send button
    public void AnswerChecker()
    {
        if (answer == challenge)
        {
            score++;
        }
        else
        {
            WrongAnswer();
        }
        LifeChecker();
    }

    // Called when the player gives an incorrect answer
    public void WrongAnswer()
    {
        // Changes button from "read" to "unread". Could also introduce an alert sound trigger here.
        buttonRead.SetActive(false);
        buttonUnread.SetActive(true);

        // Deducts 1 life, adds to the messages counter
        lifeCount--;
        messageCount++;
        if (firstMessage)
        {
            // clears the "(No messages)" text
            lifeMessages.text = messageCount + ". Warning: Incorrect recipient";
            firstMessage = false;
        }
        else
        {
            lifeMessages.text += "\n" + messageCount + ". Warning: Incorrect recipient";
        }
    }

    // Ends the gameplay session when the player has depleted their life count
    public void LifeChecker()
    {
        if (lifeCount <= 0)
        {
            EndGameSession();
        }
    }

    // Handles the gameplay timer, called regularly by Update()
    public void GameTimer()
    {
        if (countdownTimer > 0 && !pauseTimer)
        {
            countdownTimer -= Time.deltaTime;
        }
        else if (countdownTimer <= 0)
        {
            EndGameSession();
        }
    }
    
    // Toggles the gameplay timer on/off (i.e. when the player triggers the Quit Session window)
    public void StartStopTimer()
    {
        pauseTimer = !pauseTimer;
    }

    // Ends the game session and opens the End Session pop-up window and exports values to Results scene
    public void EndGameSession()
    {
        // stops Update() from running
        this.enabled = false;
        EndGameTextSetter();
        gameplayUI.SetActive(false);
        endUI.SetActive(true);
    }

    // Determines which end game text appears
    public void EndGameTextSetter()
    {
        if (lifeCount <= 0)
        {
            // Player has run out of lives
            endMessage.text = "HR has requested a meeting.";
        }
        else
        {
            // Timer has expired
            endMessage.text = "Time is up!";
        }
    }

    // Saves gameplay values to be retrieved by Results scene
    public void ExportToResults()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("lifeCount", lifeCount);
        PlayerPrefs.SetInt("targetMin", targetMin);
        PlayerPrefs.SetInt("targetComm", targetComm);
        PlayerPrefs.SetInt("targetAward", targetAward);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ProceedToResults()
    {
        ExportToResults();
        SceneManager.LoadScene("Results");
    }
}
