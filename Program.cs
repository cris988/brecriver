using System;
using System.Windows.Forms;
using System.Threading;

namespace Progetto
{
    static class Program
    {
        
        public static Form1 form1;

        // Port di ascolto di default
        public static int port = 45555;

        private static Thread serverThread;
        private static Server server;
        /// <summary>
        /// The main entry point for the application.
        /// 
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Creo un nuovo server e lancio un thread
            server = new Server(port);
            start();
            
            // inizializzazione form
            form1 = new Form1();

            // setto lo stato del form
            form1.setStatus(false, 0);
            
            // avvio form
            Application.Run(form1);

        }

        public static void start()
        {
            if (serverThread == null || !serverThread.IsAlive) // per evitare la creazione di server multipli
            {
                serverThread = new Thread(new ThreadStart(server.Start));
                serverThread.Start();
            }
        }

        public static void close()
        {
            serverThread.Abort();
            server.Stop();
        }

        public static void pause()
        {
            server.Pause();
        }

        public static void resume()
        {
            server.Resume();
        }
    }
}
