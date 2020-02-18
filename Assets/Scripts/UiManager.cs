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
   private GameObject losePainel,winPainel,pausePainel;
   [SerializeField]
   private Button pauseBtn;
   

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
        losePainel = GameObject.Find ("LosePainel");
        winPainel = GameObject.Find ("WinPainel");
        pausePainel = GameObject.Find ("PausePainel");
        pauseBtn = GameObject.Find ("pause").GetComponent<Button>();
        LigaDesligaPainel ();

        pauseBtn.onClick.AddListener(Pause);

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

    public void WinGameUI()
    {
        winPainel.SetActive (true);
    }

    void LigaDesligaPainel()
    {
        StartCoroutine (tempo());
    }

    void Pause()
    {
        pausePainel.SetActive (true);
        pausePainel.GetComponent<Animator> ().Play("MoveUI_Pause");
        Time.timeScale = 0;
    }

    IEnumerator tempo()
    {
        yield return new WaitForSeconds (0.001f);
        losePainel.SetActive (false);
        winPainel.SetActive (false);
        pausePainel.SetActive (false);
    }


  
}
