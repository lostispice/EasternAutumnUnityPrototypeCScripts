using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour, ISave
{
    // Difficulty level for this game session
    [SerializeField] int difficulty;

    // Gameplay Targets. TODO - Write a method that sets targets based on difficulty/session id.
    // TODO - Implement target modifier that can be adjusted in main menu
    [SerializeField] int targetMin;
    [SerializeField] int targetComm;
    [SerializeField] int targetAward;
    [SerializeField] int score;

    // Used to update target sheet textboxes displayed to player at start of session
    [SerializeField] TextMeshProUGUI targetMinText;
    [SerializeField] TextMeshProUGUI targetCommText;
    [SerializeField] TextMeshProUGUI targetAwardText;

    // Used to generate country playset for the given difficulty level
    [SerializeField] IDictionary<int, List<string>> nations = new Dictionary<int, List<string>>();

    // UI text written on mail item
    [SerializeField] TextMeshProUGUI mailAddress;

    // Used to generate a random challenge and then checked against the player's answer
    [SerializeField] int challenge;
    [SerializeField] int answer;

    // Life-counter. TODO - Implement lifecount modifier that can be adjusted in main menu
    [SerializeField] int lifeCount;
    [SerializeField] bool extraLife;

    // Used to manage the Messages (life counter) panel
    [SerializeField] GameObject buttonUnread;
    [SerializeField] GameObject buttonRead;
    [SerializeField] TextMeshProUGUI lifeMessages;
    [SerializeField] bool firstMessage = true;
    [SerializeField] int messageCount;

    // Timer value, measured in seconds.
    // TODO - Implement lifecount modifier that can be adjusted in main menu. Consider raising the timer value for higher difficulties
    [SerializeField] float countdownTimer = 60;
    [SerializeField] bool pauseTimer = true;

    // Used to end the game session
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject endUI;
    [SerializeField] TextMeshProUGUI endMessage;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = PlayerPrefs.GetInt("difficulty");
        LoadProfile(SaveManager.instance.player);
        SetValues();
        GeneratePlayset();
        SessionTargets();
    }

    // Update is called once per frame
    void Update()
    {
        GameTimer();
    }

    // (ISave) Retrives extra lives data earned from the previous difficulty level
    public void LoadProfile(PlayerProfile player)
    {
        try
        {
            extraLife = player.extraLives[difficulty-1];
        }
        catch // Extra lives do not apply on easy difficulty for story & gameplay purposes
        {
            extraLife = false;
        }
    }

    // (ISave) Not used on this screen, changes are applied in the Results screen. 
    public void SaveProfile(PlayerProfile player) { }

    // Resets the gameplay counters to default values. Additonal code could be implemented to make these targets difficulty-dependant.
    public void SetValues()
    {
        targetMin = 5;
        targetComm = 10;
        targetAward = 20;
        score = 0;
        SetLifeCounter(extraLife);
    }

    // Determines how many lives the player has in this gameplay session. Default is 3.
    public void SetLifeCounter(bool extraLives)
    {
        if (extraLives)
        {
            lifeCount = 4;
            // Updates the life messages to inform the player of their extra life
            messageCount++;
            lifeMessages.text = messageCount + ". Commendation previously earned.";
            firstMessage = false;
        }
        else
        {
            lifeCount = 3;
        }
    }

    // Generates the level's country playset, using 1996 city names. Special non-latin characters simplified to latin characters.
    // Non-Soviet dissolution/reunification nations like East Germany, Czechoslovakia & Yugoslavia are omitted for gameplay simplicity. Could be introduced in later versions.
    public void GeneratePlayset()
    {
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
        // Generates a random PO box number
        mailAddress.text = "To: PO Box #" + Random.Range(1, 1000) + "\n" + AddressGenerator();
    }

    // Generates a random mail item's address, the challenge value is then checked against the player's answer
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
        // Changes button from "read" to "unread". An alert sound trigger could also be inserted here.
        buttonRead.SetActive(false);
        buttonUnread.SetActive(true);

        // Deducts 1 life, adds to the penalty messages counter
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
