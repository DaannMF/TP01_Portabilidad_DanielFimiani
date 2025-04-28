using System;
using System.Collections;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationsManager : MonoBehaviour {
    private static string CHANNEL_ID = "notis";

    void Start() {
        // Check if the notification channel has already been created
        if (Convert.ToBoolean(PlayerPrefs.GetInt("NotisChanel_Created"))) {
            ScheduleNotification();
            return;
        }

        // Create the notification group
        var group = new AndroidNotificationChannelGroup() {
            Id = "Main",
            Name = "Main Notifications"
        };
        AndroidNotificationCenter.RegisterNotificationChannelGroup(group);

        // Create the notification channel
        var channel = new AndroidNotificationChannel() {
            Id = CHANNEL_ID,
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Main notifications for the app.",
            Group = group.Id
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        StartCoroutine(RequestNotificationsPermission());

        // Set the PlayerPrefs key to indicate that the channel has been created
        PlayerPrefs.SetInt("NotisChanel_Created", 1);
        PlayerPrefs.Save();
    }

    private IEnumerator RequestNotificationsPermission() {
        // Request permission to send notifications
        var request = new PermissionRequest();

        while (request.Status == PermissionStatus.RequestPending)
            yield return new WaitForEndOfFrame();

        // Schedule notifications if permission is granted
        ScheduleNotification();
    }

    public void ScheduleNotification() {
        // Cancel all previous notifications
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        // Create a new notification
        var notification10Minutes = new AndroidNotification() {
            Title = "TP01 Portabilida y optimización",
            Text = "Juego creado por Daniel Fimiani",
            FireTime = System.DateTime.Now.AddMinutes(10),
        };

        // Schedule the notification
        AndroidNotificationCenter.SendNotification(notification10Minutes, CHANNEL_ID);
    }
}
