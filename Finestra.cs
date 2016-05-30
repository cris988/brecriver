using System;
using System.Collections;
using System.Collections.Generic;

namespace Progetto
{
    //####################################################################################
    //####################################################################################
    // CLASSE VASSOIO
    class Finestra
    {
        public static int freq; // statica così la si può leggere dal form senza usare variabili interne
        public static string id; // statica così la si può leggere dal form senza usare variabili interne
        public List<float[,]> finestra;
        
        public bool fine = false;
        // START
        // 0 nessun invio
        // 1 invio primo campione alla finestra csv non ancora creato
        // 2 ricevuto primo campione + creato csv
        public int start = 0;
        public Finestra()
        {
            finestra = new List<float[,]>();
        }

        // AGGIUNTA DATI SULL'ARRAYLIST
        public void scrivi(float[] campione)
        {
            float[,] matrice = new float[5, 9];
            int offset = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    matrice[i, j] = campione[j + offset];
                }
                offset += 9;
            }
            finestra.Add(matrice);
        }

        // LETTURA DALL'ARRAYLIST
        public float[,] leggi(int index)
        {
            float[,] letto = (float[,])finestra[index];
            return letto;
        }

        // SWAP SULL'ARRAYLIST
        public void swap(int num)
        {
            finestra.RemoveRange(0, num);
        }
    }
}
