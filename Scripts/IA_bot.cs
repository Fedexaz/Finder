using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_bot : MonoBehaviour
{
    /*
    
        ESTADOS IA

        1-quieto
        2-caminando
        3-atacando
        4-muerto
     
    */

    public const int quieto = 1;
    public const int caminando = 2;
    public const int atacando = 3;
    public const int muerto = 4;

    public float velocidad = 5f;

    public int estado;
    public bool killed = false;
    public bool punch = false;

    public GameObject rangoVision;
    public detectCollisionIA colisionPlayer;

    public NavMeshAgent iaNav;

    public Animator enemyAnimator;


    private void Start()
    {
        estado = quieto;
        iaNav.speed = velocidad;
    }

    private void Update()
    {
        EstadosIA();
    }

    void EstadosIA()
    {
        switch (estado)
        {
            case quieto:
                {
                    if(colisionPlayer.playerInRange)
                    {
                        estado = caminando;
                        iaNav.destination = colisionPlayer.posTarget;
                        enemyAnimator.SetBool("caminar", true);
                    }
                    break;
                }
            case caminando:
                {
                    if (!colisionPlayer.playerInRange)
                    {
                        if (iaNav.velocity.magnitude < 0.45f)
                        {
                            estado = quieto;
                            enemyAnimator.SetBool("caminar", false);
                        }
                    }
                    else
                    {
                        estado = caminando;
                        iaNav.destination = colisionPlayer.posTarget;
                        enemyAnimator.SetBool("caminar", true);
                    }
                    break;
                }
            case atacando:
                {
                    if (!punch)
                    {
                        punch = true;
                        enemyAnimator.SetTrigger("ataque");
                    }
                    break;
                }
            case muerto:
                {
                    if (!killed)
                    {
                        enemyAnimator.SetTrigger("morir");
                        killed = true;
                    }
                    break;
                }
        }
    }

    public void StopPunch()
    {
        punch = false;
        estado = quieto;
    }
}
