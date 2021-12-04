using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using nameSpacePlayer;
using UnityEditor;

public class playerController : MonoBehaviour
{
    player Player;//Creamos la variable jugador para instanciarlo luego.
    public string stat = "Vivo";//Estado al instanciar clase
    public int vid = 5;//Vidas al instanciar clase
    public int mon = 0;//Monedas al instanciar clase

    [Header("Variables Jugador")]
    public int coins = 0;
    public int vidas = 5;
    public int cantCoinsToWin;

    [Header("Variables movimiento Jugador")]
    public float playerSpeed = 3f;
    public float velNormal = 3f;
    public float velCorriendo = 10f;
    public bool canMove = true;

    [Header("Componentes del jugador")]
    public CharacterController jugador;
    public Animator animaciones;

    [Header("HUD del jugador")]
    public GameObject botonCorrer;
    public GameObject botonNoCorrer;    

    Vector3 pos = Vector3.zero;
    Vector3 anim = Vector3.zero;

    void Start()
    {
        Player = new player(mon, vid, stat);//Instanciar clase Player

        jugador = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();//FUNCION PARA CONTROLAR PERSONAJE
        Estadisticas();//FUNCION PARA CONTROLAR ESTADISTICAS DEL PERSONAJE
    }

    void Estadisticas()//ASIGNA ESTADISTICAS EN TIEMPO REAL (MONEDAS O VIDAS)
    {
        Player.setMonedas(coins);
        Player.setVidas(vidas);

        if(coins == cantCoinsToWin)
        {
            Player.setEstado("Win");
        }
    }

    void Movement()
    { 
        //ESTADOS DEL PERSONAJE
        switch (Player.getEstado())
        {
            case "Vivo":
            {
                if(Player.getVidas() <= 0)
                {
                    Player.setEstado("Muerto");
                }
                break;
            }
            
            case "Muerto":
            {
                animaciones.SetTrigger("muerte");
                break;
            }

            case "Win":
            {
                if (canMove == true)
                {
                    animaciones.SetTrigger("festejo");
                    canMove = false;
                }
                break;
            }
        }
        
        //ASIGNAR VELOCIDADES EN BASE A SI CORRE O NO
        if(animaciones.GetBool("corriendo"))
        {
            playerSpeed = velCorriendo;
        }
        else
        {
            playerSpeed = velNormal;
        }

        //MOVIMIENTO PERSONAJE

        if (Player.getEstado() == "Vivo")
        {
            float ejex = Gamepad.current.leftStick.x.ReadValue();
            float ejez = Gamepad.current.leftStick.y.ReadValue();

            pos = new Vector3(ejex * playerSpeed * Time.deltaTime, 0, ejez * playerSpeed * Time.deltaTime);

            anim = new Vector3(ejex * playerSpeed * Time.deltaTime, 0, ejez * playerSpeed * Time.deltaTime).normalized;

            animaciones.SetFloat("speed", anim.sqrMagnitude);


            if (Player.getEstado() == "Vivo")
            {
                if (pos != Vector3.zero)
                {
                    gameObject.transform.forward = pos;
                }
                else
                {
                    botonCorrer.SetActive(true);
                    botonNoCorrer.SetActive(false);
                    animaciones.SetBool("corriendo", false);
                }
                jugador.Move(pos);
            }
            else if (Player.getEstado() == "Muerto" || Player.getEstado() == "Win")
            {
                pos = Vector3.zero;
                jugador.Move(pos);
            }
        }

        
    }

    public void SetCorrer(bool value) 
    {
        animaciones.SetBool("corriendo", value);
    }


    public void MatarJugador()
    {
        Player.setVidas(0);
        Player.setEstado("Muerto");
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("player_Vidas", Player.getVidas());
        PlayerPrefs.SetInt("player_Monedas", Player.getMonedas());
    }
}
