using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Unit tests for GameController.cs functionality.
/// </summary>
public class GameControllerTests
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

    /// <summary>
    /// This variable is used to hold a playset of countries and cities.
    /// Each country has an integer code (int) and a List containing three associated city names.
    /// </summary>
    [SerializeField] IDictionary<int, List<string>> nations = new Dictionary<int, List<string>>();

    /// <summary>
    /// These variables are used by the game's question-answer challenge system.
    /// They are updated constantly throughout the session.
    /// </summary>
    [SerializeField] int challenge;

    /// <summary>
    /// These variables are used by the game's life system.
    /// </summary>
    [SerializeField] int lifeCount;
    [SerializeField] bool extraLife;

    /// <summary>
    /// These variables are used to manage the Timer system.
    /// </summary>
    [SerializeField] float countdownTimer;
    [SerializeField] bool pauseTimer;

    /// <summary>
    /// Tests LoadProfile at Easy difficulty
    /// </summary>
    /// <param name="player"></param>
    [Test]
    public void LoadProfile1()
    {
        difficulty = 0;
        Dictionary<int, bool> extraLives = new Dictionary<int, bool>()
        {
            {0, true },
            {1, true }
        };

        try
        {
            extraLife = extraLives[difficulty - 1];
        }
        catch
        {
            extraLife = false;
        }

        Assert.False(extraLife);
    }

    /// <summary>
    /// Tests LoadProfile at any difficulty higher than Easy.
    /// </summary>
    [Test]
    public void LoadProfile2()
    {
        difficulty = 1;
        Dictionary<int, bool> extraLives = new Dictionary<int, bool>()
        {
            {0, true },
            {1, true }
        };

        try
        {
            extraLife = extraLives[difficulty - 1];
        }
        catch
        {
            extraLife = false;
        }

        Assert.True(extraLife);
    }

    /// <summary>
    /// Simulates SetTargets populating values (using default value)
    /// </summary>
    [Test]
    public void SetTargets1()
    {
        int retreived = 5;
        targetMin = retreived; 
        targetComm = targetMin * 2;
        targetAward = targetMin * 4;

        Assert.That(targetMin, Is.EqualTo(5));
        Assert.That(targetComm, Is.EqualTo(10));
        Assert.That(targetAward, Is.EqualTo(20));
    }

    /// <summary>
    /// Simulates SetTargets populating values (using non-default value)
    /// </summary>
    [Test]
    public void SetTargets2()
    {
        int retreived = 10;
        targetMin = retreived;
        targetComm = targetMin * 2;
        targetAward = targetMin * 4;

        Assert.That(targetMin, Is.EqualTo(10));
        Assert.That(targetComm, Is.EqualTo(20));
        Assert.That(targetAward, Is.EqualTo(40));
    }

    /// <summary>
    /// Simulates CountryRandomiser at Easy difficulty
    /// </summary>
    [Test]
    public void CountryRandomiser1()
    {
        difficulty = 0;

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

        if (difficulty == 0)
        {
            challenge = Random.Range(1, 6);
        }
        else
        {
            challenge = Random.Range(1, nations.Count + 1);
        }

        Assert.That(challenge, Is.InRange(1, 5));
    }

    /// <summary>
    /// Simulates CountryRandomiser at non-Easy difficulty
    /// </summary>
    [Test]
    public void CountryRandomiser2()
    {
        difficulty = 1;

        if (difficulty == 0)
        {
            challenge = Random.Range(1, 6);
        }
        else
        {
            challenge = Random.Range(1, nations.Count + 1);
        }

        Assert.That(challenge, Is.InRange(1, 20));
    }

    /// <summary>
    /// Simulates LifeChecker triggering EndGameSession when the player depletes their lives
    /// </summary>
    [Test]
    public void LifeChecker1()
    {
        bool endGame = false;
        lifeCount = 0;

        if (lifeCount <= 0)
        {
            endGame = true;
        }

        Assert.That(endGame, Is.True);
    }

    /// <summary>
    /// Simulates LifeChecker does not trigger EndGameSession when the player still has lives remaining
    /// </summary>
    [Test]
    public void LifeChecker2()
    {
        bool endGame = false;
        lifeCount = 3;

        if (lifeCount <= 0)
        {
            endGame = true;
        }

        Assert.That(endGame, Is.False);
    }

    /// <summary>
    /// Simulates that GameTimer will stop the game when countdownTimer is depleted
    /// </summary>
    [Test]
    public void GameTimer1()
    {
        bool endGame = false;

        countdownTimer = -0.1f;
        pauseTimer = false;

        if (countdownTimer > 0 && !pauseTimer)
        {
            countdownTimer -= Time.deltaTime;
        }
        else if (countdownTimer <= 0)
        {
            endGame = true;
        }
        Assert.True(endGame);
    }

    /// <summary>
    /// Simulates that GameTimer will not stop the game when countdownTimer is still active
    /// </summary>
    [Test]
    public void GameTimer2()
    {
        bool endGame = false;

        countdownTimer = 10f;
        pauseTimer = false;

        if (countdownTimer > 0 && !pauseTimer)
        {
            countdownTimer -= Time.deltaTime;
        }
        else if (countdownTimer <= 0)
        {
            endGame = true;
        }
        Assert.False(endGame);
    }
}
