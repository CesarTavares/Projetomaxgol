﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour
{
    public int fase = -1;
    [SerializeField]
    public GameObject UiManagerGO,GameManagerGO;

    public static OndeEstou instance;
    public int bolaEmUso;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad (this.gameObject);

        }
        else
        {
            Destroy (gameObject);
        }

        SceneManager.sceneLoaded += VerificaFase;
        bolaEmUso = PlayerPrefs.GetInt ("BolaUse");
    }

    void VerificaFase(Scene cena, LoadSceneMode modo)
    {
        fase = SceneManager.GetActiveScene ().buildIndex;

        if(fase != 4 && fase != 5)
        {
            Instantiate (UiManagerGO);
            Instantiate (GameManagerGO);
        }
    }
}

