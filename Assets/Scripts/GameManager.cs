using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

  void Update () {

    //Si pulsa la tecla P o hace clic izquierdo empieza el juego
    if (Input.GetKeyDown(KeyCode.P) || Input.GetMouseButton(0)){
      //Cargo la escena de Juego
      SceneManager.LoadScene("Juego");
    }
  }
}
