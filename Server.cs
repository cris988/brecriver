using System;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Windows.Forms;

namespace Progetto
{
    //####################################################################################
    //####################################################################################
    // CLASSE DI GESTIONE
    class Server
    {
        private readonly int port;
        TcpListener listener;
        Socket socket;
        Thread riceviThread, analizzaThread;
        Analyzer analizza;
        ManualResetEvent mre; // gestione eventi per mettere in pausa o riprende l'esecuzione dell'analyzer
        public Server(int port)
        {
            this.port = port;
            this.mre = new ManualResetEvent(true);
        }

        public void Start()
        {
            try
            {
                listener = new TcpListener(new IPEndPoint(IPAddress.Any, port));
                while (true)
                {
                    listener.Start(0);
                    //accetto un nuovo socket;
                    socket = listener.AcceptSocket();

                    Finestra finestra = new Finestra();

                    //creo un thread per ricevere i dati
                    Receiver ricevi = new Receiver(finestra);
                    riceviThread = new Thread(new ParameterizedThreadStart(ricevi.Ricevi));
                    riceviThread.Start(socket);
                    //creo un thread per l'analisi dei dati
                    analizza = new Analyzer(finestra, mre);
                    analizzaThread = new Thread(new ThreadStart(analizza.inizia));
                    analizzaThread.Start();
                    
                    // attende che riceve finisca
                    riceviThread.Join();
                    listener.Stop();
                    // attende che analizza finisca
                    analizzaThread.Join();

                    // Imposto stato sul form
                    Program.form1.setStatus(false, 0);
                    
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("Il server è già avviato", "ERRORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Stop()
        {
            if (socket != null)
            {
                riceviThread.Abort();
                analizza.Abort();
                analizzaThread.Abort();
                analizzaThread.Join();
                
                Console.WriteLine("sono morto");
                socket.Close();
            }
            listener.Stop();
        }

        public void Pause()
        {
            mre.Reset();
        }

        public void Resume()
        {
            mre.Set();
        }
    }
}
