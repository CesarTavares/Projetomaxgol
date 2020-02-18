using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
   public static UIManager instance;
   private Text pontosUI,bolasUI;
   [SerializeField]
   private GameObject losePainel;


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
       LigaDesligaPainel ();

    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        pontosUI = GameObject.Find ("PontosUI").GetComponent<Text> ();
        bolasUI = GameObject.Find ("bolasUI").GetComponent<Text> ();
        losePainel = GameObject.Find ("LosePainel");
    }

    public void UpdateUI()
    {
        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.bolasNum.ToString();
        
    }

    public void GameOverUI()
    {
        losePainel.SetActive (true);
    }

    void LigaDesligaPainel()
    {
        StartCoroutine (tempo());
    }

    IEnumerator tempo()
    {
        yield return new WaitForSeconds (0.001f);
        losePainel.SetActive (false);
    }
  
}
