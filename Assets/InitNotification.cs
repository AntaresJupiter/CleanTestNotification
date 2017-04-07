using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InitNotification : MonoBehaviour
{

    public Text lbl_console;
    // Use this for initialization
    void Start()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        Log("Inited");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        //UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
        File.WriteAllText(Application.persistentDataPath + "/token.txt", token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        var notification = e.Message.Notification;
        if (notification != null)
        {
            Log("title: " + notification.Title);
            Log("body: " + notification.Body);
        }
        if (e.Message.From.Length > 0)
            Log("from: " + e.Message.From);

        if (e.Message.Data.Count > 0)
        {
            foreach (var iter in e.Message.Data)
            {
                Log("\t-" + iter.Key + ": " + iter.Value);
            }
        }
    }

    public void Log(string Msg)
    {
        lbl_console.text += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " - " + Msg + "\n";
    }


    public void ClearConsole()
    {
        lbl_console.text = "";
    }
}
