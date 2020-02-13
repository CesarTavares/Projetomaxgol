using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Rotacao : MonoBehaviour
{
   //Posição Seta
   [SerializeField]private Transform posStart;
   //Seta
   [SerializeField]private Image setaImg;
   public GameObject setaGO;
   //Ang
   public float zRotate;
   public bool liberaRot = false;
   public bool liberaTiro = false;


    void Start()
    {
      
      PosicionaBola ();  
    }

    // Update is called once per frame
    void Update()
    {
       RotacaoSeta ();
       InputDeRotacao ();
       LimitaRotacao ();
       PosicionaSeta ();   
    }

    void FixedUpdate()
    {

    }

    void PosicionaSeta()
    {
        setaImg.rectTransform.position = transform.position;
    }
    void PosicionaBola()
    {
       this.gameObject.transform.position = posStart.position;
    }

    void RotacaoSeta()
    {
        setaImg.rectTransform.eulerAngles = new Vector3 (0,0,zRotate);
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
        liberaRot = true;
        setaGO.SetActive (true);
    }

    void OnMouseUp()
    {
        liberaRot = false;
        liberaTiro = true;
        setaGO.SetActive (false);
    }

}

