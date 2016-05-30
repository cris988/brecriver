using System;
using System.Threading;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace Progetto
{
    //####################################################################################
    //####################################################################################
    // CLASSE DI ANALISI
    class Analyzer
    {
        // threads figli
        Thread printStaz;
        Thread printPos;
        Thread printGir;

        // oggetto vassoio
        private Finestra finestra;

        // dati client
        private string id;
        private int freq;

        // variabili di controllo
        private const int NMAX = 500;
        private int TEMPO = 0, start, N = NMAX;
        private bool stop = false, fine; // STOP mi indica quando devo terminare l'analisi
                                        // FINE mi indica quando sto analizzando l'ultimo campione
        private int soggettoCorrente = -1;

        // scritture file
        StreamWriter sw;
        XmlDocument xmlDoc;
        public static string filename;
        DateTime dataAcquisizione;

        //private float[, ,] sampwin; // finestra d'analisi CAMPIONI, SENSORI, DATI
        private List<float[,]> sampwin; // finestra d'analisi CAMPIONI, SENSORI, DATI
        private List<float[,]> sampwinPassato;

        float[] orientamentoPassato;

        private int ultimoStaz = -1; // tiene in memoria l'ultimo stazionamento della finestra corrente
        private float ultimoPos = -1; // tiene in memoria l'ultimo posizionamento della finestra corrente
                                     //è float invece che int perchè sarebbe stato bello graficarlo il posizionamento
        private float ultimaGir = 0; // tiene in memoria l'ultima valore di girata della finestra corrente
        private float ultimoOrienta = 0;
        //ATTENZIONE: LE VARIABILI GLOBALI RIMANGONO SETTATE PER TUTTA LA DURATA DELL'ANALYZER. NON GRAFICARE PIU' DI UNA
        //FUNZIONE CHE NE UTILIZZA UNA!!! SBALLA TUTTO E FA PUTTANAIO!

        ManualResetEvent mre; // gestione eventi per mettere in pausa o riprende l'esecuzione dell'analisi
        Mutex mut = new Mutex(); // gestione scrittura su file xml

        public Analyzer(Finestra finestra, ManualResetEvent mre)
        {
            this.finestra = finestra;
            //sampwin = new float[N, 5, 9];
            sampwin = new List<float[,]>(NMAX);
            sampwinPassato = new List<float[,]>();
            this.mre = mre;

            // Ripulisco il grafico
            Program.form1.clear();
        }
        // ANALISI
        public void inizia()
        {

            lock (finestra)
            {
                // rimane in attesa di leggere la frequenza
                while (Finestra.freq == 0)
                {
                    Monitor.Wait(finestra);
                }
                this.freq = Finestra.freq;
                this.id = Finestra.id;
                dataAcquisizione = DateTime.Now;
                Monitor.Pulse(finestra);
            }
            while (stop == false)
            {

                lock (finestra)
                {
                    // rimane in attesa di avere il numero di campioni necessario
                    // se ricevo l'ultimo campione proseguo
                    while (finestra.finestra.Count < NMAX && finestra.fine == false)
                    {
                        Monitor.Wait(finestra);
                    }
                    fine = finestra.fine;
                    
                    // controllo se ho ricevuto l'ultimo campione dello streaming di dati
                    // imposto N uguale al nuovo numero di campioni della finestra
                    if (fine == true)
                    {
                        if (finestra.finestra.Count <= NMAX)
                        {
                            N = finestra.finestra.Count;
                        }
                    }
                    // se inizio la trasmissione di dati comincio a scrivere e recupero il soggetto (Uomo Cavallo rilevamento automatico) dalla gui
                    if (finestra.start == 1)
                    {
                        soggettoCorrente = Program.form1.getSoggetto(); //recupero il soggetto quando inizia l'invio dei dati

                        filename = id + dataAcquisizione.ToString("_d-M-yyyy_HH.mm.ss_") + freq.ToString() + "Hz";

                        sw = new StreamWriter(filename + ".csv");

                        // prepara la scrittura dell'xml
                        xmlDoc = new XmlDocument();
                        XmlTextWriter xmlWriter = new XmlTextWriter(filename + ".xml", System.Text.Encoding.UTF8);
                        xmlWriter.Formatting = Formatting.Indented;
                        xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                        xmlWriter.WriteStartElement("acquisizione");
                        xmlWriter.Close();
                        xmlDoc.Load(filename + ".xml");
                        XmlNode root = xmlDoc.DocumentElement;
                        XmlAttribute oraAcquisizione = xmlDoc.CreateAttribute("ora");
                        oraAcquisizione.Value = dataAcquisizione.ToString(@"HH\:mm\:ss");
                        root.Attributes.Append(oraAcquisizione);
                        XmlElement staz = xmlDoc.CreateElement("stazionamento");
                        XmlElement gir = xmlDoc.CreateElement("girata");
                        XmlElement pos = xmlDoc.CreateElement("posizionamento");
                        root.AppendChild(staz);
                        root.AppendChild(gir);
                        root.AppendChild(pos);
                        xmlDoc.Save(filename + ".xml");

                        // a questo punto ho ricevuto dei dati quindi setto start a 2 che sta a indicare che il primo pacchetto
                        // è inviato e che le operazioni primarie non sono più necessarie
                        finestra.start = 2;
                    }

                    if (sampwin.Count != 0)
                    {
                        sampwinPassato = sampwin.GetRange(0, NMAX / 2);
                    }

                    // inizio procedura di salvataggio dei dati nella finestra d'analisi
                    sampwin.Clear();
                    for (int t = 0; t < N; t++)
                    {
                        sampwin.Add(finestra.leggi(t));
                    }

                    if (N == NMAX) // ho ancora campioni dopo da analizzare o perlomeno la metà successiva
                    {
                        finestra.swap(N / 2);
                    }
                    else
                    {
                        finestra.swap(N); // ho analizzato  l'ultimo campione di dati quindi swappo tutto
                    }
                    start = finestra.start;
                    Monitor.Pulse(finestra);
                }
                if (start != 0)
                {
                    // ANALISI
                    
                    // scrivi csv
                    aggiornaCSV();

                    // imposta titoli dei pannelli dei grafici
                    Program.form1.setTitlesGraph(new string[] { "moduloAcc", "moduloGyr", "orient"}); 

                    // prendi valori da gui
                    int valoreSmooth = Program.form1.getValoreSmooth();
                    int sensoreModuloAcc = Program.form1.getSensoreAccellerometro();
                    int sensoreModuloGyr = Program.form1.getSensoreGiroscopio();

                    if (soggettoCorrente == 2)
                    {//Rilevamento automatico. Soggetto corrente mi serve solo per sapere se sono alla prima finestra.
                        soggettoCorrente = rilevaSoggetto(moduloAcc(sensoreModuloAcc, sampwin));
                        Program.form1.setSoggetto(soggettoCorrente);
                    }

                    List<float[]> ar = new List<float[]>();

                    float[] orientamentoRilevato = orientamento(soggettoCorrente); // orientamento originale
                    
                    // ORIENTAMENTO SMOOTHATO
                    if (valoreSmooth != 0)
                    {
                        float[] orientamentoSmooth; // orientamento smooth
                        if (orientamentoPassato != null)
                        {
                            // crea l'orientamento totale copiando prima il passato poi quello attuale e futuro
                            float[] orientamentoTotale = new float[orientamentoPassato.Length + orientamentoRilevato.Length];
                            orientamentoPassato.CopyTo(orientamentoTotale, 0);
                            orientamentoRilevato.CopyTo(orientamentoTotale, orientamentoPassato.Length);
                            // smootha su tutto
                            orientamentoSmooth = smooth(valoreSmooth, orientamentoTotale);

                            // crea orientamento finale dato dalla lunghezza di quello smoothato - quello passato
                            float[] orientamentoFinale = new float[orientamentoSmooth.Length - orientamentoPassato.Length];
                            // copia i valori nell'array finale da quello smoothato
                            for (int i = orientamentoPassato.Length, j = 0; i < orientamentoSmooth.Length; i++, j++)
                                orientamentoFinale[j] = orientamentoSmooth[i];
                            orientamentoSmooth = orientamentoFinale;
                        }
                        else
                        {
                            orientamentoSmooth = smooth(valoreSmooth, orientamentoRilevato);
                        }

                        // copia i primi NMAX/2 per il passato
                        orientamentoPassato = new float[NMAX / 2];
                        Array.Copy(orientamentoRilevato, 0, orientamentoPassato, 0, NMAX / 2);
                        
                        List<float[,]> sampwinTotale = new List<float[,]>(sampwinPassato);
                        sampwinTotale.AddRange(sampwin); // crea passato presente e futuro
                        smooth(valoreSmooth, orientamentoRilevato);
                        ar.Add(moduloAcc(sensoreModuloAcc, sampwinTotale, valoreSmooth));
                        ar.Add(moduloGyr(sensoreModuloGyr, sampwinTotale, valoreSmooth));
                        ar.Add(orientamentoSmooth);
                    }
                    else // non è impostato alcuno smooth
                    {
                        ar.Add(moduloAcc(sensoreModuloAcc, sampwin, valoreSmooth));
                        ar.Add(moduloGyr(sensoreModuloGyr, sampwin, valoreSmooth));
                        ar.Add(orientamentoRilevato);
                    }

                    // threads per scrivere sulla form stazionamento, posizionamento e girata
                    printStaz = new Thread(new ParameterizedThreadStart(stampaStazionamento));
                    printStaz.Start(new object[] { riconosciStazionamento(soggettoCorrente) });

                    printPos = new Thread(new ParameterizedThreadStart(stampaPosizionamento));
                    printPos.Start(new object[] { laySitStand(soggettoCorrente) });

                    printGir = new Thread(new ParameterizedThreadStart(stampaGirata));
                    printGir.Start(new object[] { riconosciGirata(orientamentoRilevato, soggettoCorrente) });

                    object[] array = ar.ToArray();

                    mre.WaitOne(); // si ferma se è stato messo in pausa

                    // aggiorna grafici
                    flushGraph(TEMPO, array);
                    
                    // attende la fine dei thread che scrivono lo stazionamento, il posizionamento e la girata
                    printStaz.Join();
                    printPos.Join();
                    printGir.Join();

                    // tengo traccia del tempo di inizio del campione che sto analizzando
                    // N.B. il tempo è inteso come il numero di pacchetti totali analizzati
                    if (N == NMAX)
                    {
                        TEMPO = TEMPO + N/2;
                    }
                    else
                    {
                        TEMPO = TEMPO + N;
                    }
                    
                    // se non ho più niente da leggere chiudo gli stream aperti, setto allo stato iniziale le variabili
                    // e forzo la terminazione del thread impostando start a 0 e vedi else successivo!!!
                    if (finestra.finestra.Count == 0)
                    {
                        Program.form1.enabledFiles();
                        sw.Close();
                        stop = true;
                        finestra.start = 0;
                        finestra.fine = false;
                        Finestra.id = null;
                        Finestra.freq = 0;
                    }
                }
                else
                {
                    stop = true; // impostando stop a true il thread termina
                }
            }
        }


        /* 
         * 
         * 
         *
         * 
         * 
        */


        //############################################################
        // INVIO DATI AI GRAFICI

        private void flushGraph(int tempo,object[] dati)
        {
            int numGraph = dati.Length;
            object[] graphData=new object[numGraph];
            int lung = 0;
            if (N<NMAX) // minore di 500: passo l'array con i campioni della dimensione N 
            {
                graphData = dati;
                lung = N;
            }
            else // passo l'array con i campioni con la dimensione N/2 + 1
            {
                for (int x = 0; x < numGraph; x++)
                {
                    float[] da = (float[])dati[x];
                    float[] a = new float[(N / 2) + 1];
                    Array.Copy(da, a, (N / 2) + 1);

                    graphData[x] = a;
                }
                lung = (N / 2) + 1;
            }
            
            float time = tempo / 50;

            for (int i = 0; i < lung - 25; i = i + 25)
            {
                
                object[] subGraphData = new object[numGraph];
                
                for (int x = 0; x < numGraph; x++)
                {
                    float[] da = (float[])graphData[x];
                    float[] a = new float[26];
                    Array.Copy(da,i,a,0,26);
                    subGraphData[x] = a;
                }
                mre.WaitOne(); // si ferma se è stato messo in pausa
                Program.form1.writeChart(subGraphData, time);
                time = time + (0.02F * 25);
                // attendi 0,05 s
                Thread.Sleep(500);
            }
        }
        //############################################################
        //############################################################v


        //############################################################
        // FUNZIONI PER I THREAD DI STAMPA

        // log stazionamento
        private void stampaStazionamento(object sta)
        {
            bool[] staz = (bool[])((object[])sta)[0];

            int n;
            if (N < NMAX)
            {
                n = N;
            }
            else
            {
                n = NMAX / 2;
            }
            for (int i = 0; i < n; i++) 
            {
                int stazionamento;
                if (staz[i])
                    stazionamento = 1;
                else
                    stazionamento = 0;
                mre.WaitOne(); // si ferma se è stato messo in pausa
                if (stazionamento != ultimoStaz)
                {
                    TimeSpan tempo = TimeSpan.FromSeconds((double)(TEMPO + i) / 50);
                    if (staz[i])
                    {                        
                        Program.form1.setStazLabel(tempo.ToString(@"mm\:ss\,fff") + " - Soggetto fermo\n");
                        salvaDati("stazionamento", "fermo", (dataAcquisizione + tempo).ToString(@"HH\:mm\:ss\,fff"));
                    }
                    else
                    {                        
                        Program.form1.setStazLabel(tempo.ToString(@"mm\:ss\,fff") + " - Soggetto in movimento\n");
                        salvaDati("stazionamento", "in movimento", (dataAcquisizione + tempo).ToString(@"HH\:mm\:ss\,fff"));
                    }
                    ultimoStaz = stazionamento;
                }
                Thread.Sleep(20); // attendo 0,02s per sincronizzare ogni campione con il tempo reale
            }
        }
        // log posizionamento
        private void stampaPosizionamento(object pos)
        {
            float[] posz = (float[])((object[])pos)[0];

            int n;
            if (N < NMAX)
            {
                n = N;
            }
            else
            {
                n = NMAX / 2;
            }
            for (int i = 0; i < n; i++)
            {
                mre.WaitOne(); // si ferma se è stato messo in pausa
                if (posz[i] != ultimoPos)
                {
                    TimeSpan tempo = TimeSpan.FromSeconds((double)(TEMPO + i) / 50);
                    
                    switch ((int)posz[i])
                    {
                        case 0:
                            Program.form1.setPosLabel(tempo.ToString(@"mm\:ss\,fff") + " - Sdraiato\n");
                            salvaDati("posizionamento", "sdraiato", (dataAcquisizione + tempo).ToString(@"HH\:mm\:ss\,fff"));
                            break;
                        case 1:
                            Program.form1.setPosLabel(tempo.ToString(@"mm\:ss\,fff") + " - Sdraiato/seduto\n");
                            salvaDati("posizionamento", "sdraiato/seduto", (dataAcquisizione + tempo).ToString(@"HH\:mm\:ss\,fff"));
                            break;
                        case 2:
                            Program.form1.setPosLabel(tempo.ToString(@"mm\:ss\,fff") + " - Seduto\n");
                            salvaDati("posizionamento", "seduto", (dataAcquisizione + tempo).ToString(@"HH\:mm\:ss\,fff"));
                            break;
                        case 3:
                            Program.form1.setPosLabel(tempo.ToString(@"mm\:ss\,fff") + " - In piedi\n");
                            salvaDati("posizionamento", "in piedi", (dataAcquisizione + tempo).ToString(@"HH\:mm\:ss\,fff"));
                            break;
                    }
                    ultimoPos = posz[i];
                }
                Thread.Sleep(20); // attendo 0,02s per sincronizzare ogni campione con il tempo reale
            }
        }
        // log girata
        private void stampaGirata(object gir)
        {
            
            float[] girataFinestraCorrente = (float[])((object[])gir)[0];

            int n;
            if (N < NMAX)
            {
                n = N;
            }
            else
            {
                n = NMAX / 2;
            }
            
            for (int i = 0; i < n; i++) 
            {
                mre.WaitOne(); // si ferma se è stato messo in pausa
                if (girataFinestraCorrente[i] != ultimaGir)
                {
                    TimeSpan tempo = TimeSpan.FromSeconds((double)(TEMPO + i) / 50);
                    if (girataFinestraCorrente[i]==1)
                    {                        
                        Program.form1.setGirLabel(tempo.ToString(@"mm\:ss\,fff") + " - Girata destra\n");
                        salvaDati("girata", "destra", (dataAcquisizione + tempo).ToString(@"HH\:mm\:ss\,fff"));
                    }
                    else if (girataFinestraCorrente[i] == -1)
                    {                        
                        Program.form1.setGirLabel(tempo.ToString(@"mm\:ss\,fff") + " - Girata sinistra\n");
                        salvaDati("girata", "sinistra", (dataAcquisizione + tempo).ToString(@"HH\:mm\:ss\,fff"));
                    }
                    else
                    {                        
                        Program.form1.setGirLabel(tempo.ToString(@"mm\:ss\,fff") + " - Girata terminata\n");
                        salvaDati("girata", "terminata", (dataAcquisizione + tempo).ToString(@"HH\:mm\:ss\,fff"));
                    }
                    ultimaGir = girataFinestraCorrente[i];
                }
                Thread.Sleep(20); // attendo 0,02s per sincronizzare ogni campione con il tempo reale
            }
            
        }
        //############################################################
        //############################################################


        //###########################################################
        // SALVATAGGIO DATI SU FILE

        // SCRITTURA CSV
        private void aggiornaCSV()
        {
            int n;
            if ( N < NMAX )
            {
                n = N;
            }
            else
            {
                n = NMAX / 2;
            }
            for (int t = 0; t < n; t++)
            {
                for (int s = 0; s < 5; s++)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        sw.Write(sampwin[t][s, i].ToString());
                        sw.Write(";");
                    }
                    sw.Write(";");
                }
                sw.WriteLine(";");
            }

        }
        // AGGIORNAMENTO BASE DI DATI XML
        private void salvaDati(string azione, string tipo, string tempo)
        {
            XmlElement azioneNode = xmlDoc.CreateElement("azione");
            XmlElement tipoNode = xmlDoc.CreateElement("tipo");
            XmlText txtTipoNode = xmlDoc.CreateTextNode(tipo);
            XmlElement tempoNode = xmlDoc.CreateElement("tempo");
            XmlText txtTempoNode = xmlDoc.CreateTextNode(tempo);

            tipoNode.AppendChild(txtTipoNode);
            tempoNode.AppendChild(txtTempoNode);

            azioneNode.AppendChild(tipoNode);
            azioneNode.AppendChild(tempoNode);

            XmlNode root = xmlDoc.DocumentElement;
            XmlNode rootAzione = (((XmlElement)root).GetElementsByTagName(azione)).Item(0);
            rootAzione.AppendChild(azioneNode);

            mut.WaitOne();
            xmlDoc.Save(filename + ".xml");
            mut.ReleaseMutex();
        }
        //############################################################
        //############################################################


        //###########################################################
        // FUNZIONI MATEMATICHE DI UTILITA'

        // MODULO ACCELEROMETRO
        private float[] moduloAcc(int s, List<float[,]> sampwin, int smoothValue=0)
        {
            float[] risultato = new float[N];
            if ( smoothValue == 0 )
            {
                for (int t = 0; t < N; t++)
                {
                    risultato[t] = ((float)Math.Sqrt(Math.Pow(sampwin[t][s, 0], 2) + Math.Pow(sampwin[t][s, 1], 2) + Math.Pow(sampwin[t][s, 2], 2))); 
                }
            }
            else
            {
                int lunghezzaSampwin = sampwin.Count;
                float[] moduloAcc = new float[lunghezzaSampwin];
                for (int t = 0; t < lunghezzaSampwin; t++)
                {
                    moduloAcc[t] = ((float)Math.Sqrt(Math.Pow(sampwin[t][s, 0], 2) + Math.Pow(sampwin[t][s, 1], 2) + Math.Pow(sampwin[t][s, 2], 2)));
                }
                float[] risultatoSmooth = smooth(smoothValue, moduloAcc); // calcolato il moduloAcc si fa lo smooth con il passato

                int lunghezzaPrecedenti = sampwinPassato.Count;
                risultato = new float[risultatoSmooth.Length - lunghezzaPrecedenti];

                for (int i = lunghezzaPrecedenti, j = 0; i < risultatoSmooth.Length; i++, j++)
                {
                    risultato[j] = risultatoSmooth[i];
                }
            }
            return risultato;
        }
        // MODULO GIROSCOPIO
        private float[] moduloGyr(int s, List<float[,]> sampwin, int smoothValue = 0)
        {
            float[] risultato = new float[N];
            if (smoothValue == 0)
            {
                for (int t = 0; t < N; t++)
                {
                    risultato[t] = ((float)Math.Sqrt(Math.Pow(sampwin[t][s, 3], 2) + Math.Pow(sampwin[t][s, 4], 2) + Math.Pow(sampwin[t][s, 5], 2)));
                }
            }
            else
            {
                int lunghezzaSampwin = sampwin.Count;
                float[] moduloGyr = new float[lunghezzaSampwin];
                for (int t = 0; t < sampwin.Count; t++)
                {
                    moduloGyr[t] = ((float)Math.Sqrt(Math.Pow(sampwin[t][s, 3], 2) + Math.Pow(sampwin[t][s, 4], 2) + Math.Pow(sampwin[t][s, 5], 2)));
                }
                float[] risultatoSmooth = smooth(smoothValue, moduloGyr); // calcolato il moduloAcc si fa lo smooth con il passato

                int lunghezzaPrecedenti = sampwinPassato.Count;
                risultato = new float[risultatoSmooth.Length - lunghezzaPrecedenti];

                for (int i = lunghezzaPrecedenti, j = 0; i < risultatoSmooth.Length; i++, j++)
                {
                    risultato[j] = risultatoSmooth[i];
                }
            }
            return risultato;
        }
        // ORIENTAMENTO MAGNETOMETRO
        private float[] orientaMag(int organismo = 0)
        {
            //organismo 0 = uomo
            //organismo 1 = cavallo

            int sensore = 0;
            //sensore è settato a 0 di default in quanto per l'uomo il sensore utile è quello sul bacino (sensore 0)

            int coordinata1 = 7;
            int coordinata2 = 8;
            if (organismo == 1)
            { //se cavallo cambia gli assi perchè il sensore è stato messo in modo diverso
                coordinata1 = 6;
                coordinata2 = 7;
                sensore = 4;
            }
            float[] orientaMag = new float[N];
            for (int t = 0; t < N; t++)
            {
                orientaMag[t] = (float)Math.Atan(sampwin[t][sensore, coordinata1] / sampwin[t][sensore, coordinata2]);
            }
            return orientaMag;
        }
        // SMOOTHING
        private float[] smooth(int k, float[] data)
        {
            float[] smooth = new float[data.Length];

            for (int t = 0; t < data.Length; t++)
            {
                float totale = 0;
                int n = 0;
                for (int i = t - k; i <= t + k; i++)
                {
                    if (i >= 0 && i < data.Length)
                    {
                        totale += data[i];
                        n++;
                    }
                }
                smooth[t] = totale / n;
            }
            return smooth;
        }
        // RAPPORTO INCREMENTALE
        private float[] RIfunc(float[] data)
        {
            float[] RI = new float[data.Length];
            for (int i = 0; i < data.Length - 1; i++)
            {
                RI[i] = data[i + 1] - data[i];
            }
            return RI;
        }
        // RAPPORTO INCREMENTALE con variabile incremento
        private float[] RIfunc(int incremento, float[] data, int moltiplicatore=1)
        {
            float[] RI = new float[data.Length];
            for (int i = 0; i < data.Length - 1; i++)
            {
                if (i + incremento < data.Length)
                {

                    RI[i] = ((data[i + incremento] - data[i])/incremento)*moltiplicatore;
                }
                else
                {
                    RI[i] = 0;
                }
            }
            return RI;
        }
        // DEVIAZIONE STANDARD CON FINESTRA MOBILE SU MEDIA MOBILE
        private float[] stDev(int k, float[] data)
        {
            float[] stDev = new float[data.Length];
            float[] media = smooth(k, data);
            for (int t = 0; t < data.Length; t++)
            {
                float valore;
                float totale = 0;
                int N = 0;
                for (int i = t - k; i <= t + k; i++)
                {
                    if (i >= 0 && i < data.Length)
                    {
                        valore = data[i] - media[i];
                        totale += (float)Math.Pow(valore, 2);
                        N++;
                    }
                }
                stDev[t] = (float)Math.Sqrt(totale / N);
            }
            return stDev;
        }
        // DEVIAZIONE STANDARD CON FINESTRA MOBILE E MEDIA FISSA
        private float[] stDev(int k, float[] data, float media)
        {
            float[] stDev = new float[data.Length];
            for (int t = 0; t < data.Length; t++)
            {
                float valore;
                float totale = 0;
                int N = 0;
                for (int i = t - k; i <= t + k; i++)
                {
                    if (i >= 0 && i < data.Length)
                    {
                        valore = data[i] - media;
                        totale += (float)Math.Pow(valore, 2);
                        N++;
                    }
                }
                stDev[t] = (float)Math.Sqrt(totale / N);
            }
            return stDev;
        }
        //############################################################
        //############################################################



        //############################################################
        // RICONOSCIMENTO DEI DATI

        // riconosci stazionamento
        private bool[] riconosciStazionamento(int organismo = 0)
        {
            bool[] stazionamento = new bool[N];
            int sensore = 0;
            if ( organismo == 1 )
                sensore = 4;
            float[] deviazione = stDev(25, moduloAcc(sensore, sampwin), 9.81F); 
            bool statoPrecedente = true; //parto con il soggetto fermo
           
            for (int i = 0; i < N; i++)
            {
                if (deviazione[i] > 0.65 && deviazione[i] < 0.95)
                {
                    stazionamento[i] = statoPrecedente; // "soluzione pensata pensando all'accensione della caldaia"
                    //mantengo lo stesso stato perchè altrimenti, con brevi variazioni nell'intorno
                    //continuerei a rilevare cambiamenti continui di stato non desiderati.
                    //nel range di incertezza mantengo lo stato precedente
                }
                else if (deviazione[i] < 0.80 ) //è stato portato da 1 a 0.6 in quanto prove sperimentali hanno dimostrato il migliore riconoscimento con valori più bassi
                {
                    stazionamento[i] = true; // "soggetto fermo"
                }
                else
                {
                    stazionamento[i] = false; // "soggetto in movimento"
                }
                statoPrecedente = stazionamento[i];
            }
            return stazionamento;
        }
        // riconosci orientamento
        private float[] orientamento(int organismo = 0)
        {

            float[] arcotg = orientaMag(organismo); // dati dal magnetometro, resistuisce l'arcotangente di y e z
            float[] arctan = new float[N]; // dati arcotangente da stampare


            for (int i = 0; i < N; i++)
            {
                if ( i == 0 )
                {
                    if ( ultimoOrienta == 0 )
                        arctan[i] = arcotg[i];
                    else
                        if (Math.Abs(arcotg[i + 1] - arcotg[i]) > 1.2F)
                            arctan[i] = ultimoOrienta;
                        else
                        {
                            // differenza tra successore e attuale
                            float diff = arcotg[i + 1] - arcotg[i];
                            // il valore è dato da precendente + differenza
                            arctan[i] = ultimoOrienta + diff;
                        }
                }
                else if (i == (N - 1))
                {
                    arctan[i] = arctan[i - 1];
                }
                else
                {
                    // controlla il salto con la differenza assoluta tra successore e a attuale
                    if (Math.Abs(arcotg[i + 1] - arcotg[i]) > 1.2F)
                    {
                        arctan[i] = arctan[i - 1];
                    }
                    else
                    {
                        // differenza tra successore e attuale
                        float diff = arcotg[i + 1] - arcotg[i];
                        // il valore è dato da precendente + differenza
                        arctan[i] = arctan[i - 1] + diff;
                    }
                }
            }
            ultimoOrienta = arctan[N / 2 - 1];
            return arctan;
        }
        // riconosci girata
        public float[] riconosciGirata(float[] orientamento, int organismo = 0)
        {
            orientamento=smooth(30, orientamento); // appiattisce le oscillazioni sia del corpo che del garrese del cavallo che ogni oscillazione involontaria o sportatura del segnale
            float[] girata = new float[N];
            int visibilitaGirata=100; //è una variabile che permette di rendere più chiaro il rapporto incrementale moltiplicandone gli effetti.
                                                    //permette inoltre di rendere visibile il rapporto incrementale se graficato.
            int incremento = 30; //30, scelto da me (Brenna) basandomi su test sperimentali //100 scelto da me basandomi su test sperimentali
            if(organismo == 1){ //se è un cavallo gira più lentamente
                incremento = 100;
            }


            float[] indicatoreDiGirata = RIfunc(incremento, orientamento, visibilitaGirata);

            float valorePrecedente = 0;

            for(int i=0; i<orientamento.Length; i++){
                if(indicatoreDiGirata[i]>0.4F && indicatoreDiGirata[i]<0.6F){ //Solito principio della caldaia o range di sicurezza
                    girata[i] = valorePrecedente;
                }else if(Math.Abs(indicatoreDiGirata[i])>0.5){//se il soggetto sta girando
                    if(indicatoreDiGirata[i]>0.5){ //il numero0.6 è stato scelto da me basandomi su dati sperimentali
                        girata[i] = -1; //il soggetto gira a sinistra
                    }else{ //altrimenti sto girando nell'altro senso
                        girata[i] = 1; //il soggetto gira a destra
                    }
                }else{
                     girata[i]=0; //0 in questo caso significa non sta girando
                }
            }
            return girata;
        }
        // riconosci soggetto (cavallo/uomo)
        private int rilevaSoggetto(float[] moduloAcc)
        {
            float[] variazioneModuloAcc = stDev(1, moduloAcc, 9.81F);

            int contaVariazione = 0;
            for (int i = 0; i < variazioneModuloAcc.Length / 2; i++)
            {
                if (variazioneModuloAcc[i] > 1.7)
                {
                    contaVariazione = contaVariazione + 1;
                }
            }

            if (contaVariazione > variazioneModuloAcc.Length / 10)
            {
                return 1; //Soggetto a cavallo
            }
            else
            {
                return 0; //Soggetto a piedi
            }
        }
        // riconosci posizionamento
        private float[] laySitStand(int organismo = 0)
        {
            // convenzione:
            // LAY (sdraiato)   -> 0
            // LAY/SIT          -> 1
            // SIT (seduto)     -> 2
            // STAND (in piedi) -> 3
            float[] posizionamento = new float[N];
            bool[] stazionamento = riconosciStazionamento(organismo);
            float[] datiDaElaborare = new float[N];
            int sensore = 0;
            int coordinata = 0;

            if (organismo == 1)
            {
                sensore = 4;
                coordinata = 2;
            }
            float statoPrecedente;
            if (sampwin[0][sensore, coordinata] < 3)
                statoPrecedente = 0;
            else if (sampwin[0][sensore, coordinata] < 8)
                statoPrecedente = 2;
            else
                statoPrecedente = 3;

            for (int z = 0; z < N; z++)
            {
                datiDaElaborare[z] = sampwin[z][sensore, coordinata];
            }


            datiDaElaborare = smooth(10, datiDaElaborare);

            for (int i = 0; i < N; i++)
            {
                if (stazionamento[i] == false)
                {
                    if (datiDaElaborare[i] <= 2.7F)
                    {
                        posizionamento[i] = 0;
                    }
                    else if (datiDaElaborare[i] > 2.7F && datiDaElaborare[i] <= 3.7F)
                    {
                        posizionamento[i] = 1;
                    }
                    /* //Per quanto possa muoversi una persona da seduto, muove solo piedi e mani (dovrei verificare gli altri 4 sensori, quello sul bacino non mi basta)
                     * //Per i nostri obiettivi quindi, se una persona è in movimento non è seduta!!!
                    else if (sampwin[i, 0, 0] > 3.7F && sampwin[i, 0, 0] <= 7F)
                    {
                        posizionamento[i] = 2;
                    }
                    */
                    else
                    {
                        posizionamento[i] = 3;
                    }
                }
                else
                { //altrimenti se il soggetto è fermo, tara il sistema di riconoscimento in modo che possa capire se il soggetto è seduto o meno

                    if (datiDaElaborare[i] <= 2.7F)
                    {
                        posizionamento[i] = 0;
                    }
                    else if (datiDaElaborare[i] > 2.7F && datiDaElaborare[i] <= 3.7F)
                    {
                        posizionamento[i] = 1;
                    }
                    else if (datiDaElaborare[i] > 8.2 && datiDaElaborare[i] < 8.9 && statoPrecedente >= 2) //Solito principio della caldaia
                    {
                        posizionamento[i] = statoPrecedente;
                    }
                    else if (datiDaElaborare[i] > 3.7F && datiDaElaborare[i] < 8.6F)
                    {
                        posizionamento[i] = 2;
                    }
                    else
                    {
                        posizionamento[i] = 3;
                    }
                }
            }
            return posizionamento;
        }
        //############################################################
        //############################################################


        //############################################################
        // KILL YOUR OWN CHILDREN IS (fucking) AWESOME
        public void Abort()
        {
            printGir.Abort();
            printGir.Join();
            printPos.Abort();
            printPos.Join();
            printStaz.Abort();
            printStaz.Join();
        }
    }
}

