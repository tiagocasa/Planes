using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;
using TMPro;

public class Notifications : MonoBehaviour
{
    public MenuManager mg;
    public bool NotificationActivated = true;
    public int hours;
    [SerializeField] private GameObject Bell_Cont;
    // Start is called before the first frame update
    void Start()
    {
        var c = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);

        if (PlayerPrefs.GetInt("Notification", 1) == 1)
        {
            NotificationActivated = true;
        }
        else
        {
            NotificationActivated = false;
        }

        if (NotificationActivated)
        {

                SendNotification("Can you escape in time?", "Come back try to get the highscore");
        }

        SettingsON();
    }

  
    public void SendNotification(string title, string msg)
    {
        var notification = new AndroidNotification
        {
            Title = title,
            Text = msg,
            LargeIcon = "large_icon",
            SmallIcon = "small_icon",
            FireTime = System.DateTime.Now.AddHours(hours)
        };

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");
        var notificationStatus = AndroidNotificationCenter.CheckScheduledNotificationStatus(id);

        if (notificationStatus == NotificationStatus.Scheduled)
        {
            //Replace the schelued notification with a new notification
            AndroidNotificationCenter.UpdateScheduledNotification(id, notification, "channel_id");
        }
        else if (notificationStatus == NotificationStatus.Delivered)
        {
            //Remove the previously shown notificaion from the status bar
            AndroidNotificationCenter.CancelNotification(id);
        }
        else if (notificationStatus == NotificationStatus.Unknown)
        {
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }

    }

    public void NotificationOnOff()
    {
        FindObjectOfType<AudioManager>().Play("botao");

        if (NotificationActivated)
        {
            NotificationActivated = false;
            //Bell_Cont.transform.GetChild(2).GetChild(1).GetComponent<Toggle>().isOn = false;
            PlayerPrefs.SetInt("Notification", 0);

        }
        else
        {
            NotificationActivated = true;
            //Bell_Cont.transform.GetChild(2).GetChild(1).GetComponent<Toggle>().isOn = true;
            PlayerPrefs.SetInt("Notification", 1);

        }

    }

    public void SettingsON()
    {
        if (PlayerPrefs.GetInt("Notification", 1) == 1)
        {
            NotificationActivated = true;
        }
        else
        {
            NotificationActivated = false;
        }

        if (!NotificationActivated)
        {
            Bell_Cont.transform.GetChild(2).GetChild(2).GetComponent<Toggle>().isOn = false;
        }

        if (Bell_Cont.transform.GetChild(2).GetChild(2).GetComponent<Toggle>().isOn == false)
        {
            PlayerPrefs.SetInt("Notification", 0);
            NotificationActivated = false;
        }

    }


}
