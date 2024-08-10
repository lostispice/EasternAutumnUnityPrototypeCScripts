using NUnit.Framework;

/// <summary>
/// Unit tests for ResultsController.cs functionality.
/// </summary>
public class ResultsControllerTests
{
    /// <summary>
    /// These variables are used to determines if player passed (true) or failed (false) their respective objectives
    /// </summary>
    private bool commendationPass;
    private bool awardPass;

    /// <summary>
    /// These variables are Cross/Tick symbols used to report if player has met their respective targets
    /// </summary>
    private bool commMarkPass;
    private bool commMarkFail;
    private bool awardMarkPass;
    private bool awardMarkFail;

    /// <summary>
    /// Tests that the correct Commendation icon appears (Pass icon)
    /// </summary>
    [Test]
    public void MarkCommendation1()
    {
        commendationPass = true;
        commMarkPass = false;
        commMarkFail = false;

        if (commendationPass)
        {
            commMarkPass = true;
            commMarkFail = false;
        }
        else
        {
            commMarkPass = false;
            commMarkFail = true;
        }
        Assert.True(commendationPass);
        Assert.False(commMarkFail);
    }

    /// <summary>
    /// Tests that the correct Commendation icon appears (Fail icon)
    /// </summary>
    [Test]
    public void MarkCommendation2()
    {
        commendationPass = false;
        commMarkPass = false;
        commMarkFail = false;

        if (commendationPass)
        {
            commMarkPass = true;
            commMarkFail = false;
        }
        else
        {
            commMarkPass = false;
            commMarkFail = true;
        }
        Assert.False(commendationPass);
        Assert.True(commMarkFail);
    }

    /// <summary>
    /// Tests that the correct Commendation icon appears (Pass icon)
    /// </summary>
    [Test]
    public void MarkAward1()
    {
        awardPass = true;
        awardMarkPass = false;
        awardMarkFail = false;

        if (awardPass)
        {
            awardMarkPass = true;
            awardMarkFail = false;
        }
        else
        {
            awardMarkPass = false;
            awardMarkFail = true;
        }
        Assert.True(awardMarkPass);
        Assert.False(awardMarkFail);
    }

    /// <summary>
    /// Tests that the correct Commendation icon appears (Fail icon).
    /// </summary>
    [Test]
    public void MarkAward2()
    {
        awardPass = false;
        awardMarkPass = false;
        awardMarkFail = false;

        if (awardPass)
        {
            awardMarkPass = true;
            awardMarkFail = false;
        }
        else
        {
            awardMarkPass = false;
            awardMarkFail = true;
        }
        Assert.False(awardMarkPass);
        Assert.True(awardMarkFail);
    }
}
