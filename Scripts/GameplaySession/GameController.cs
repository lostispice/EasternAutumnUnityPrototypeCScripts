using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages all primary functionality within the GameplaySession screen.
/// It is loaded alongside MailController.cs, MessagesController.cs, StampController.cs and SendSheetController.cs.
/// Future versions could further segregate this script's functionalities into even more separate scripts to enhance modularity.
/// </summary>
public class GameController : MonoBehaviour, ISave
{
    /// <summary>
    /// This variable defines the difficulty level for this game session.
    /// </summary>
    [SerializeField] int difficulty;

    /// <summary>
    /// These variables define the gameplay targets and player score for this game session.
    /// </summary>
    [SerializeField] int targetMin;
    [SerializeField] int targetComm;
    [SerializeField] int targetAward;
    [SerializeField] int score;

    /// <summary>
    /// These variables are used to update target sheet textboxes displayed to player at start of the session.
    /// </summary>
    [SerializeField] TextMeshProUGUI targetMinText;
    [SerializeField] TextMeshProUGUI targetCommText;
    [SerializeField] TextMeshProUGUI targetAwardText;

    /// <summary>
    /// This variable is used to hold a playset of countries and cities.
    /// Each country has an integer code (int) and a List containing three associated city names.
    /// </summary>
    [SerializeField] IDictionary<int, List<string>> nations = new Dictionary<int, List<string>>();

    /// <summary>
    /// These variables are contain the "Send To" toggleboxes.
    /// The version accessible to the player is dependent on difficulty level. "Easy" uses receiverSimple which removes unused countries. 
    /// </summary>
    [SerializeField] GameObject receiverSimple;
    [SerializeField] GameObject receiverFull;

    /// <summary>
    /// This variable holds the text written on a mail item.
    /// It is updated constantly throughout the session.
    /// </summary>
    [SerializeField] TextMeshProUGUI mailAddress;

    /// <summary>
    /// These variables are used by the game's question-answer challenge system.
    /// They are updated constantly throughout the session.
    /// </summary>
    [SerializeField] int challenge;
    [SerializeField] int answer;

    /// <summary>
    /// These variables are used by the game's life system.
    /// </summary>
    [SerializeField] int lifeCount; // Note: *not* visible to player, game design intentionally requires the player to count/guess their remaining lives.
    [SerializeField] bool extraLife;

    /// <summary>
    /// These variables are used to manage the Messages panel in the GUI.
    /// </summary>
    [SerializeField] GameObject buttonUnread;
    [SerializeField] GameObject buttonRead;
    [SerializeField] TextMeshProUGUI lifeMessages;
    [SerializeField] bool firstMessage;
    [SerializeField] int messageCount;

    /// <summary>
    /// These variables are used to manage the Timer system.
    /// </summary>
    [SerializeField] float countdownTimer; // Measured in seconds
    [SerializeField] bool pauseTimer;

    /// <summary>
    /// These variables are used by the "end session" window.
    /// </summary>
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject endUI;
    [SerializeField] TextMeshProUGUI endMessage;

    /// <summary>
    /// Unity calls this method automatically when the GameSession screen is first loaded.
    /// These methods serve to initialise the gameplay session before the player starts the timer.
    /// </summary>
    void Start()
    {
        SetValues();
        GeneratePlayset();
        TogglePlayset();
        SessionTargets();
    }

    /// <summary>
    /// Unity calls this method continuously, once per frame.
    /// It is used to manage the gameplay timer and associated functions.
    /// </summary>
    void Update()
    {
        GameTimer();
    }

    /// <summary>
    /// Retrives extra lives data from the player's profile.
    /// Ideally, the user should be playing each difficulty level consecutively.
    /// This method applies any "commendations" (extra lives) earned in the previous gameplay level.
    /// Uses the ISave interface.
    /// </summary>
    /// <param name="player"></param>
    public void LoadProfile(PlayerProfile player)
    {
        try
        {
            extraLife = player.extraLives[difficulty-1];
        }
        catch
        {
            extraLife = false; // Extra lives do not apply on easy difficulty for story & gameplay purposes
        }
    }

