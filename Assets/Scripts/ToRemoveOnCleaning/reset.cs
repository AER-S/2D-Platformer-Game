using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour
{
    public void Reset()
    {
        ProfileController.ResetLock();
        ProfileController.UpdateProfile();
    }
}
