using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManagerLevels : MonoBehaviour
{
        [SerializeField]
        private Text moedasLevel;

    // Start is called before the first frame update
    void Start()
    {
        moedasLevel.text = PlayerPrefs.GetInt ("moedasSave").ToString ();     
    }

   
}
