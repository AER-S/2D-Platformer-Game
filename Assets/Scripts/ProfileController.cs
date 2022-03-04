using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProfileController
{
    private static int unlockedLevel;

    public static void UpdateProfile()
    {
        unlockedLevel=PlayerPrefs.GetInt("unlocked", 1);
    }

    public static void ResetLock()
    {
        PlayerPrefs.SetInt("unlocked",1);
    }

    public static int Getunlocked()
    {
        UpdateProfile();
        return unlockedLevel;
    }

    public static void UpdateLocked(int _index)
    {
        if (_index>unlockedLevel)
        {
            PlayerPrefs.SetInt("unlocked", _index);
            UpdateProfile();
        }
    }
}
