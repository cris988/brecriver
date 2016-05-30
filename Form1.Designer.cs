namespace Progetto
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.separator1EditMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.restartMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.separator2EditMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.clearMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.csvMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xmlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblGirata = new System.Windows.Forms.Label();
            this.lblPosizionamento = new System.Windows.Forms.Label();
            this.rtbGirataLog = new System.Windows.Forms.RichTextBox();
            this.rtbPosLog = new System.Windows.Forms.RichTextBox();
            this.lblStazionamento = new System.Windows.Forms.Label();
            this.rtbStazLog = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbRilevamentoAutomatico = new System.Windows.Forms.RadioButton();
            this.rbCavallo = new System.Windows.Forms.RadioButton();
            this.rbUomo = new System.Windows.Forms.RadioButton();
            this.groupGyr = new System.Windows.Forms.GroupBox();
            this.g4 = new System.Windows.Forms.RadioButton();
            this.g3 = new System.Windows.Forms.RadioButton();
            this.g2 = new System.Windows.Forms.RadioButton();
            this.g1 = new System.Windows.Forms.RadioButton();
            this.g0 = new System.Windows.Forms.RadioButton();
            this.groupAcc = new System.Windows.Forms.GroupBox();
            this.a4 = new System.Windows.Forms.RadioButton();
            this.a3 = new System.Windows.Forms.RadioButton();
            this.a2 = new System.Windows.Forms.RadioButton();
            this.a1 = new System.Windows.Forms.RadioButton();
            this.a0 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSmooth = new System.Windows.Forms.CheckBox();
            this.txtValoreSmooth = new System.Windows.Forms.TextBox();
            this.pausaToolBar = new System.Windows.Forms.ToolStripButton();
            this.resumeToolBar = new System.Windows.Forms.ToolStripButton();
            this.separator1ToolBar = new System.Windows.Forms.ToolStripSeparator();
            this.restartToolBar = new System.Windows.Forms.ToolStripButton();
            this.stopToolBar = new System.Windows.Forms.ToolStripButton();
            this.separator2ToolBar = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolBar = new System.Windows.Forms.ToolStripButton();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.lblRilevamento = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.panel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupGyr.SuspendLayout();
            this.groupAcc.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // zedGraphControl
            // 
            this.zedGraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.zedGraphControl.IsShowHScrollBar = true;
            this.zedGraphControl.Location = new System.Drawing.Point(191, 83);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.ScrollGrace = 0D;
            this.zedGraphControl.ScrollMaxX = 0D;
            this.zedGraphControl.ScrollMaxY = 0D;
            this.zedGraphControl.ScrollMaxY2 = 0D;
            this.zedGraphControl.ScrollMinX = 0D;
            this.zedGraphControl.ScrollMinY = 0D;
            this.zedGraphControl.ScrollMinY2 = 0D;
            this.zedGraphControl.Size = new System.Drawing.Size(829, 485);
            this.zedGraphControl.TabIndex = 2;
            // 
            // lblStatusLabel
            // 
            this.lblStatusLabel.AutoSize = true;
            this.lblStatusLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblStatusLabel.Location = new System.Drawing.Point(196, 58);
            this.lblStatusLabel.Name = "lblStatusLabel";
            this.lblStatusLabel.Size = new System.Drawing.Size(68, 13);
            this.lblStatusLabel.TabIndex = 3;
            this.lblStatusLabel.Text = "Connessione";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.viewMenuItem,
            this.aboutMenuItem});
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(1020, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "File";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(94, 22);
            this.exitMenuItem.Text = "Esci";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pauseMenuItem,
            this.resumeMenuItem,
            this.separator1EditMenuItem,
            this.restartMenuItem,
            this.stopMenuItem,
            this.separator2EditMenuItem,
            this.clearMenuItem});
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editMenuItem.Text = "Edit";
            // 
            // pauseMenuItem
            // 
            this.pauseMenuItem.Image = global::Progetto.Properties.Resources.Pause;
            this.pauseMenuItem.Name = "pauseMenuItem";
            this.pauseMenuItem.Size = new System.Drawing.Size(167, 22);
            this.pauseMenuItem.Text = "Pausa";
            this.pauseMenuItem.Click += new System.EventHandler(this.pauseMenuItem_Click);
            // 
            // resumeMenuItem
            // 
            this.resumeMenuItem.Image = global::Progetto.Properties.Resources.Resume;
            this.resumeMenuItem.Name = "resumeMenuItem";
            this.resumeMenuItem.Size = new System.Drawing.Size(167, 22);
            this.resumeMenuItem.Text = "Riprendi";
            this.resumeMenuItem.Click += new System.EventHandler(this.resumeMenuItem_Click);
            // 
            // separator1EditMenuItem
            // 
            this.separator1EditMenuItem.Name = "separator1EditMenuItem";
            this.separator1EditMenuItem.Size = new System.Drawing.Size(164, 6);
            // 
            // restartMenuItem
            // 
            this.restartMenuItem.Image = global::Progetto.Properties.Resources.Restart;
            this.restartMenuItem.Name = "restartMenuItem";
            this.restartMenuItem.Size = new System.Drawing.Size(167, 22);
            this.restartMenuItem.Text = "Riavvia";
            this.restartMenuItem.Click += new System.EventHandler(this.restartMenuItem_Click);
            // 
            // stopMenuItem
            // 
            this.stopMenuItem.Image = global::Progetto.Properties.Resources.Stop;
            this.stopMenuItem.Name = "stopMenuItem";
            this.stopMenuItem.Size = new System.Drawing.Size(167, 22);
            this.stopMenuItem.Text = "Ferma";
            this.stopMenuItem.Click += new System.EventHandler(this.stopMenuItem_Click);
            // 
            // separator2EditMenuItem
            // 
            this.separator2EditMenuItem.Name = "separator2EditMenuItem";
            this.separator2EditMenuItem.Size = new System.Drawing.Size(164, 6);
            // 
            // clearMenuItem
            // 
            this.clearMenuItem.Image = global::Progetto.Properties.Resources.Clear;
            this.clearMenuItem.Name = "clearMenuItem";
            this.clearMenuItem.Size = new System.Drawing.Size(167, 22);
            this.clearMenuItem.Text = "Ripulisci il grafico";
            this.clearMenuItem.Click += new System.EventHandler(this.clearMenuItem_Click);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csvMenuItem,
            this.xmlMenuItem});
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewMenuItem.Text = "View";
            // 
            // csvMenuItem
            // 
            this.csvMenuItem.Enabled = false;
            this.csvMenuItem.Name = "csvMenuItem";
            this.csvMenuItem.Size = new System.Drawing.Size(119, 22);
            this.csvMenuItem.Text = "File CSV";
            this.csvMenuItem.Click += new System.EventHandler(this.csvMenuItem_Click);
            // 
            // xmlMenuItem
            // 
            this.xmlMenuItem.Enabled = false;
            this.xmlMenuItem.Name = "xmlMenuItem";
            this.xmlMenuItem.Size = new System.Drawing.Size(119, 22);
            this.xmlMenuItem.Text = "File XML";
            this.xmlMenuItem.Click += new System.EventHandler(this.xmlMenuItem_Click);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(24, 20);
            this.aboutMenuItem.Text = "?";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.AliceBlue;
            this.panel.Controls.Add(this.tabControl1);
            this.panel.Location = new System.Drawing.Point(0, 46);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(190, 523);
            this.panel.TabIndex = 13;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(187, 520);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.AliceBlue;
            this.tabPage1.Controls.Add(this.lblGirata);
            this.tabPage1.Controls.Add(this.lblPosizionamento);
            this.tabPage1.Controls.Add(this.rtbGirataLog);
            this.tabPage1.Controls.Add(this.rtbPosLog);
            this.tabPage1.Controls.Add(this.lblStazionamento);
            this.tabPage1.Controls.Add(this.rtbStazLog);
            this.tabPage1.Location = new System.Drawing.Point(23, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(160, 512);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            // 
            // lblGirata
            // 
            this.lblGirata.AutoSize = true;
            this.lblGirata.Location = new System.Drawing.Point(2, 248);
            this.lblGirata.Name = "lblGirata";
            this.lblGirata.Size = new System.Drawing.Size(35, 13);
            this.lblGirata.TabIndex = 18;
            this.lblGirata.Text = "Girata";
            // 
            // lblPosizionamento
            // 
            this.lblPosizionamento.AutoSize = true;
            this.lblPosizionamento.Location = new System.Drawing.Point(2, 125);
            this.lblPosizionamento.Name = "lblPosizionamento";
            this.lblPosizionamento.Size = new System.Drawing.Size(81, 13);
            this.lblPosizionamento.TabIndex = 18;
            this.lblPosizionamento.Text = "Posizionamento";
            // 
            // rtbGirataLog
            // 
            this.rtbGirataLog.BackColor = System.Drawing.SystemColors.Info;
            this.rtbGirataLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbGirataLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbGirataLog.Location = new System.Drawing.Point(3, 265);
            this.rtbGirataLog.Name = "rtbGirataLog";
            this.rtbGirataLog.ReadOnly = true;
            this.rtbGirataLog.Size = new System.Drawing.Size(154, 101);
            this.rtbGirataLog.TabIndex = 17;
            this.rtbGirataLog.Text = "";
            // 
            // rtbPosLog
            // 
            this.rtbPosLog.BackColor = System.Drawing.SystemColors.Info;
            this.rtbPosLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbPosLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbPosLog.Location = new System.Drawing.Point(3, 142);
            this.rtbPosLog.Name = "rtbPosLog";
            this.rtbPosLog.ReadOnly = true;
            this.rtbPosLog.Size = new System.Drawing.Size(154, 101);
            this.rtbPosLog.TabIndex = 17;
            this.rtbPosLog.Text = "";
            // 
            // lblStazionamento
            // 
            this.lblStazionamento.AutoSize = true;
            this.lblStazionamento.Location = new System.Drawing.Point(1, 3);
            this.lblStazionamento.Name = "lblStazionamento";
            this.lblStazionamento.Size = new System.Drawing.Size(77, 13);
            this.lblStazionamento.TabIndex = 14;
            this.lblStazionamento.Text = "Stazionamento";
            // 
            // rtbStazLog
            // 
            this.rtbStazLog.BackColor = System.Drawing.SystemColors.Info;
            this.rtbStazLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbStazLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbStazLog.Location = new System.Drawing.Point(3, 21);
            this.rtbStazLog.Name = "rtbStazLog";
            this.rtbStazLog.ReadOnly = true;
            this.rtbStazLog.Size = new System.Drawing.Size(154, 101);
            this.rtbStazLog.TabIndex = 13;
            this.rtbStazLog.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupGyr);
            this.tabPage2.Controls.Add(this.groupAcc);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(23, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(160, 512);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Opzioni";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbRilevamentoAutomatico);
            this.groupBox1.Controls.Add(this.rbCavallo);
            this.groupBox1.Controls.Add(this.rbUomo);
            this.groupBox1.Location = new System.Drawing.Point(3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 69);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Soggetto:";
            // 
            // rbRilevamentoAutomatico
            // 
            this.rbRilevamentoAutomatico.AutoSize = true;
            this.rbRilevamentoAutomatico.Checked = true;
            this.rbRilevamentoAutomatico.Location = new System.Drawing.Point(6, 42);
            this.rbRilevamentoAutomatico.Name = "rbRilevamentoAutomatico";
            this.rbRilevamentoAutomatico.Size = new System.Drawing.Size(140, 17);
            this.rbRilevamentoAutomatico.TabIndex = 18;
            this.rbRilevamentoAutomatico.TabStop = true;
            this.rbRilevamentoAutomatico.Text = "Rilevamento Automatico";
            this.rbRilevamentoAutomatico.UseVisualStyleBackColor = true;
            this.rbRilevamentoAutomatico.CheckedChanged += new System.EventHandler(this.changeSubject);
            // 
            // rbCavallo
            // 
            this.rbCavallo.AutoSize = true;
            this.rbCavallo.Location = new System.Drawing.Point(65, 19);
            this.rbCavallo.Name = "rbCavallo";
            this.rbCavallo.Size = new System.Drawing.Size(70, 17);
            this.rbCavallo.TabIndex = 17;
            this.rbCavallo.Text = "A Cavallo";
            this.rbCavallo.UseVisualStyleBackColor = true;
            this.rbCavallo.CheckedChanged += new System.EventHandler(this.changeSubject);
            // 
            // rbUomo
            // 
            this.rbUomo.AutoSize = true;
            this.rbUomo.Location = new System.Drawing.Point(6, 19);
            this.rbUomo.Name = "rbUomo";
            this.rbUomo.Size = new System.Drawing.Size(53, 17);
            this.rbUomo.TabIndex = 15;
            this.rbUomo.Text = "Uomo";
            this.rbUomo.UseVisualStyleBackColor = true;
            this.rbUomo.CheckedChanged += new System.EventHandler(this.changeSubject);
            // 
            // groupGyr
            // 
            this.groupGyr.Controls.Add(this.g4);
            this.groupGyr.Controls.Add(this.g3);
            this.groupGyr.Controls.Add(this.g2);
            this.groupGyr.Controls.Add(this.g1);
            this.groupGyr.Controls.Add(this.g0);
            this.groupGyr.Location = new System.Drawing.Point(3, 226);
            this.groupGyr.Name = "groupGyr";
            this.groupGyr.Size = new System.Drawing.Size(154, 89);
            this.groupGyr.TabIndex = 14;
            this.groupGyr.TabStop = false;
            this.groupGyr.Text = "Modulo Gyr";
            // 
            // g4
            // 
            this.g4.AutoSize = true;
            this.g4.Location = new System.Drawing.Point(87, 43);
            this.g4.Name = "g4";
            this.g4.Size = new System.Drawing.Size(61, 17);
            this.g4.TabIndex = 4;
            this.g4.Text = "Sens. 5";
            this.g4.UseVisualStyleBackColor = true;
            this.g4.CheckedChanged += new System.EventHandler(this.changeGyrSens);
            // 
            // g3
            // 
            this.g3.AutoSize = true;
            this.g3.Location = new System.Drawing.Point(87, 19);
            this.g3.Name = "g3";
            this.g3.Size = new System.Drawing.Size(61, 17);
            this.g3.TabIndex = 3;
            this.g3.Text = "Sens. 4";
            this.g3.UseVisualStyleBackColor = true;
            this.g3.CheckedChanged += new System.EventHandler(this.changeGyrSens);
            // 
            // g2
            // 
            this.g2.AutoSize = true;
            this.g2.Location = new System.Drawing.Point(7, 67);
            this.g2.Name = "g2";
            this.g2.Size = new System.Drawing.Size(61, 17);
            this.g2.TabIndex = 2;
            this.g2.Text = "Sens. 3";
            this.g2.UseVisualStyleBackColor = true;
            this.g2.CheckedChanged += new System.EventHandler(this.changeGyrSens);
            // 
            // g1
            // 
            this.g1.AutoSize = true;
            this.g1.Location = new System.Drawing.Point(7, 43);
            this.g1.Name = "g1";
            this.g1.Size = new System.Drawing.Size(61, 17);
            this.g1.TabIndex = 1;
            this.g1.Text = "Sens. 2";
            this.g1.UseVisualStyleBackColor = true;
            this.g1.CheckedChanged += new System.EventHandler(this.changeGyrSens);
            // 
            // g0
            // 
            this.g0.AutoSize = true;
            this.g0.Checked = true;
            this.g0.Location = new System.Drawing.Point(7, 19);
            this.g0.Name = "g0";
            this.g0.Size = new System.Drawing.Size(61, 17);
            this.g0.TabIndex = 0;
            this.g0.TabStop = true;
            this.g0.Text = "Sens. 1";
            this.g0.UseVisualStyleBackColor = true;
            this.g0.CheckedChanged += new System.EventHandler(this.changeGyrSens);
            // 
            // groupAcc
            // 
            this.groupAcc.Controls.Add(this.a4);
            this.groupAcc.Controls.Add(this.a3);
            this.groupAcc.Controls.Add(this.a2);
            this.groupAcc.Controls.Add(this.a1);
            this.groupAcc.Controls.Add(this.a0);
            this.groupAcc.Location = new System.Drawing.Point(3, 131);
            this.groupAcc.Name = "groupAcc";
            this.groupAcc.Size = new System.Drawing.Size(154, 89);
            this.groupAcc.TabIndex = 13;
            this.groupAcc.TabStop = false;
            this.groupAcc.Text = "Modulo Acc";
            // 
            // a4
            // 
            this.a4.AutoSize = true;
            this.a4.Location = new System.Drawing.Point(87, 40);
            this.a4.Name = "a4";
            this.a4.Size = new System.Drawing.Size(61, 17);
            this.a4.TabIndex = 4;
            this.a4.Text = "Sens. 5";
            this.a4.UseVisualStyleBackColor = true;
            this.a4.CheckedChanged += new System.EventHandler(this.changeAccSens);
            // 
            // a3
            // 
            this.a3.AutoSize = true;
            this.a3.Location = new System.Drawing.Point(87, 16);
            this.a3.Name = "a3";
            this.a3.Size = new System.Drawing.Size(61, 17);
            this.a3.TabIndex = 3;
            this.a3.Text = "Sens. 4";
            this.a3.UseVisualStyleBackColor = true;
            this.a3.CheckedChanged += new System.EventHandler(this.changeAccSens);
            // 
            // a2
            // 
            this.a2.AutoSize = true;
            this.a2.Location = new System.Drawing.Point(7, 64);
            this.a2.Name = "a2";
            this.a2.Size = new System.Drawing.Size(61, 17);
            this.a2.TabIndex = 2;
            this.a2.Text = "Sens. 3";
            this.a2.UseVisualStyleBackColor = true;
            this.a2.CheckedChanged += new System.EventHandler(this.changeAccSens);
            // 
            // a1
            // 
            this.a1.AutoSize = true;
            this.a1.Location = new System.Drawing.Point(7, 40);
            this.a1.Name = "a1";
            this.a1.Size = new System.Drawing.Size(61, 17);
            this.a1.TabIndex = 1;
            this.a1.Text = "Sens. 2";
            this.a1.UseVisualStyleBackColor = true;
            this.a1.CheckedChanged += new System.EventHandler(this.changeAccSens);
            // 
            // a0
            // 
            this.a0.AutoSize = true;
            this.a0.Checked = true;
            this.a0.Location = new System.Drawing.Point(7, 16);
            this.a0.Name = "a0";
            this.a0.Size = new System.Drawing.Size(61, 17);
            this.a0.TabIndex = 0;
            this.a0.TabStop = true;
            this.a0.Text = "Sens. 1";
            this.a0.UseVisualStyleBackColor = true;
            this.a0.CheckedChanged += new System.EventHandler(this.changeAccSens);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.chkSmooth);
            this.groupBox3.Controls.Add(this.txtValoreSmooth);
            this.groupBox3.Location = new System.Drawing.Point(3, 79);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(154, 46);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Smooth";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Valore";
            // 
            // chkSmooth
            // 
            this.chkSmooth.AutoSize = true;
            this.chkSmooth.Location = new System.Drawing.Point(6, 19);
            this.chkSmooth.Name = "chkSmooth";
            this.chkSmooth.Size = new System.Drawing.Size(54, 17);
            this.chkSmooth.TabIndex = 6;
            this.chkSmooth.Text = "Abilita";
            this.chkSmooth.UseVisualStyleBackColor = true;
            // 
            // txtValoreSmooth
            // 
            this.txtValoreSmooth.Location = new System.Drawing.Point(114, 17);
            this.txtValoreSmooth.MaxLength = 2;
            this.txtValoreSmooth.Name = "txtValoreSmooth";
            this.txtValoreSmooth.Size = new System.Drawing.Size(25, 20);
            this.txtValoreSmooth.TabIndex = 8;
            this.txtValoreSmooth.Text = "0";
            this.txtValoreSmooth.LostFocus += new System.EventHandler(this.txtValoreSmooth_LostFocus);
            // 
            // pausaToolBar
            // 
            this.pausaToolBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pausaToolBar.Image = global::Progetto.Properties.Resources.Pause;
            this.pausaToolBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pausaToolBar.Name = "pausaToolBar";
            this.pausaToolBar.Size = new System.Drawing.Size(23, 22);
            this.pausaToolBar.Text = "Pause";
            this.pausaToolBar.Click += new System.EventHandler(this.pauseToolBar_Click);
            // 
            // resumeToolBar
            // 
            this.resumeToolBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.resumeToolBar.Image = global::Progetto.Properties.Resources.Resume;
            this.resumeToolBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resumeToolBar.Name = "resumeToolBar";
            this.resumeToolBar.Size = new System.Drawing.Size(23, 22);
            this.resumeToolBar.Text = "Riprendi";
            this.resumeToolBar.Click += new System.EventHandler(this.resumeToolBar_Click);
            // 
            // separator1ToolBar
            // 
            this.separator1ToolBar.Name = "separator1ToolBar";
            this.separator1ToolBar.Size = new System.Drawing.Size(6, 25);
            // 
            // restartToolBar
            // 
            this.restartToolBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.restartToolBar.Image = global::Progetto.Properties.Resources.Restart;
            this.restartToolBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.restartToolBar.Name = "restartToolBar";
            this.restartToolBar.Size = new System.Drawing.Size(23, 22);
            this.restartToolBar.Text = "Riavvia";
            this.restartToolBar.Click += new System.EventHandler(this.restartToolBar_Click);
            // 
            // stopToolBar
            // 
            this.stopToolBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopToolBar.Image = global::Progetto.Properties.Resources.Stop;
            this.stopToolBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopToolBar.Name = "stopToolBar";
            this.stopToolBar.Size = new System.Drawing.Size(23, 22);
            this.stopToolBar.Text = "Ferma";
            this.stopToolBar.Click += new System.EventHandler(this.stopToolBar_Click);
            // 
            // separator2ToolBar
            // 
            this.separator2ToolBar.Name = "separator2ToolBar";
            this.separator2ToolBar.Size = new System.Drawing.Size(6, 25);
            // 
            // clearToolBar
            // 
            this.clearToolBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearToolBar.Image = global::Progetto.Properties.Resources.Clear;
            this.clearToolBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearToolBar.Name = "clearToolBar";
            this.clearToolBar.Size = new System.Drawing.Size(23, 22);
            this.clearToolBar.Text = "Ripulisci il grafico";
            this.clearToolBar.Click += new System.EventHandler(this.clearToolBar_Click);
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pausaToolBar,
            this.resumeToolBar,
            this.separator1ToolBar,
            this.restartToolBar,
            this.stopToolBar,
            this.separator2ToolBar,
            this.clearToolBar});
            this.toolBar.Location = new System.Drawing.Point(0, 24);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(1020, 25);
            this.toolBar.TabIndex = 14;
            this.toolBar.Text = "toolStrip1";
            // 
            // lblRilevamento
            // 
            this.lblRilevamento.AutoSize = true;
            this.lblRilevamento.Location = new System.Drawing.Point(925, 58);
            this.lblRilevamento.Name = "lblRilevamento";
            this.lblRilevamento.Size = new System.Drawing.Size(0, 13);
            this.lblRilevamento.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1020, 570);
            this.Controls.Add(this.lblRilevamento);
            this.Controls.Add(this.lblStatusLabel);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.zedGraphControl);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(1022, 568);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BRECRIVER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupGyr.ResumeLayout(false);
            this.groupGyr.PerformLayout();
            this.groupAcc.ResumeLayout(false);
            this.groupAcc.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resumeMenuItem;
        private System.Windows.Forms.ToolStripSeparator separator1EditMenuItem;
        private System.Windows.Forms.ToolStripSeparator separator2EditMenuItem;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblStazionamento;
        private System.Windows.Forms.RichTextBox rtbStazLog;
        private System.Windows.Forms.GroupBox groupGyr;
        private System.Windows.Forms.RadioButton g4;
        private System.Windows.Forms.RadioButton g3;
        private System.Windows.Forms.RadioButton g2;
        private System.Windows.Forms.RadioButton g1;
        private System.Windows.Forms.RadioButton g0;
        private System.Windows.Forms.GroupBox groupAcc;
        private System.Windows.Forms.RadioButton a4;
        private System.Windows.Forms.RadioButton a3;
        private System.Windows.Forms.RadioButton a2;
        private System.Windows.Forms.RadioButton a1;
        private System.Windows.Forms.RadioButton a0;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSmooth;
        private System.Windows.Forms.TextBox txtValoreSmooth;
        private System.Windows.Forms.ToolStripButton pausaToolBar;
        private System.Windows.Forms.ToolStripButton resumeToolBar;
        private System.Windows.Forms.ToolStripSeparator separator1ToolBar;
        private System.Windows.Forms.ToolStripButton restartToolBar;
        private System.Windows.Forms.ToolStripButton stopToolBar;
        private System.Windows.Forms.ToolStripSeparator separator2ToolBar;
        private System.Windows.Forms.ToolStripButton clearToolBar;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.Label lblPosizionamento;
        private System.Windows.Forms.RichTextBox rtbPosLog;
        private System.Windows.Forms.Label lblGirata;
        private System.Windows.Forms.RichTextBox rtbGirataLog;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem csvMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xmlMenuItem;
        private System.Windows.Forms.Label lblRilevamento;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbRilevamentoAutomatico;
        private System.Windows.Forms.RadioButton rbCavallo;
        private System.Windows.Forms.RadioButton rbUomo;
    }
}

