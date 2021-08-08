using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento_personaje : MonoBehaviour
{
    struct pair{
        public string accion;
        public int atributo;
    }


    string accionActual;
    float atributo_Accion;
    public float unidadesPorPaso = 20 ;
    Queue<pair> AccionesEnEspera;

    public Animator animator;
    /*
    0 -> arriba
    1 -> izquierda
    2 -> abajo
    3 -> derecha
    */
    float direccion;


  //  actualizar_entrada cuadro_salida;

    //public float stepSize = 100; 
    // Start is called before the first frame update
    void Start()
    {
        AccionesEnEspera = new Queue<pair>();
        accionActual="none";
        direccion=0;
        animator= gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        Debug.Log(accionActual);
        if(accionActual=="none"){
            //idle animation
            animator.SetFloat("speed",0);
            float temp =ToSingle(direccion*0.1);
            animator.SetFloat("direccion",temp);
        }
        else if(accionActual=="avanzar"){
            if(atributo_Accion>0){
                if(direccion==0){ //arriba
                    gameObject.transform.Translate(0,unidadesPorPaso * Time.deltaTime,0);
                    animator.SetFloat("speed",1);
                    animator.SetFloat("horizontal",0);
                    animator.SetFloat("vertical",1);
                }
                else if(direccion==1){ //izquierda
                    gameObject.transform.Translate(-(unidadesPorPaso * Time.deltaTime),0,0);
                   animator.SetFloat("speed",1);
                    animator.SetFloat("horizontal",-1);
                    animator.SetFloat("vertical",0);
                }
                else if(direccion==2){ //abajo
                    gameObject.transform.Translate(0,-(unidadesPorPaso * Time.deltaTime),0);
                    animator.SetFloat("speed",1);
                    animator.SetFloat("horizontal",0);
                    animator.SetFloat("vertical",-1);
                }
                else if(direccion==3){ //derecha
                    gameObject.transform.Translate(unidadesPorPaso * Time.deltaTime,0,0);
                    animator.SetFloat("speed",1);
                    animator.SetFloat("horizontal",1);
                    animator.SetFloat("vertical",0);
                }
                atributo_Accion-=(unidadesPorPaso * Time.deltaTime);
                
            }
            else{
                accionActual="none";
                atributo_Accion=0;
                animator.SetFloat("speed",0);
            }
        }
        else if(accionActual=="girar"){
            direccion += atributo_Accion;
            if(direccion>3)
                direccion=0;
            else if(direccion<0)
                direccion=3;
            accionActual="none";
            atributo_Accion=0;
        }

        administrarListaAcciones();
    }

    void administrarListaAcciones(){
        if(accionActual=="none" && AccionesEnEspera.Count>0){
            pair temp = AccionesEnEspera.Dequeue();
            accionActual=temp.accion;
            atributo_Accion=temp.atributo;
        }
    }

    public void add_accion_a_la_cola(string metodo,int valor){
        Debug.Log("INSTRUCCION>"+metodo+" = "+valor);
        pair temp;
        temp.accion = metodo;
        temp.atributo =  valor;
        Debug.Log("Accion anhadia a la cola");
        AccionesEnEspera.Enqueue(temp);
    }

    public void reset(){
        AccionesEnEspera = new Queue<pair>();
    }

    private void onTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Pum");
    }

    public static float ToSingle(double value)
    {
     return (float)value;
    }

}
