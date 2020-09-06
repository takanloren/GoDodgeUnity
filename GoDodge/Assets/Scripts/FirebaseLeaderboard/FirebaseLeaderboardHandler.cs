using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class FirebaseLeaderboardHandler
{
    private static FirebaseLeaderboardHandler _instance;

    // Firebase Database reference
    private DatabaseReference DBRef;

    public static FirebaseLeaderboardHandler Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new FirebaseLeaderboardHandler();
            }

            return _instance;
        }
    }
    

    public void SetupDBReference()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://gododge-1aa10.firebaseio.com/");

        // Get the root reference location of the database.
        DBRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SubmitScore(LeaderboardEntry entry)
    {
        string json = JsonUtility.ToJson(entry);
        Debug.Log("JSON DATA " + json);
        DBRef.Child("Leaderboard").Child(entry.map).Push().SetRawJsonValueAsync(json);
    }

    public string TimeSpanToString(double timeInMiliseconds)
    {
        TimeSpan timeSpan = TimeSpan.FromMilliseconds(timeInMiliseconds);
        string totalTimeText = "";

        if (timeSpan.TotalMinutes < 1.0)
        {
            totalTimeText = String.Format("{0}s", timeSpan.Seconds);
        }
        else if (timeSpan.TotalHours < 1.0)
        {
            totalTimeText = String.Format("{0}m : {1:D2}s", timeSpan.Minutes, timeSpan.Seconds);
        }
        else // more than 1 hour
        {
            totalTimeText = String.Format("{0}h : {1:D2}m : {2:D2}s", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
        }

        return totalTimeText;
    }
}