    /// <summary>
    /// Not used on this screen, changes are applied in the Results screen.
    /// Part of ISave. Prevents CS0535 error in VStudio.
    /// </summary>
    /// <param name="player"></param>
    public void SaveProfile(PlayerProfile player) { }

    /// <summary>
    /// (Re)sets the gameplay values for this session, according to the parameters set by the player.
    /// PlayerPrefs is used to retrieve values set by the player in the MainMenu or Results screens.
    /// </summary>
    public void SetValues()
    {
        pauseTimer = true;
        firstMessage = true;
        difficulty = PlayerPrefs.GetInt("difficulty");
        countdownTimer = PlayerPrefs.GetInt("timerModifier"); // The player can reduce the timer for increased challenge.
        score = 0;
        SetTargets();
        LoadProfile(SaveManager.instance.player);
        SetLifeCounter(extraLife);
    }

    /// <summary>
    /// Sets the gameplay target values for this session.
    /// Default minimum target is 5, with secondary targets being multipliers of the minimum target.
    /// </summary>
    public void SetTargets()
    {
        targetMin = PlayerPrefs.GetInt("targetModifier"); // The player can raise the target(s) for increased challenge.
        targetComm = targetMin * 2;
        targetAward = targetMin * 4;
    }

    /// <summary>
    /// Determines how many lives (default: 3) the player has in this gameplay session. 
    /// If the player has earned any extra lives, this is applied and the messages panel is updated to inform the player accordingly.
    /// </summary>
    /// <param name="extraLives"></param>
    public void SetLifeCounter(bool extraLives)
    {
        lifeCount = PlayerPrefs.GetInt("livesModifier"); // The player can reduce the life counter for increased challenge.
        if (extraLives)
        {
            lifeCount++;
            messageCount++;
            lifeMessages.text = messageCount + ". Commendation previously earned.";
            firstMessage = false;
        }
    }

