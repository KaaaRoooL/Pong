using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Añade esta librería apra poder utilizar los elemento de User Interface */
using UnityEngine.UI;

public class Bola : MonoBehaviour
{
    //Velocidad
    [SerializeField] private float velocidad = 30.0f;

    //Contadores de goles
    [SerializeField] private int golesIzquierda = 0;
    [SerializeField] private int golesDerecha = 0;

    //Cajas de texto de los contadores
    [SerializeField] private Text contadorIzquierda;
    [SerializeField] private Text contadorDerecha;

    //Audio Source
    AudioSource fuenteDeAudio;

    //Clips de audio
    [SerializeField] private AudioClip audioGol, audioRaqueta, audioRebote;

    //Resultado
    [SerializeField] private Text resultado;

    //Centro y bola para ocultarlos cuando acaba el juego
    [SerializeField] private GameObject centro;
    [SerializeField] private GameObject bola;

    //Caja de texto para el temporizador
    [SerializeField] private Text temporizador;  

    //variable para contabilizar el tiempo inicializada a 180 segundos (3 minutos)
    private float tiempo = 180;  
    

    //Se ejecuta al arrancar
    void Start () {

        //Velocidad inicial hacia la derecha
        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;

        //Pongo los contadores a 0
        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();

        //Recupero el componente audio source;
        fuenteDeAudio = GetComponent<AudioSource>();

        //Desactivo la caja de resultado
        resultado.enabled = false;

        //Quito la pausa
        Time.timeScale = 1;

 
    }

    // Update is called once per frame
    void Update()
    {

        //Incremento la velocidad de la bola
        velocidad = velocidad + 2 * Time.deltaTime;

        //Si aún no se ha acabado el tiempo, decremento su valor y lo muestro en la caja de texto
        if (tiempo >= 0){
        tiempo -= Time.deltaTime; //Le resto el tiempo transcurrido en cada frame
        temporizador.text = formatearTiempo(tiempo); //Formateo el tiempo y lo escribo en la caja de texto
        }
        //Si se ha acabado el tiempo, compruebo quién ha ganado y se acaba el juego
        else{
        temporizador.text = "00:00"; //Para evitar valores negativos	
        //Compruebo quién ha ganado
        if (golesIzquierda > golesDerecha){
            centro.SetActive(false);
            bola.SetActive(false);
            //Escribo y muestro el resultado
            
            resultado.text = "¡Jugador Izquierda GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
        }
        else if (golesDerecha > golesIzquierda){
            centro.SetActive(false);
            bola.SetActive(false);
            //Escribo y muestro el resultado
            resultado.text = "¡Jugador Derecha GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
        }
        else{
            //Escribo y muestro el resultado
            centro.SetActive(false);
            bola.SetActive(false);
            resultado.text = "¡EMPATE!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
        }
        //Muestro el resultado, pauso el juego y devuelvo true
        resultado.enabled = true;
        Time.timeScale = 0; //Pausa
        }

    }

    //Se ejecuta al colisionar
    void OnCollisionEnter2D(Collision2D micolision){

        //transform.position es la posición de la bola
        //micolision contiene toda la información de la colisión
        //Si la bola colisiona con la raqueta:
        //micolision.gameObject es la raqueta
        //micolision.transform.position es la posición de la raqueta

        //Si choca con la raqueta izquierda
        if (micolision.gameObject.name == "RaquetaIzquierda"){
            //Reproduzco el sonido de la raqueta
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();

            //Valor de x
            int x = 1;

            //Valor de y
            int y = direccionY(transform.position, micolision.transform.position);

            //Vector de dirección
            Vector2 direccion = new Vector2(x, y);

            //Aplico velocidad
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
        }

        //Si choca con la raqueta derecha
        if (micolision.gameObject.name == "RaquetaDerecha"){
            //Reproduzco el sonido de la raqueta
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();

            //Valor de x
            int x = -1;

            //Valor de y
            int y = direccionY(transform.position, micolision.transform.position);

            //Vector de dirección
            Vector2 direccion = new Vector2(x, y);

            //Aplico velocidad
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
        }

        //Para el sonido del rebote
        if (micolision.gameObject.name == "Arriba" || micolision.gameObject.name == "Abajo"){

            //Reproduzco el sonido del rebote
            fuenteDeAudio.clip = audioRebote;
            fuenteDeAudio.Play();

}


    }

    //Método para calcular la direccion de Y (deevuelve un número entero int)
    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta){

        if (posicionBola.y > posicionRaqueta.y){
            return 1; //Si choca por la parte superior de la raqueta, sale hacia arriba
        }
        else if (posicionBola.y < posicionRaqueta.y){
            return -1; //Si choca por la parte inferior de la raqueta, sale hacia abajo
        }
        else{
            return 0; //Si choca por la parte central de la raqueta, sale en horizontal
        }
    }



    //Reinicio la posición de la bola
    public void reiniciarBola(string direccion){

        //Reproduzco el sonido del gol
        fuenteDeAudio.clip = audioGol;
        fuenteDeAudio.Play();

        //Posición 0 de la bola
        transform.position = Vector2.zero;
        //Vector2.zero es lo mismo que new Vector2(0,0);

        //Velocidad inicial de la bola
        velocidad = 30;

        //Velocidad y dirección
        if (direccion == "Derecha"){
            //Incremento goles al de la derecha
            golesDerecha++;
            //Lo escribo en el marcador
            contadorDerecha.text = golesDerecha.ToString();
            //Reinicio la bola
            if (!comprobarFinal()){               
                GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
                //Vector2.right es lo mismo que new Vector2(1,0)
            }
        }
        else if (direccion == "Izquierda"){
            //Incremento goles al de la izquierda
            golesIzquierda++;
            //Lo escribo en el marcador
            contadorIzquierda.text = golesIzquierda.ToString();
            //Reinicio la bola
            if (!comprobarFinal()){             
                GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
                //Vector2.left es lo mismo que new Vector2(-1,0)
            }
        }
    }


    

    //Compruebo si alguno ha llegado a 5 goles
    bool comprobarFinal(){

        //Si el de la izquierda ha llegado a 5
        if (golesIzquierda == 5){            
            centro.SetActive(false); 
            bola.SetActive(false);
            //Escribo y muestro el resultado
            resultado.text = "¡Jugador Izquierda GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            //Muestro el resultado, pauso el juego y devuelvo true
            resultado.enabled = true;
            Time.timeScale = 0; //Pausa
            return true;
        }
        //Si el de le aderecha a llegado a 5
        else if (golesDerecha == 5){
            centro.SetActive(false);
            bola.SetActive(false);
            //Escribo y muestro el resultado
            resultado.text = "¡Jugador Derecha GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            //Muestro el resultado, pauso el juego y devuelvo true
            resultado.enabled = true;
            Time.timeScale = 0; //Pausa
            return true;
        }
        //Si ninguno ha llegado a 5, continúa el juego
        else{
            return false;
        }
    }


    string formatearTiempo(float tiempo){

    //Formateo minutos y segundos a dos dígitos
        string minutos = Mathf.Floor(tiempo / 60).ToString("00");
        string segundos = Mathf.Floor(tiempo % 60).ToString("00");
        
        //Devuelvo el string formateado con : como separador
        return minutos + ":" + segundos;
    
    }

}
