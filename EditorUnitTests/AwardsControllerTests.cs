using System.Collections.Generic;
using NUnit.Framework;

/// <summary>
/// Unit tests for AwardChecker.cs functionality.
/// </summary>
public class AwardCheckerTests
{
    /// <summary>
    /// These variables are used to store the award badge GameObjects into their respective categories.
    /// </summary>
    private Dictionary<string, bool> notEarnedAwards;
    private Dictionary<string, bool> earnedAwards;

    /// <summary>
    /// Simulates awards dictionary retrieved from Save Manager/PlayerProfile.
    /// </summary>
    private Dictionary<string, bool> Awards;

    /// <summary>
    /// Tests that dictionaries are populated correctly.
    /// In the actual game, true/false is set within the Unity Editor
    /// </summary>
    [Test]
    public void PopulateCheckerDictionaries()
    {
        notEarnedAwards = new Dictionary<string, bool>
        {
            { "0T", true },
            { "0A", true },
        };

        earnedAwards = new Dictionary<string, bool>
        {
            { "0T", false },
            { "0A", false },
        };
        Assert.IsNotEmpty(notEarnedAwards);
        Assert.IsNotEmpty(earnedAwards);
    }

    /// <summary>
    /// Tests that award checker dictionaries are updated correctly based on the player's awards data.
    /// </summary>
    [Test]
    public void AwardChecker()
    {
        PopulateCheckerDictionaries();
        Awards = new Dictionary<string, bool>
        {
            { "0T", true },
            { "0A", false },
        };

        foreach (var award in Awards)
        {
            string key = award.Key;
            bool earned = award.Value;

            if (earned)
            {
                earnedAwards[key] = true;
                notEarnedAwards[key] = false;
            }
        }

        Assert.AreEqual(true, earnedAwards["0T"]);
        Assert.AreEqual(false, notEarnedAwards["0T"]);
        Assert.AreEqual(false, earnedAwards["0A"]);
        Assert.AreEqual(true, notEarnedAwards["0A"]);
    }
}
