using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EggCount : MonoBehaviour
{
    [SerializeField]
    private int dailyEggMin = 10;
    [SerializeField]
    private TMP_Text numEggDisplay;
    [SerializeField]
    private bool testingEggOverride = false;
    [SerializeField]
    [Tooltip("Number of eggs recharged for one hour of not checking in")]
    private int numEggsRechargedPerHour = 1;
    [SerializeField]
    [Tooltip("Popup that should display when the user is out of eggs")]
    private GameObject outOfEggsPopup;


    private int numEggs;

    private const string EGG_NUM_PLAYER_PREF_KEY = "DragonDropPlayerEggNum";
    private const string EGG_LAST_CHECKIN_TIMESTAMP_KEY = "DragonDropPlayerEggLastCheckinTimestamp";
    private const double NEVER_CHECKED_IN = -1.0;


    private void Start()
    {
        numEggs = GetUserEggs();
        UpdateEggDisplay();
    }

    private int GetUserEggs()
    {
        if(testingEggOverride)
        {
            return dailyEggMin;
        }

        int playerEggs = PlayerPrefs.GetInt(EGG_NUM_PLAYER_PREF_KEY);
        int dailyEggCheck = GetDailyEggs();

        // only add daily eggs if we are under the min
        if (playerEggs < dailyEggMin)
        {
            // if under daily egg min cannot get more than daily egg min
            playerEggs = Math.Min(playerEggs + dailyEggCheck, dailyEggMin);
        }

        return playerEggs;
    }

    private int GetDailyEggs()
    {
        // gives back up to daily egg minimum number of eggs. restores an egg every hour
        int hoursSinceLastCheck = (int) GetHoursSinceLastCheckin();
        CheckIn();
        if (hoursSinceLastCheck == NEVER_CHECKED_IN)
        {
            return dailyEggMin;
        } else
        {
            int numEggsRecharged = numEggsRechargedPerHour * hoursSinceLastCheck;
            return numEggsRecharged;
        }
    }

    private void CheckIn()
    {
        // only register an egg check-in if it has been more than an hour since last check-in
        // or if they have never checked in before
        int hoursSinceLastCheckIn = (int)GetHoursSinceLastCheckin();
        Debug.Log("Hours since last check in is: " + hoursSinceLastCheckIn);
        if (hoursSinceLastCheckIn == NEVER_CHECKED_IN || hoursSinceLastCheckIn > 1.0)
        {
            Debug.Log("It's been more than an hour so checking in");
            string currentTime = DateTime.Now.ToBinary().ToString();
            PlayerPrefs.SetString(EGG_LAST_CHECKIN_TIMESTAMP_KEY, currentTime);
        }
    }

    // returns -1 if never checked in otherwise returns hours since last check in.
    private double GetHoursSinceLastCheckin()
    {
        DateTime currentTime = DateTime.Now;

        String lastCheckinString = PlayerPrefs.GetString(EGG_LAST_CHECKIN_TIMESTAMP_KEY);
        DateTime lastcheckinTime;
        if (String.IsNullOrEmpty(lastCheckinString))
        {
            // never logged in before so return special negative number
            return NEVER_CHECKED_IN;
        }
        else
        {
            long timestamp = Convert.ToInt64(lastCheckinString);
            lastcheckinTime = DateTime.FromBinary(timestamp);
        }
        TimeSpan timeSinceLastCheckin = currentTime.Subtract(lastcheckinTime);
        return timeSinceLastCheckin.TotalHours;
    }

    private void SetUserEggs(int numEggs)
    {
        PlayerPrefs.SetInt(EGG_NUM_PLAYER_PREF_KEY, numEggs);
    }

    public bool HasEggs()
    {
        return numEggs > 0;
    }

    public void UseEgg()
    {
        numEggs--;
        UpdateEggDisplay();
        // only update the number of eggs if we aren't in override mode
        if (!testingEggOverride)
        {
            SetUserEggs(numEggs);
        }
    }

    //rewarded Ad adds eggs
    public void AddEgg(int num)
    {
        numEggs+= num;
        UpdateEggDisplay();
        // only update the number of eggs if we aren't in override mode
        if (!testingEggOverride)
        {
            SetUserEggs(numEggs);
        }
    }

        private void UpdateEggDisplay()
    {
        numEggDisplay.text = "x" + numEggs;
    }

    public void DisplayOutOfEggs()
    {
        outOfEggsPopup.SetActive(true);
    }

    public void HideOutOfEggs()
    {
        outOfEggsPopup.SetActive(false);
    }

}
