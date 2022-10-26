﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace ServiceCheckList
{
    public partial class UserControlCheckFailed : UserControl
    {


        public UserControlCheckFailed()
        {
            InitializeComponent();
        }

        //0 - Not Set | 1 - Passed | 2 Failed
        public int Status = 0;
        public string TaskName = "";
        public DateTime LastEventDate = new DateTime(1901, 1, 1, 0, 0, 0);

        private void btnCheck_MouseHover(object sender, EventArgs e)
        {

        }

        private void btnCheck_MouseEnter(object sender, EventArgs e)
        {
            if (Status != 1)
            {
                btnPassed.BackColor = Color.LimeGreen;
                //   btnLabel.ForeColor = Color.LimeGreen;
            }
        }

        private void btnCheck_MouseLeave(object sender, EventArgs e)
        {
            if (Status != 1)
            {
                btnPassed.BackColor = Color.White;
                //   btnLabel.ForeColor = Color.Black;
            }
        }



        private void btnFailed_MouseEnter(object sender, EventArgs e)
        {
            if (Status != 2)
            {
                btnFailed.BackColor = Color.OrangeRed;
                //  btnLabel.ForeColor = Color.OrangeRed;
            }
        }

        private void btnFailed_MouseLeave(object sender, EventArgs e)
        {
            if (Status != 2)
            {
                btnFailed.BackColor = Color.White;
                // btnLabel.ForeColor = Color.Black;
            }
        }

        private void btnFailed_Leave(object sender, EventArgs e)
        {

        }



        public void UserControlCheckFailed_Load(object sender, EventArgs e)
        {
            lblTaskName.Text = TaskName;
            UpdateColors();

            if (TaskName == "SERVICED")
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        if (Form1.dropBoxCount > 1)
                        {
                            Status = 1;
                            UpdateTaskStatus();
                        }
                        else
                        {
                            Status = 2;
                            UpdateTaskStatus();
                        }
                        
                        if(Form1.IsLoaded)
                            break;
                        
                        System.Threading.Thread.Sleep(1000);
                    }
                });
            }
        }

        private void btnPassed_Click(object sender, EventArgs e)
        {
            if (Status != 1)
            {
                Status = 1;
                UpdateTaskStatus();
            }
            else
            {
                Status = 0;
                UpdateTaskStatus();
            }
        }

        private void btnFailed_Click(object sender, EventArgs e)
        {
            if (Status != 2)
            {
                Status = 2;
                UpdateTaskStatus();
            }
            else
            {
                Status = 0;
                UpdateTaskStatus();
            }
        }

        void UpdateColors()
        {

            if (Status == 0)
            {

                btnPassed.BackColor = ContrastColor.ButtonActiveColor;
                btnFailed.BackColor = ContrastColor.ButtonActiveColor;
                lblTaskName.BackColor = ContrastColor.ButtonActiveColor;
                lblTaskName.ForeColor = ContrastColor.UserControlLabelActiveColor;
            }
            else if (Status == 1)
            {
                btnPassed.BackColor = Color.LimeGreen;
                btnFailed.BackColor = Color.White;
                lblTaskName.BackColor = Color.LightGreen;
            }
            else if (Status == 2)
            {
                btnPassed.BackColor = Color.White;
                btnFailed.BackColor = Color.OrangeRed;
                lblTaskName.BackColor = Color.OrangeRed;
            }

        }

        void UpdateTaskStatus()
        {
            LastEventDate = DateTime.Now;
            UpdateColors();
            Form1.UpdateTaskChange(TaskName, Status);
        }

        ToolTip tt = new ToolTip();

        private void lblTaskName_MouseHover(object sender, EventArgs e)
        {

        }

        private void lblTaskName_MouseEnter(object sender, EventArgs e)
        {
            if (LastEventDate.Date.Year != 1901)
            {
                tt.ToolTipTitle = "Last changed date: ";
                tt.InitialDelay = 0;
                tt.IsBalloon = true;
                tt.UseAnimation = true;
                tt.BackColor = Color.DodgerBlue;
                tt.ForeColor = Color.White;
                tt.SetToolTip(lblTaskName, LastEventDate.ToString());
                if (TaskName == "BIOS UPDATE")
                {
                    tt.ToolTipTitle = "Version: ";
                    tt.SetToolTip(lblTaskName, Form1.BIOSVersion);
                }
            }
        }

        private void lblTaskName_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                tt.Hide(lblTaskName);
            }
            catch { }
        }

        private void lblTaskName_Click(object sender, EventArgs e)
        {

        }
    }
}