    /// <summary>
    /// Generates the playset using (1996-era) city names.
    /// Special non-latin characters have been simplified to latin characters due to font limitations.
    /// In this version non-Soviet dissolution/reunification nations (East Germany, Czechoslovakia & Yugoslavia) are omitted for gameplay simplicity.
    /// Future versions can load these values from an external Playset config file. 
    /// </summary>
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
        nations.Add(16, new List<string> { "Chisinau", "Balti", "Tiraspol" });              // Moldova
        nations.Add(17, new List<string> { "Dushanbe", "Khujand", "Kulob" });               // Tajikistan
        nations.Add(18, new List<string> { "Ashgabat", "Turkmenabat", "Dasoguz" });         // Turkmenistan
        nations.Add(19, new List<string> { "Kyiv", "Kharkiv", "Odessa" });                  // Ukraine
        nations.Add(20, new List<string> { "Tashkent", "Samarkand", "Namangan" });          // Uzbekistan
    }

    /// <summary>
    /// Determines which togglebox set is displayed.
    /// Easy difficulty (0) will display a simplified list with Soviet SSRs removed as only Eastern bloc nations will appear at that difficulty.
    /// </summary>
    public void TogglePlayset()
    {
        if (difficulty == 0)
        {
            receiverSimple.gameObject.SetActive(true);
        }
        else
        {
            receiverFull.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Populates the target sheet displayed to the player at the start of the session.
    /// </summary>
    public void SessionTargets()
    {
        targetMinText.text = targetMin.ToString();
        targetCommText.text = targetComm.ToString();
        targetAwardText.text = targetAward.ToString();
    }

    /// <summary>
    /// Generates a new mail item used in the gameplay loop.
    /// The player will be shown an address with a randomised PO Box number and a random city name.
    /// </summary>
    public void NewMailRequested()
    {
        CountryRandomiser();
        mailAddress.text = "To: PO Box #" + Random.Range(1, 1000) + "\n" + AddressGenerator();
    }

    /// <summary>
    /// Generates a random country (not shown the player) which is then used to generate a random city address.
    /// The challenge value that is generated is also used to check the player's answer value.
    /// </summary>
    public void CountryRandomiser()
    {
        if (difficulty == 0)
        {            
            challenge = Random.Range(1, 6); // Easy difficulty (0) only uses countries #1 - 5
        }
        else
        {
            challenge = Random.Range(1, nations.Count + 1);
        }
    }

    /// <summary>
    /// Generates a random city address using the randomly selected country.
    /// The pool of potential city names used is dependent on the current difficulty level, with higher difficulties widening the pool.
    /// Easy & Medium = 1 City
    /// Hard = 2 Cities
    /// Expert = 3 Cities
    /// </summary>
    public string AddressGenerator()
    {
        if (difficulty == 0)
        {
            return nations[challenge][0];

        }
        else
        {
            return nations[challenge][difficulty - 1];
        }
        
    }

    /// <summary>
    /// Used to store the player's answer.
    /// In game, the player marks their answer using a labelled togglebox on the Send sheet.
    /// This answer is then checked against the challenge value.
    /// </summary>
    /// <param name="checkbox"></param>
    public void AnswerReceiver(int checkbox)
    {
        answer = checkbox;
    }

    /// <summary>
    /// Called when player confirms their answer by pressing the Send button.
    /// The player is *not* informed of correct answers, but is warned about incorrect answers in the messages panel.
    /// </summary>
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

    /// <summary>
    /// Called when the player gives an incorrect answer.
    /// Reduces the player's life counter by 1, and displays a warning message in the messages panel.
    /// It also changes the Messages button status from "read" to "unread". 
    /// Future versions could also include an alert sound.
    /// </summary>
    public void WrongAnswer()
    {
        buttonRead.SetActive(false);
        buttonUnread.SetActive(true);
        lifeCount--;
        messageCount++;
        if (firstMessage)
        {            
            lifeMessages.text = messageCount + ". Warning: Incorrect recipient"; // clears the "(No messages)" text
            firstMessage = false;
        }
        else
        {
            lifeMessages.text += "\n" + messageCount + ". Warning: Incorrect recipient";
        }
    }

    /// <summary>
    /// Ends the gameplay session automatically when the player has depleted their life count.
    /// </summary>
    public void LifeChecker()
    {
        if (lifeCount <= 0)
        {
            EndGameSession();
        }
    }

    /// <summary>
    /// Handles the gameplay timer, called regularly by Update().
    /// Ends the gameplay session automatically when the timer has expired.
    /// </summary>
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
    
    /// <summary>
    /// Toggles the gameplay timer on/off.
    /// It is called when the player presses "start" in the targets window at the beginning of the session, starting the timer.
    /// It is also called when the player opens/closes the Quit Session window.
    /// </summary>
    public void StartStopTimer()
    {
        pauseTimer = !pauseTimer;
    }

    /// <summary>
    /// Ends the game session and opens the End Session pop-up window.
    /// </summary>
    public void EndGameSession()
    {        
        this.enabled = false; // stops Update() from running
        EndGameTextSetter();
        gameplayUI.SetActive(false);
        endUI.SetActive(true);
    }

    /// <summary>
    /// Determines which end game text appears in the End Session window.
    /// </summary>
    public void EndGameTextSetter()
    {
        if (lifeCount <= 0) // Player has run out of lives (failure).
        {            
            endMessage.text = "HR has requested a meeting.";
        }
        else // Timer has expired
        {            
            endMessage.text = "Time is up!";
        }
    }

    /// <summary>
    /// Saves gameplay session values to be retrieved by Results scene.
    /// </summary>
    public void ExportToResults()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("lifeCount", lifeCount);
        PlayerPrefs.SetInt("targetMin", targetMin);
        PlayerPrefs.SetInt("targetComm", targetComm);
        PlayerPrefs.SetInt("targetAward", targetAward);
    }

    /// <summary>
    /// Exits the game session and returns to Main Menu screen.
    /// </summary>
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Exits the game session and proceeds to Results screen.
    /// </summary>
    public void ProceedToResults()
    {
        ExportToResults();
        SceneManager.LoadScene("Results");
    }
}
