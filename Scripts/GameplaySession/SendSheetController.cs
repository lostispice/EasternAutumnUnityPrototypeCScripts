using UnityEngine;

/// <summary>
/// This script handles the visibility of the Send Sheet in the player view.
/// It is loaded alongside GameController.cs, MessagesController.cs, MailController.cs and SendSheetController.cs.
/// </summary>
public class SendSheetController : MonoBehaviour
{
    /// <summary>
    /// These variables are used for the "new mail item" animations
    /// </summary>
    [SerializeField] bool newMailRequest = false;
    public Vector2 destinationNew = new Vector2(-3, -1);

    /// <summary>
    /// These variables are used for "send mail item" animations
    /// </summary>
    [SerializeField] bool sendMail = false;
    [SerializeField] Vector2 destinationSend = new Vector2(10, 0);

    /// <summary>
    /// Unity calls this method continuously, once per frame.
    /// It is used to determine which position the send sheet should be moving to.
    /// </summary>
    void Update()
    {
        if (newMailRequest)
        {
            transform.position = destinationNew;
        }
        if (sendMail)
        {
            transform.position = Vector2.Lerp(transform.position, destinationSend, Time.deltaTime);
        }
    }

    /// <summary>
    /// Moves the sheet into the player's FOV.
    /// Originally, this was designed to move into view using physics (like the mail item) however this simultenous movement created a disorientating effect.
    /// In this version, the send sheet simply "teleports" to the required position.
    /// </summary>
    public void NewMailRequested()
    {
        newMailRequest = true;
        sendMail = false;
    }

    /// <summary>
    /// Sends the sheet offscreen beyond the player's FOV
    /// </summary>
    public void SendMailOut()
    {
        sendMail = true;
        newMailRequest = false;
    }

}
