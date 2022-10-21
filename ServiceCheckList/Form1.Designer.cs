namespace ServiceCheckList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTicketCompleted = new System.Windows.Forms.Button();
            this.cbPrevChecklists = new System.Windows.Forms.ComboBox();
            this.rbNotes = new System.Windows.Forms.RichTextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbStartup = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbTicketNo = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.rbDropOffNotes = new System.Windows.Forms.RichTextBox();
            this.btnUnlockTicket = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rdbCleanUp = new System.Windows.Forms.RadioButton();
            this.rdbNewInstallation = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dxDiagDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runAllProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iConToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killOpenProcessesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteShortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diskCleanupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installAgentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.serialBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbMake = new System.Windows.Forms.TextBox();
            this.tbModel = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(100, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "COMPUTER SERVICES BY KENNY P";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(376, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "EST. 2010 - HOLLYWOOD, FLORIDA, USA - CSBYKP.COM - (954) 602-0004";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(9, 115);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(1, 1, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(445, 275);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // btnTicketCompleted
            // 
            this.btnTicketCompleted.BackColor = System.Drawing.Color.Lime;
            this.btnTicketCompleted.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTicketCompleted.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTicketCompleted.ForeColor = System.Drawing.Color.Black;
            this.btnTicketCompleted.Location = new System.Drawing.Point(240, 545);
            this.btnTicketCompleted.Name = "btnTicketCompleted";
            this.btnTicketCompleted.Size = new System.Drawing.Size(194, 28);
            this.btnTicketCompleted.TabIndex = 16;
            this.btnTicketCompleted.Text = "TICKET COMPLETED";
            this.btnTicketCompleted.UseVisualStyleBackColor = false;
            this.btnTicketCompleted.Click += new System.EventHandler(this.btnRemoveStartup_Click);
            // 
            // cbPrevChecklists
            // 
            this.cbPrevChecklists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrevChecklists.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrevChecklists.FormattingEnabled = true;
            this.cbPrevChecklists.Location = new System.Drawing.Point(20, 65);
            this.cbPrevChecklists.Name = "cbPrevChecklists";
            this.cbPrevChecklists.Size = new System.Drawing.Size(122, 23);
            this.cbPrevChecklists.TabIndex = 8;
            this.cbPrevChecklists.SelectedIndexChanged += new System.EventHandler(this.cbDate_SelectedIndexChanged);
            // 
            // rbNotes
            // 
            this.rbNotes.DetectUrls = false;
            this.rbNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNotes.Location = new System.Drawing.Point(114, 483);
            this.rbNotes.Name = "rbNotes";
            this.rbNotes.Size = new System.Drawing.Size(329, 32);
            this.rbNotes.TabIndex = 13;
            this.rbNotes.Text = "Notes...";
            this.rbNotes.TextChanged += new System.EventHandler(this.rbNotes_TextChanged);
            this.rbNotes.Leave += new System.EventHandler(this.rbNotes_Leave);
            this.rbNotes.MouseEnter += new System.EventHandler(this.rbNotes_MouseEnter);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.Lime;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.Location = new System.Drawing.Point(20, 545);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(194, 28);
            this.btnConnect.TabIndex = 15;
            this.btnConnect.Text = "CONNECT TO TICKET";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbStartup
            // 
            this.cbStartup.AutoSize = true;
            this.cbStartup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStartup.Location = new System.Drawing.Point(198, 62);
            this.cbStartup.Name = "cbStartup";
            this.cbStartup.Size = new System.Drawing.Size(68, 20);
            this.cbStartup.TabIndex = 14;
            this.cbStartup.Text = "Startup";
            this.cbStartup.UseVisualStyleBackColor = true;
            this.cbStartup.CheckedChanged += new System.EventHandler(this.cbStartup_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(65, 484);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "Notes:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(55, 521);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "Ticket #:";
            // 
            // tbTicketNo
            // 
            this.tbTicketNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTicketNo.Location = new System.Drawing.Point(114, 520);
            this.tbTicketNo.Name = "tbTicketNo";
            this.tbTicketNo.Size = new System.Drawing.Size(251, 20);
            this.tbTicketNo.TabIndex = 14;
            this.tbTicketNo.Click += new System.EventHandler(this.tbTicketNo_Click);
            this.tbTicketNo.TextChanged += new System.EventHandler(this.tbTicketNo_TextChanged);
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.Location = new System.Drawing.Point(114, 458);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(251, 20);
            this.tbPassword.TabIndex = 12;
            this.tbPassword.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
            this.tbPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPassword_KeyPress);
            this.tbPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbPassword_KeyUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(43, 459);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "Password:";
            // 
            // tbUserName
            // 
            this.tbUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUserName.Location = new System.Drawing.Point(114, 396);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.ReadOnly = true;
            this.tbUserName.Size = new System.Drawing.Size(251, 20);
            this.tbUserName.TabIndex = 19;
            this.tbUserName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(63, 399);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "Name:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(17, 422);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 15);
            this.label9.TabIndex = 22;
            this.label9.Text = "Drop Off Notes:";
            // 
            // rbDropOffNotes
            // 
            this.rbDropOffNotes.DetectUrls = false;
            this.rbDropOffNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDropOffNotes.Location = new System.Drawing.Point(114, 421);
            this.rbDropOffNotes.Name = "rbDropOffNotes";
            this.rbDropOffNotes.ReadOnly = true;
            this.rbDropOffNotes.Size = new System.Drawing.Size(329, 32);
            this.rbDropOffNotes.TabIndex = 21;
            this.rbDropOffNotes.Text = "Drop Off Notes...";
            this.rbDropOffNotes.TextChanged += new System.EventHandler(this.tbDropOffNotes_TextChanged);
            this.rbDropOffNotes.MouseEnter += new System.EventHandler(this.rbDropOffNotes_MouseEnter);
            // 
            // btnUnlockTicket
            // 
            this.btnUnlockTicket.Location = new System.Drawing.Point(368, 518);
            this.btnUnlockTicket.Name = "btnUnlockTicket";
            this.btnUnlockTicket.Size = new System.Drawing.Size(48, 23);
            this.btnUnlockTicket.TabIndex = 23;
            this.btnUnlockTicket.Text = "unlock";
            this.btnUnlockTicket.UseVisualStyleBackColor = true;
            this.btnUnlockTicket.EnabledChanged += new System.EventHandler(this.btnUnlockTicket_EnabledChanged);
            this.btnUnlockTicket.Click += new System.EventHandler(this.btnUnlockTicket_Click);
            // 
            // rdbCleanUp
            // 
            this.rdbCleanUp.AutoSize = true;
            this.rdbCleanUp.Checked = true;
            this.rdbCleanUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbCleanUp.Location = new System.Drawing.Point(20, 95);
            this.rdbCleanUp.Margin = new System.Windows.Forms.Padding(2);
            this.rdbCleanUp.Name = "rdbCleanUp";
            this.rdbCleanUp.Size = new System.Drawing.Size(77, 17);
            this.rdbCleanUp.TabIndex = 24;
            this.rdbCleanUp.TabStop = true;
            this.rdbCleanUp.Text = "Clean Up";
            this.rdbCleanUp.UseVisualStyleBackColor = true;
            // 
            // rdbNewInstallation
            // 
            this.rdbNewInstallation.AutoSize = true;
            this.rdbNewInstallation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbNewInstallation.Location = new System.Drawing.Point(114, 95);
            this.rdbNewInstallation.Margin = new System.Windows.Forms.Padding(2);
            this.rdbNewInstallation.Name = "rdbNewInstallation";
            this.rdbNewInstallation.Size = new System.Drawing.Size(116, 17);
            this.rdbNewInstallation.TabIndex = 25;
            this.rdbNewInstallation.Text = "New Installation";
            this.rdbNewInstallation.UseVisualStyleBackColor = true;
            this.rdbNewInstallation.CheckedChanged += new System.EventHandler(this.rdbNewInstallation_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(464, 24);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dxDiagDetailsToolStripMenuItem,
            this.runAllProgramToolStripMenuItem,
            this.iConToolStripMenuItem,
            this.killOpenProcessesToolStripMenuItem,
            this.deleteShortToolStripMenuItem,
            this.diskCleanupToolStripMenuItem,
            this.installAgentToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.fileToolStripMenuItem.Text = "Options";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // dxDiagDetailsToolStripMenuItem
            // 
            this.dxDiagDetailsToolStripMenuItem.Name = "dxDiagDetailsToolStripMenuItem";
            this.dxDiagDetailsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.dxDiagDetailsToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.dxDiagDetailsToolStripMenuItem.Text = "DxDiag Details";
            this.dxDiagDetailsToolStripMenuItem.Click += new System.EventHandler(this.dxDiagDetailsToolStripMenuItem_Click);
            // 
            // runAllProgramToolStripMenuItem
            // 
            this.runAllProgramToolStripMenuItem.Name = "runAllProgramToolStripMenuItem";
            this.runAllProgramToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.runAllProgramToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.runAllProgramToolStripMenuItem.Text = "Run All Programs";
            this.runAllProgramToolStripMenuItem.Click += new System.EventHandler(this.runAllProgramToolStripMenuItem_Click);
            // 
            // iConToolStripMenuItem
            // 
            this.iConToolStripMenuItem.Name = "iConToolStripMenuItem";
            this.iConToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.iConToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.iConToolStripMenuItem.Text = "Icon && Power Settings";
            this.iConToolStripMenuItem.Click += new System.EventHandler(this.iConToolStripMenuItem_Click);
            // 
            // killOpenProcessesToolStripMenuItem
            // 
            this.killOpenProcessesToolStripMenuItem.Name = "killOpenProcessesToolStripMenuItem";
            this.killOpenProcessesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.killOpenProcessesToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.killOpenProcessesToolStripMenuItem.Text = "Kill Open Processes";
            this.killOpenProcessesToolStripMenuItem.Click += new System.EventHandler(this.killOpenProcessesToolStripMenuItem_Click);
            // 
            // deleteShortToolStripMenuItem
            // 
            this.deleteShortToolStripMenuItem.Name = "deleteShortToolStripMenuItem";
            this.deleteShortToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deleteShortToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.deleteShortToolStripMenuItem.Text = "Delete Shortcuts";
            this.deleteShortToolStripMenuItem.Click += new System.EventHandler(this.deleteShortToolStripMenuItem_Click);
            // 
            // diskCleanupToolStripMenuItem
            // 
            this.diskCleanupToolStripMenuItem.Name = "diskCleanupToolStripMenuItem";
            this.diskCleanupToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.diskCleanupToolStripMenuItem.Text = "Disk Cleanup";
            this.diskCleanupToolStripMenuItem.Click += new System.EventHandler(this.diskCleanupToolStripMenuItem_Click);
            // 
            // installAgentToolStripMenuItem
            // 
            this.installAgentToolStripMenuItem.Name = "installAgentToolStripMenuItem";
            this.installAgentToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.installAgentToolStripMenuItem.Text = "Install Agent";
            this.installAgentToolStripMenuItem.Click += new System.EventHandler(this.installAgentToolStripMenuItem_ClickAsync);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(296, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Serial #:";
            // 
            // serialBox
            // 
            this.serialBox.Location = new System.Drawing.Point(354, 89);
            this.serialBox.Name = "serialBox";
            this.serialBox.ReadOnly = true;
            this.serialBox.Size = new System.Drawing.Size(100, 20);
            this.serialBox.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Make :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(299, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Model :";
            // 
            // tbMake
            // 
            this.tbMake.Location = new System.Drawing.Point(354, 48);
            this.tbMake.Name = "tbMake";
            this.tbMake.ReadOnly = true;
            this.tbMake.Size = new System.Drawing.Size(100, 20);
            this.tbMake.TabIndex = 30;
            // 
            // tbModel
            // 
            this.tbModel.Location = new System.Drawing.Point(354, 68);
            this.tbModel.Name = "tbModel";
            this.tbModel.ReadOnly = true;
            this.tbModel.Size = new System.Drawing.Size(100, 20);
            this.tbModel.TabIndex = 31;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(464, 578);
            this.Controls.Add(this.tbModel);
            this.Controls.Add(this.tbMake);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.serialBox);
            this.Controls.Add(this.rdbNewInstallation);
            this.Controls.Add(this.rdbCleanUp);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnUnlockTicket);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.rbDropOffNotes);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbTicketNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbStartup);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.rbNotes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbPrevChecklists);
            this.Controls.Add(this.btnTicketCompleted);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 599);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSBYKP Ticket Checklist";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnTicketCompleted;
        private System.Windows.Forms.ComboBox cbPrevChecklists;
        private System.Windows.Forms.RichTextBox rbNotes;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.CheckBox cbStartup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbTicketNo;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox rbDropOffNotes;
        private System.Windows.Forms.Button btnUnlockTicket;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton rdbCleanUp;
        private System.Windows.Forms.RadioButton rdbNewInstallation;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dxDiagDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runAllProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iConToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem killOpenProcessesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteShortToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox serialBox;
        private System.Windows.Forms.ToolStripMenuItem diskCleanupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installAgentToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbMake;
        private System.Windows.Forms.TextBox tbModel;
    }
}

