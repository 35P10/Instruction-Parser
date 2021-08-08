using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actualizar_entrada : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    public void addEntrada(string s)
    {
        string input = s;
        int i;
        Text txt=gameObject.GetComponent<Text>(); 

        for(i=0 ; i < input.Length ; i++){
            txt.text += input[i];
            if(input[i]==';')
                txt.text += "\n";
        }
    }

    public void clear(){
        Text txt=gameObject.GetComponent<Text>(); 
        txt.text = "";
        
    }

    public void printMensaje(string s)
    {
        string input = s;

        Text txt=gameObject.GetComponent<Text>(); 
    
        txt.text += s;
        txt.text += "\n";
        
    }
}
