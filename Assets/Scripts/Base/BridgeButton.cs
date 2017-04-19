﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BridgeButton : MonoBehaviour
{

    public void Click()
    {
        SceneManager.LoadSceneAsync("Bridge", LoadSceneMode.Single);
    }
}
