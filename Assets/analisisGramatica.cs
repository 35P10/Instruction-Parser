using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class analisisGramatica : MonoBehaviour
{

    movimiento_personaje player;
    actualizar_entrada cuadro_salida;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").GetComponent<movimiento_personaje>();
        cuadro_salida = GameObject.FindWithTag("salida_texto").GetComponent<actualizar_entrada>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
S -> INICIO instrucciones FIN
INICIO -> iniciar();
instrucciones -> instruccion instrucciones
instrucciones -> vacio
INICIO -> terminar();
instruccion -> girar
instruccion -> avanzar
*/

    private string _input="";
    private string input;
    private int tok=0;
    Queue<string> Entrada = new Queue<string>();

    public void ReadStringInput(string s){
        input = s;
        for(int i=0 ; i < input.Length ; i++){
            _input += input[i];
            if(input[i]==';'){
                Debug.Log(_input);
                Entrada.Enqueue(_input);
                _input ="";
            }
        }
    }

    private string preanalisis;
    void scan(){
        Debug.Log("SCAN");
        if(Entrada.Count>0){
            preanalisis = Entrada.Dequeue();
            if(preanalisis=="iniciar();")
                tok=1;
            else if (preanalisis=="terminar();") 
                tok=2;
            else
                tok=3;
        }else{
            Debug.Log("error");
            tok=-1;
        }
    }
    
    public void S(){
        Debug.Log("inicio de analisis de gramatica");
        cuadro_salida.clear();
        cuadro_salida.printMensaje("ANALIZANDO GRAMATICA");
        scan();
        iniciar();
        //instrucciones();
        //fin();
    }

    void iniciar(){
        if(tok ==  1){
            cuadro_salida.printMensaje("INICIADO");
            Debug.Log("Iniciado");
            scan();
            instrucciones();
        }
        else
            cuadro_salida.printMensaje("ERROR EN LA GRAMATICA");
    }

    void instrucciones(){
        if(tok == 3){
            if(instruccion() == 1){
            scan();
            instrucciones();
            }
            else
            ; //error: instruccion incorrecta
        }
        else
            fin();
    }

    int instruccion(){
        int i;
        string metodo="";
        string valor="";
        for(i=0 ; i < preanalisis.Length ; i++){
            if(preanalisis[i]=='('){
                i++;
                break;
            }
            metodo += preanalisis[i];
        }
        for(i=i ; i < preanalisis.Length ; i++){
            if(preanalisis[i]==')' && preanalisis[i+1]==';'){
                break;
            }
            valor += preanalisis[i];
        }
        Debug.Log(metodo);
        Debug.Log(valor);
    
        return AnalizadorDeInstruccion(metodo,valor);
    }

    int AnalizadorDeInstruccion(string metodo,string valor){
        if(metodo == "avanzar"){
            Debug.Log("metodo");
            float size_of_block = 48;
            int x = 0;
            if (int.TryParse(valor, out x)){
                player.add_accion_a_la_cola(metodo,x*48);
                cuadro_salida.printMensaje("INSTRUCCION>"+metodo+" = "+x);
                return 1;
            }
            else{
                //accionActual="error"
                Debug.Log("Atributo no admitido");
                cuadro_salida.printMensaje("ATRIBUTO NO RECONOCIDO PARA AVANZAR");
                return 0;
            }
        }
        else if(metodo=="girar"){
            if(valor=="derecha"){
                player.add_accion_a_la_cola(metodo,-1);
                cuadro_salida.printMensaje("INSTRUCCION>"+metodo+" = "+valor);
                return 1;
            }
            else if(valor=="izquierda"){
                player.add_accion_a_la_cola(metodo,1);
                cuadro_salida.printMensaje("INSTRUCCION>"+metodo+" = "+valor);
                return 1;
            }
            else{
                Debug.Log("Atributo no admitido");
                cuadro_salida.printMensaje("ATRIBUTO NO RECONOCIDO PARA GIRAR");
                return 0;
            }
                      
        }
        else{
            cuadro_salida.printMensaje("INSTRUCCION NO RECONOCIDA");
            return 0;
        }
    }

    void fin(){
        if(tok ==  2)
            cuadro_salida.printMensaje("TERMINADO");
        else
            cuadro_salida.printMensaje("ERROR EN LA GRAMATICA");
    }

    public void reset(){
        Entrada = new Queue<string>();
        _input="";
        input="";
        preanalisis="";
        tok=0;
        cuadro_salida.clear();
        cuadro_salida.printMensaje("REINICIAR");
    }

    public void borrarUltimaEntrada(){
        cuadro_salida.clear();
        cuadro_salida.printMensaje("BORRAR ULTIMA ENTRADA");
        Queue<string> nueva_Entrada = new Queue<string>();
        _input="";
        int temp = Entrada.Count;
        for(int i=0 ; i <temp-1;i++){
            string temp2 =Entrada.Dequeue();
            cuadro_salida.printMensaje(temp2);
            nueva_Entrada.Enqueue(temp2);
        }
        Entrada = nueva_Entrada;
    }
    
}
