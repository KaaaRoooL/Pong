using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    //Velocidad
    [SerializeField] private float velocidad = 30.0f;

    //Se ejecuta al arrancar
    void Start () {

    //Velocidad inicial hacia la derecha
    GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
 
    }

    // Update is called once per frame
    void Update()
    {
       
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

            //Valor de x
            int x = -1;

            //Valor de y
            int y = direccionY(transform.position, micolision.transform.position);

            //Vector de dirección
            Vector2 direccion = new Vector2(x, y);

            //Aplico velocidad
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

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
}
