using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
        SISTEMA DE ESTADOS:

        -Parado
        -Caminando
        -Corriendo
        -Muerto

 */

namespace nameSpacePlayer
{

    public class player : MonoBehaviour
    {
        private int monedas = 0;
        private int vidas = 0;
        private string estado = "Parado";

        public player()
        {
            this.monedas = 0;
            this.vidas = 0;
            this.estado = "Parado";
        }

        public player(int monedas, int vidas, string estado)
        {
            this.monedas = monedas;
            this.vidas = vidas;
            this.estado = estado;
        }

        /// <summary>
        /// GETTERS AND SETTERS DE LA CLASE JUGADOR
        /// </summary>
        public int getMonedas()
        {
            return this.monedas;
        }

        public void setMonedas(int monedas)
        {
            this.monedas = monedas;
        }

        public int getVidas()
        {
            return this.vidas;
        }

        public void setVidas(int vidas)
        {
            this.vidas = vidas;
        }

        public string getEstado()
        {
            return this.estado;
        }

        public void setEstado(string estado)
        {
            this.estado = estado;
        }

        override
        public string ToString()
        {
            return "OBJETO: Vidas: "+ getVidas() +", Monedas: "+ getMonedas() + ", Estado: \" "+ getEstado() +" \"";
        }
    }

}