using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BolaControll : MonoBehaviour
{
        
   //Seta
   public GameObject setaGO;

   //Ang
   public float zRotate;
   public bool liberaRot = false;
   public bool liberaTiro = false;

   //Força

    private Rigidbody2D bola;
    private float force = 0f;
    public GameObject seta2Img;

    //Paredes
    private Transform paredeLD,paredeLE;

   void Awake()
   {
       
        setaGO = GameObject.Find ("Seta");
        seta2Img = setaGO.transform.GetChild(0).gameObject;
        setaGO.GetComponent<Image> ().enabled = false;
        seta2Img.GetComponent<Image> ().enabled = false;
        paredeLD = GameObject.Find ("ParedeLD").GetComponent<Transform> ();
        paredeLE = GameObject.Find ("ParedeLE").GetComponent<Transform> ();

   }


    // Start is called before the first frame update
    void Start()
    {
      
    
       //Força
       
        bola = GetComponent<Rigidbody2D> ();
      
    }

    // Update is called once per frame
    void Update()
    {
       RotacaoSeta ();
       InputDeRotacao ();
       LimitaRotacao ();
       PosicionaSeta (); 

       //Força 

        ControlaForca ();
        AplicaForca ();
        //Paredes
        Paredes ();


    }

     void PosicionaSeta()
    {
        setaGO.GetComponent<Image>().rectTransform.position = transform.position;
    }
    
    void RotacaoSeta()
    {
        setaGO.GetComponent<Image>().rectTransform.eulerAngles = new Vector3 (0,0,zRotate);
    }

    void InputDeRotacao()
    {
        /*if(Input.GetKey(KeyCode.UpArrow))
        {
            zRotate += 2.5f;
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            zRotate -= 2.5f;
        }*/

    

        if(liberaRot == true)
        {
            
            float moveY = Input.GetAxis ("Mouse Y");

            if(zRotate < 90)
            {
               if(moveY > 0)
               {
                   zRotate += 2.5f;
               }
            }

               if(zRotate > 0)
            {
               if(moveY < 0)
               {
                   zRotate -= 2.5f;
               }  
            }

          
        }
    }
    void LimitaRotacao()
    {
        if(zRotate >= 90)
        {
            zRotate = 90;
        }

         if(zRotate <= 0)
        {
            zRotate = 0;
        }

    }

    void OnMouseDown()
    {
        if(GameManager.instance.tiro == 0)
        {
        liberaRot = true;
        setaGO.GetComponent<Image> ().enabled = true;
        seta2Img.GetComponent<Image> ().enabled = true;
        }
    }

    void OnMouseUp()
    {
        liberaRot = false;
        setaGO.GetComponent<Image> ().enabled = false;
        seta2Img.GetComponent<Image> ().enabled = false;
        
      

        if(GameManager.instance.tiro == 0 && force > 0)
        {
        liberaTiro = true;
        seta2Img.GetComponent<Image> ().fillAmount = 0;       
        AudioManager.instance.SonsFXToca (1);
        GameManager.instance.tiro = 1;

        }
       
    }

    //Força

     void AplicaForca()
    {
        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
         float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);

        if(liberaTiro == true)
        {
            bola.AddForce(new Vector2(x,y));
            liberaTiro = false;  
        }
    }

      void ControlaForca()
    {
        if(liberaRot == true)
        {
            float moveX = Input.GetAxis ("Mouse X");

            if(moveX < 0)
            {
                seta2Img.GetComponent<Image>().fillAmount += 0.8f * Time.deltaTime;
                force =  seta2Img.GetComponent<Image>().fillAmount * 1000f;
            }

            if(moveX > 0)
            {
                seta2Img.GetComponent<Image>().fillAmount -= 0.8f * Time.deltaTime;
                force =  seta2Img.GetComponent<Image>().fillAmount * 1000f;
            }


        }
    }

    void BolaDinamica()
    {
        this.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
    }

    void Paredes()
    {
        if(this.gameObject.transform.position.x > paredeLD.position.x)
        {
            Destroy (this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }

        if(this.gameObject.transform.position.x < paredeLE.position.x)
        {
            Destroy (this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
             GameManager.instance.bolasNum -= 1;
        }
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if(outro.gameObject.CompareTag("morte"))
        {
            Destroy (this.gameObject);
            GameManager.instance.bolasEmCena -= 1;   
        }

        if(outro.gameObject.CompareTag("win"))
        {
            GameManager.instance.win = true;
        }
    }
}
