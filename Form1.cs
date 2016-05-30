using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Progetto
{
    public partial class Form1 : Form
    {
        // Delegati che servono a scrivere sulla GUI dall'analyzer
        delegate void SetgraphCallback(object[] value, float tempo);
        delegate void setStatus_(bool connesso, int stato);
        delegate void setTitlesGraph_(string[] yLabels);
        delegate void clear_();
        delegate void setStazLabel_(string value);
        delegate void setPosLabel_(string value);
        delegate void setGirLabel_(string value);
        delegate void setSoggetto_(int value);
        delegate void enabledFiles_();

        // variabili interne per le impostazioni runtime
        private int sensoreAccellerometro = 0; //visto che all'avvio è selezionato il sensore a0 lo setto a 0
        private int sensoreGiroscopio = 0; //visto che all'avvio è selezionato il sensore g0 lo setto a 0
        private int valoreSmooth;
        private int soggetto = 2; //rilevamento automatico

        public Form1()
        {
            InitializeComponent();
        }

        //############################################################
        // FORM EVENTS

        // EVENTO DI APERTURA DEL FORM
        private void Form1_Load(object sender, EventArgs e)
        {
            //Al caricamento del form lancio una funzione che prende come parametro l'unico grafico che abbiamo
            CreateChart(zedGraphControl);

            // Regola la dimensione del form per netbook obsoleti con risoluzione dello schermo inferiore a 1024x768
            if (Screen.PrimaryScreen.Bounds.Width <= 1024 && Screen.PrimaryScreen.Bounds.Height <= 768)
                this.WindowState = FormWindowState.Maximized;
        }
        // EVENTO DI CHIUSURA DEL FORM
        private void Form1_FormClosing(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                MessageBox.Show("invokeeeee");

            Program.close(); // termina tutto
            
        }
        //############################################################
        //############################################################



        //############################################################
        // FORM MENU
        
        // exit
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Program.close();
            this.Close();
        }
        // restart
        private void restartMenuItem_Click(object sender, EventArgs e)
        {
            Program.start();
            setStatus(false, 0);
        }
        // stop
        private void stopMenuItem_Click(object sender, EventArgs e)
        {
            Program.close();
            setStatus(false, 5);
        }
        // pause
        private void pauseMenuItem_Click(object sender, EventArgs e)
        {
            Program.pause();
            setStatus(true, 3);
        }
        // resume
        private void resumeMenuItem_Click(object sender, EventArgs e)
        {
            Program.resume();
            setStatus(true, 4);
        }
        // clearAll
        private void clearMenuItem_Click(object sender, EventArgs e)
        {
            clear();
        }
        // view csv
        private void csvMenuItem_Click(object sender, EventArgs e)
        {
            apriFile(Analyzer.filename + ".csv");
        }
        // view xml
        private void xmlMenuItem_Click(object sender, EventArgs e)
        {
            apriFile(Analyzer.filename + ".xml");
        }
        // about
        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            about aboutForm = new about();
            aboutForm.Show();
        }
        //############################################################
        //############################################################



        //############################################################
        // FORM TOOLBAR

        // pause
        private void pauseToolBar_Click(object sender, EventArgs e)
        {
            Program.pause();
            setStatus(true, 3);
        }
        // resume
        private void resumeToolBar_Click(object sender, EventArgs e)
        {
            Program.resume();
            setStatus(true, 4);
        }
        //restart
        private void restartToolBar_Click(object sender, EventArgs e)
        {
            Program.start();
            setStatus(false, 0);
        }
        // stop
        private void stopToolBar_Click(object sender, EventArgs e)
        {
            Program.close();
            setStatus(false, 5);
        }
        // clearAll
        private void clearToolBar_Click(object sender, EventArgs e)
        {
            clear();
        }
        //############################################################
        //############################################################



        //############################################################
        // ELEMENTI FORM EVENT
        private void txtValoreSmooth_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int valore;
            if (Int32.TryParse(tb.Text, out valore) && valore >= 0 && valore <= 250)
            {
                valoreSmooth = valore;
            }
            else
            {
                if (tb.Text == "")
                    tb.Text = "0";
                else
                {
                    MessageBox.Show("Il valore inserito non è valido");
                    tb.Text = "0";
                }
            }
        }

        private void changeAccSens(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                String nomeSensore = rb.Name;
                Match match = Regex.Match(nomeSensore, "[0-9]*$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Int32.TryParse(match.ToString(), out sensoreAccellerometro);
            }
        }

        private void changeGyrSens(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                String nomeSensore = rb.Name;
                Match match = Regex.Match(nomeSensore, "[0-9]*$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                Int32.TryParse(match.ToString(), out sensoreGiroscopio);
            }
        }

        private void changeSubject(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                String nomeSoggetto = rb.Text;
                switch (nomeSoggetto)
                {
                    case "Uomo":
                        this.soggetto = 0;
                        break;

                    case "A Cavallo":
                        this.soggetto = 1;
                        break;

                    case "Rilevamento Automatico":
                        this.soggetto = 2;
                        break;
                }
            }
        }

        public void setSoggetto(int sogg)
        {
            if (this.lblStatusLabel.InvokeRequired)
            {
                setSoggetto_ d = new setSoggetto_(setSoggetto);
                this.lblRilevamento.Invoke(d, sogg);
            }
            else
            {
                if (sogg == 0)
                {
                    this.lblRilevamento.Text = "Rilevato: Uomo";
                }else if(sogg == 1){
                    this.lblRilevamento.Text = "Rilevato: A Cavallo";
                }
            }
        }
        //############################################################
        //############################################################



        //############################################################
        // FUNZIONI GUI

        // CREAZIONE GRAFICO
        public void CreateChart(ZedGraphControl zgc)
        {

            // First, clear out any old GraphPane's from the MasterPane collection
            MasterPane master = zgc.MasterPane;
            master.PaneList.Clear();
            master.Border.IsVisible = false;
            master.Fill.IsVisible = false;
            master.Title.IsVisible = false;
            master.Margin.All = 0;
            master.InnerPaneGap = 0;
            //ColorSymbolRotator rotator = new ColorSymbolRotator();
            for (int j = 0; j < 3; j++)
            {
                GraphPane pane1 = new GraphPane(new Rectangle(), "", "Tempo in secondi", "");

                // Sfondo pannello grafico
                pane1.Fill.IsVisible = false;

                // Sfondo singolo grafico
                pane1.Chart.Fill = new Fill(Color.SeaShell);
                //pane1.Chart.Fill = new Fill(Color.White, Color.SteelBlue, 45.0F);
                // Impedisce il resize del font
                pane1.IsFontsScaled = false;
                // Nasconde la scala e il titol XAxis
                pane1.XAxis.Title.IsVisible = false;
                pane1.XAxis.Scale.IsVisible = false;
                // Nasconde la legenda, il bordo, e il titolo del grafico
                pane1.Legend.IsVisible = false;
                pane1.Border.IsVisible = false;
                pane1.Title.IsVisible = false;
                // Nasconde la linea dello zero
                pane1.YAxis.MajorGrid.IsZeroLine = false;
                // Impedisce che le "tacchette" dei valori delle x non escano fuori dal grafico
                pane1.XAxis.MajorTic.IsOutside = false;
                pane1.XAxis.MinorTic.IsOutside = false;
                // Imposta margini
                pane1.Margin.Bottom = 1;
                // Margine alto del primo grafico
                if (j == 0)
                    pane1.Margin.Top = 10;
                // Mostra il titolo, e la scala XAxis nell'ultimo grafico
                if (j == 2)
                {
                    pane1.XAxis.Title.IsVisible = true;
                    pane1.XAxis.Scale.IsVisible = true;
                    pane1.YAxis.MinorTic.IsAllTics = false;
                }
                // Nasconde ultimo valore della scala YAxis dal secondo grafico
                if (j > 0)
                {
                    pane1.YAxis.Scale.IsSkipLastLabel = true;
                    pane1.Margin.Top = 1;
                }

                // Scala asse x iniziale
                pane1.XAxis.Scale.Min = -2.5d;
                pane1.XAxis.Scale.Max = 2.5d;

                // Le "tacchette" non sono presenti nei lati opposti
                pane1.XAxis.MinorTic.IsOpposite = false;
                pane1.YAxis.MinorTic.IsOpposite = false;
                pane1.XAxis.MajorTic.IsOpposite = false;
                pane1.YAxis.MajorTic.IsOpposite = false;

                // Spazio a sinistra e a destra del margine per allineare i grafici
                pane1.YAxis.MinSpace = 60;
                pane1.Y2Axis.MinSpace = 10;

                master.Add(pane1);
            }

            master[0].YAxis.Scale.Min = 0;
            master[0].YAxis.Scale.Max = 20;
            master[1].YAxis.Scale.Min = 0;
            master[1].YAxis.Scale.Max = 6;
            master[2].YAxis.Scale.Min = -3;
            master[2].YAxis.Scale.Max = 5;
            
            
            // Refigure the axis ranges for the GraphPanes
            zgc.AxisChange();

            // Layout the GraphPanes using a default Pane Layout
            using (Graphics g = this.CreateGraphics())
            {

                master.SetLayout(g, PaneLayout.SingleColumn);

                master.AxisChange(g);

                // Synchronize the Axes

                zgc.IsAutoScrollRange = true;
                zgc.IsShowHScrollBar = true;
                zgc.IsSynchronizeXAxes = true;
                zgc.Width = this.Width - 210;
                zgc.Height = this.Height - 120;
                g.Dispose();

            }
        }
        // SCRITTURA GRAFICO
        public void writeChart(object[] obj, float tempo)
        {
            //Se la funzione writeChart è chiamata dall'esterno di form (ad esempio da un thread tipo l'analyzer)
            if (this.zedGraphControl.InvokeRequired)
            {
                //Passa il delegato all'invoke (che gestirà il multithreading e la mutua esclusione) e il mio array di float.
                this.zedGraphControl.Invoke(new SetgraphCallback(writeChart), obj, tempo);
            }
            else  //altrimenti se la chiamata arriva dalla classe form o è frutto dell'invoke
            {

                float[] primo = (float[])obj[0];
                int lung = primo.Length;
                float[] tempi = new float[obj.Length];

                PointPairList[] list = new PointPairList[obj.Length];
                for (int x = 0; x < obj.Length; x++)
                {
                    tempi[x] = tempo;
                    list[x] = new PointPairList();
                }

                for (int j = 0; j < lung; j++)
                {
                    for (int x = 0; x < obj.Length; x++)
                    {
                        float[] value = (float[])obj[x];
                        list[x].Add(tempi[x], value[j]);
                        tempi[x] = tempi[x] + 0.02F;
                        if (value[j] > zedGraphControl.MasterPane[x].YAxis.Scale.Max)
                            zedGraphControl.MasterPane[x].YAxis.Scale.Max = value[j]+1;
                        if (value[j] < zedGraphControl.MasterPane[x].YAxis.Scale.Min)
                            zedGraphControl.MasterPane[x].YAxis.Scale.Min = value[j]-1;
                    }
                }

                for (int x = 0; x < obj.Length; x++)
                {
                    LineItem curve = zedGraphControl.MasterPane[x].AddCurve("", list[x], Color.Red, SymbolType.None);
                    curve.Line.Width = 1.5F;
                    zedGraphControl.MasterPane[x].XAxis.Scale.Min = tempi[x] - 5;
                    zedGraphControl.MasterPane[x].XAxis.Scale.Max = tempi[x];
                }
                zedGraphControl.AxisChange();
                //Console.WriteLine("scrivo grafico");
                //zedGraphControl.Invalidate(new Rectangle(181, 83, 829, 485));
                zedGraphControl.Refresh();
                //zedGraphControl.Update();
                //Refresh();
            }
        }
        // RIPULISCI IL GRAFICO
        public void clear()
        {
            if (this.zedGraphControl.InvokeRequired)
            {
                clear_ d = new clear_(clear);
                this.zedGraphControl.Invoke(d);
            }
            else
            {
                for (int x = 0; x < 3; x++)
                {
                    zedGraphControl.MasterPane[x].CurveList.Clear();
                    zedGraphControl.MasterPane[x].XAxis.Scale.Min = -2.5d;
                    zedGraphControl.MasterPane[x].XAxis.Scale.Max = 2.5;
                }
                Refresh();
                zedGraphControl.Invalidate();
                rtbStazLog.Clear();
                rtbPosLog.Clear();
                rtbGirataLog.Clear();
                lblRilevamento.Text = "";
            }
        }
        // IMPOSTA LE ETICHETTE SUL FORM
        public void setTitlesGraph(string[] yLabels)
        {
            if (this.InvokeRequired)
            {
                setTitlesGraph_ d = new setTitlesGraph_(setTitlesGraph);
                this.Invoke(d, new object[] { yLabels });
            }
            else
            {
                for (int x = 0; x < yLabels.Length; x++)
                {
                    zedGraphControl.MasterPane[x].YAxis.Title.Text = yLabels[x];
                }
            }
        }
        // IMPOSTA LO STATO
        public void setStatus(bool connesso, int stato)
        {
            if (this.lblStatusLabel.InvokeRequired)
            {
                setStatus_ d = new setStatus_(setStatus);
                this.lblStatusLabel.Invoke(d, new object[] { connesso, stato });
            }
            else
            {
                if (connesso == true)
                {
                    switch (stato)
                    {
                        case 1:
                            lblStatusLabel.Text = "Connesso con " + Finestra.id + " ad una frequenza di " + Finestra.freq + " Hz";
                            break;

                        case 2:
                            lblStatusLabel.Text = "Ricezione dati in corso da " + Finestra.id + " ad una frequenza di " + Finestra.freq + " Hz";
                            break;

                        case 3:
                            lblStatusLabel.Text = "Analisi in pausa, per riprendere selezionare \"Riprendi\"";
                            break;

                        case 4:
                            lblStatusLabel.Text = "Analisi ripresa, elaborazione dati in corso da " + Finestra.id + " ad una frequenza di " + Finestra.freq + " Hz";
                            break;
                    }
                }
                else if (stato == 5)
                {
                    lblStatusLabel.Text = "L'analisi è stata terminata, per iniziare una nuova analisi selezionare la voce \"Riavvia\"";
                }
                else
                {
                    lblStatusLabel.Text = "In attesa di connessione su porta " + Program.port;
                }
            }
        }
        // SCRITTURA DATI STAZIONAMENTO 
        public void setStazLabel(string value)
        {
            if (this.rtbStazLog.InvokeRequired)
            {
                setStazLabel_ d = new setStazLabel_(setStazLabel);
                this.rtbStazLog.Invoke(d, value);
            }
            else
            {
                rtbStazLog.Text = rtbStazLog.Text + " " + value;
                rtbStazLog.SelectionStart = rtbStazLog.Text.Length;
                rtbStazLog.ScrollToCaret();
            }
        }
        // SCRITTURA DATI POSIZIONAMENTO
        public void setPosLabel(string value)
        {
            if (this.rtbPosLog.InvokeRequired)
            {
                setPosLabel_ d = new setPosLabel_(setPosLabel);
                this.rtbPosLog.Invoke(d, value);
            }
            else
            {
                rtbPosLog.Text = rtbPosLog.Text + " " + value;
                rtbPosLog.SelectionStart = rtbPosLog.Text.Length;
                rtbPosLog.ScrollToCaret();
            }
        }
        // SCRITTURA DATI GIRATA
        public void setGirLabel(string value)
        {
            if (this.rtbStazLog.InvokeRequired)
            {
                setGirLabel_ d = new setGirLabel_(setGirLabel);
                this.rtbGirataLog.Invoke(d, value);
            }
            else
            {
                rtbGirataLog.Text = rtbGirataLog.Text + " " + value;
                rtbGirataLog.SelectionStart = rtbGirataLog.Text.Length;
                rtbGirataLog.ScrollToCaret();
            }
        }
        //############################################################
        //############################################################



        //############################################################
        // FUNZIONI GET PARAMETRI

        // GET SENSORE ACCELEROMETRO
        public int getSensoreAccellerometro()
        {
            return sensoreAccellerometro;
        }
        // GET SENSORE GIROSCOPIO
        public int getSensoreGiroscopio()
        {
            return sensoreGiroscopio;
        }
        // GET SOGGETTO
        public int getSoggetto()
        {
            return soggetto;
        }
        // GET SMOOTH
        public int getValoreSmooth()
        {
            if (chkSmooth.Checked)
                return valoreSmooth;
            else
                return 0;
        }
        //############################################################
        //############################################################



        //############################################################
        // LETTURA FILE ESTERNI
        
        // APERTURA
        private void apriFile(string filename)
        {
            Process prc = new Process();
            try
            {
                prc.StartInfo.FileName = filename;
                prc.Start();
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }
        // ABILITAZIONE VOCI MENU
        public void enabledFiles()
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new enabledFiles_(enabledFiles));
            }
            else
            {
                csvMenuItem.Enabled = true;
                xmlMenuItem.Enabled = true;
            }
        }
        //############################################################
        //############################################################
    }
}
