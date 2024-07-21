using UnityEngine;

public class SendSheetController : MonoBehaviour
{

    // Handles new mail item animations, placeholer values
    public bool newMailRequest = false;
    public Vector2 destinationStart = new Vector2(-3, 6);
    public Vector2 destinationNew = new Vector2(-3, -1);

    // Handles mail item being "sent off(screen)"
    public bool sendMail = false;
    public Vector2 destinationSend = new Vector2(100, 0);

    // Start is called before the first frame update
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        if (newMailRequest)
        {
            transform.position = Vector2.Lerp(transform.position, destinationNew, Time.deltaTime);
        }
        if (sendMail)
        {
            transform.position = Vector2.Lerp(transform.position, destinationSend, Time.deltaTime);
        }
    }


    // Moves the object into the player's FOV
    public void NewMailRequested()
    {
        transform.position = destinationStart;
        newMailRequest = true;
        sendMail = false;
    }

    // Sends the object offscreen beyond the player's FOV
    public void SendMailOut()
    {
        sendMail = true;
        newMailRequest = false;
    }

}