using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
   public static UIManager instance;
   private Text pontosUI,bolasUI;


   void Awake()
   {
       if(instance == null)
       {
            instance = this;
            DontDestroyOnLoad (this.gameObject);   
       }

       else
       {
           Destroy(gameObject);
       }

       SceneManager.sceneLoaded += Carrega;

    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        pontosUI = GameObject.Find ("PontosUI").GetComponent<Text> ();
        bolasUI = GameObject.Find ("bolasUI").GetComponent<Text> ();

    }

    public void UpdateUI()
    {
        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.bolasNum.ToString();
    }
  
}
