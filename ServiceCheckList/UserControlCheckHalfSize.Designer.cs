namespace ServiceCheckList
{
    partial class UserControlCheckHalfSize
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTaskName = new System.Windows.Forms.Label();
            this.btnPassed = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTaskName
            // 
            this.lblTaskName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTaskName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskName.Location = new System.Drawing.Point(33, 1);
            this.lblTaskName.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.lblTaskName.Name = "lblTaskName";
            this.lblTaskName.Size = new System.Drawing.Size(66, 24);
            this.lblTaskName.TabIndex = 2;
            this.lblTaskName.Text = "SOME TASK NAME";
            this.lblTaskName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTaskName.Click += new System.EventHandler(this.lblTaskName_Click);
            this.lblTaskName.MouseEnter += new System.EventHandler(this.lblTaskName_MouseEnter);
            // 
            // btnPassed
            // 
            this.btnPassed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPassed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPassed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPassed.Location = new System.Drawing.Point(3, 1);
            this.btnPassed.Margin = new System.Windows.Forms.Padding(4);
            this.btnPassed.Name = "btnPassed";
            this.btnPassed.Size = new System.Drawing.Size(27, 25);
            this.btnPassed.TabIndex = 0;
            this.btnPassed.Text = "✔";
            this.btnPassed.UseVisualStyleBackColor = true;
            this.btnPassed.Click += new System.EventHandler(this.btnPassed_Click);
            this.btnPassed.MouseEnter += new System.EventHandler(this.btnCheck_MouseEnter);
            this.btnPassed.MouseLeave += new System.EventHandler(this.btnCheck_MouseLeave);
            this.btnPassed.MouseHover += new System.EventHandler(this.btnCheck_MouseHover);
            // 
            // UserControlCheckHalfSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.lblTaskName);
            this.Controls.Add(this.btnPassed);
            this.Margin = new System.Windows.Forms.Padding(32, 1, 1, 1);
            this.Name = "UserControlCheckHalfSize";
            this.Size = new System.Drawing.Size(113, 27);
            this.Load += new System.EventHandler(this.UserControlCheckFailed_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblTaskName;
        public System.Windows.Forms.Button btnPassed;
    }
}
