using System;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace Progetto
{
    //####################################################################################
    //####################################################################################
    // CLASSE DI RICEZIONE
    class Receiver
    {
        private Finestra finestra;
        public Receiver(Finestra finestra)
        {
            this.finestra = finestra;
        }
        // RICEZIONE
        public void Ricevi(object obj)
        {
            Socket socket = (Socket)obj;
            using (Stream stream = new NetworkStream(socket))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                //CONNESSIONE
                byte[] id = reader.ReadBytes(10); //bloccante
                byte[] freq = reader.ReadBytes(4);
                string s = System.Text.ASCIIEncoding.ASCII.GetString(id);
                int f = BitConverter.ToInt32(freq, 0);
                lock (finestra)
                {
                    Finestra.id = s;
                    Finestra.freq = f;
                    Monitor.Pulse(finestra);
                }

                // SETTO LO STATO SUL FORM 
                Program.form1.setStatus(true, 1);
                
                //RICEZIONE DATI
                // BitConverter.ToSingle per convertire secondo IEEE 754
                while (stream.ReadByte() != -1)
                {
                    if ( finestra.start == 0 )
                        Program.form1.setStatus(true, 2);
                    byte[] pacchetto = new byte[0];
                    reader.ReadBytes(6); // INTESTAZIONE
                    for (int i = 0; i < 5; i++)
                    {
                        pacchetto = combineArray(pacchetto, reader.ReadBytes(9 * 4)); //DATI:
                        // 3 ACC, 3 GIRO, 3 MAGNE
                        reader.ReadBytes(4 * 4); // 4 QUATERNIONI
                    }
                    reader.ReadBytes(1); // CHECKSUM

                    float[] campione = new float[pacchetto.Length / 4];
                    int j = 0;
                    for (int i = 0; i < pacchetto.Length; i += 4)
                    {

                        byte[] nuovo = new byte[4];
                        Array.Copy(pacchetto, i, nuovo, 0, 4); // COPIA 4 BYTE DA PACCHETTO
                        Array.Reverse(nuovo); // BIG ENDIAN quindi faccio la reverse
                        campione[j] = BitConverter.ToSingle(nuovo, 0);
                        j++;
                    }
                    
                    //ritorno l'array di float (il mio campione) -> li mando alla classe analizza
                    lock (finestra)
                    {
                        finestra.scrivi(campione);
                        if (finestra.finestra.Count == 500)
                        {
                            // se è la prima volta che passo qua la metto a true
                            if (finestra.start == 0)
                            {
                                finestra.start = 1;
                            }
                            // ho scritto 500 campioni quindi sveglio l'analizzatore
                            Monitor.Pulse(finestra);
                        }
                    }
                }
                // HO LETTO TUTTO
                lock (finestra)
                {
                    finestra.fine = true;
                    // se è la prima volta che passo qua la metto a true
                    // 0 nessun invio
                    // 1 invio primo campione alla finestra, csv non ancora creato
                    // 2 ricevuto primo campione + creato csv
                    if (finestra.start == 0 && finestra.finestra.Count != 0)
                    {
                        finestra.start = 1;
                    }

                    Monitor.Pulse(finestra);
                }
            }
        }

        // FUNZIONE PER COMBINARE ARRAY
        private byte[] combineArray(byte[] a1, byte[] a2)
        {
            byte[] ret = new byte[a1.Length + a2.Length];
            Array.Copy(a1, ret, a1.Length);
            Array.Copy(a2, 0, ret, a1.Length, a2.Length);
            return ret;
        }
    }
}
