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
   private Button pauseBtn,pauseBTN_Return;
   [SerializeField]
   private Button btnNovamente,btnLevel;
  
   public int moedasNumAntes,moedasNumDepois,Resultado;

   

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
        pauseBTN_Return = GameObject.Find ("Pause").GetComponent<Button>();
        btnNovamente = GameObject.Find ("Novamentelose").GetComponent<Button>();
        btnLevel = GameObject.Find ("MenuFases").GetComponent<Button>();
       

        pauseBtn.onClick.AddListener (Pause);
        pauseBTN_Return.onClick.AddListener (PauseReturn);

        //you Lose

        btnNovamente.onClick.AddListener (JogarNovamente);

        moedasNumAntes = PlayerPrefs.GetInt ("moedasSave");


    }

    public void StartUI()
    {
         LigaDesligaPainel ();
    }

    public void UpdateUI()
    {
        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.bolasNum.ToString();
        moedasNumDepois = ScoreManager.instance.moedas;
        
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

    void PauseReturn()
    {
        
        pausePainel.GetComponent<Animator> ().Play ("MoveUI_PauseR");
        Time.timeScale = 1;
        StartCoroutine (EsperaPause());
    }

    IEnumerator EsperaPause()
    {
        yield return new WaitForSeconds (0.8f);
        pausePainel.SetActive (false);

    }




    IEnumerator tempo()
    {
        yield return new WaitForSeconds (0.001f);
        losePainel.SetActive (false);
        winPainel.SetActive (false);
        pausePainel.SetActive (false);
    }

    void JogarNovamente()
    {
        SceneManager.LoadScene (GameManager.instance.ondeEstou);
        Resultado = moedasNumDepois - moedasNumAntes;
        ScoreManager.instance.PerdeMoedas (Resultado);
        Resultado = 0;
    }
  
}
