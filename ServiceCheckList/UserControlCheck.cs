using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceCheckList.Properties;

namespace ServiceCheckList
{
    public partial class UserControlCheck : UserControl
    {
        public UserControlCheck()
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


        private void btnFailed_Leave(object sender, EventArgs e)
        {
        }


        private void UserControlCheckFailed_Load(object sender, EventArgs e)
        {
            lblTaskName.Text = TaskName;
            UpdateColors();

            switch (TaskName)
            {
                case "ACTIVATED":
                {
                    if (Form1.IsActivated)
                    {
                        Status = 1;
                        UpdateTaskStatus();
                    }

                    break;
                }
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
                lblTaskName.BackColor = ContrastColor.ButtonActiveColor;
                lblTaskName.ForeColor = ContrastColor.UserControlLabelActiveColor;
            }
            else
            {
                btnPassed.BackColor = Color.LimeGreen;
                lblTaskName.BackColor = Color.LightGreen;
            }
        }

        public void UpdateTaskStatus()
        {
            LastEventDate = DateTime.Now;
            UpdateColors();
            Form1.UpdateTaskChange(TaskName, Status);
        }

        ToolTip tt = new ToolTip();

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
            }
        }

        private void lblTaskName_Click(object sender, EventArgs e)
        {
            switch (TaskName)
            {
                case "CHKDSK":
                {
                    string file = Form1.GetFileName("CHKDSK", Resources.CHKDSK);

                    if (File.Exists(file))
                    {
                        try
                        {
                            string output = Form1.ExecuteCommand(file);
                            lblTaskName.Text = "CHKDSK (waiting for restart)";
                            Status = 1;
                            UpdateTaskStatus();
                        }
                        catch (Exception exception)
                        {
                            Text = exception.Message;
                        }
                    }

                    break;
                }
                case "DRIVERS":
                {
                    string file = Form1.GetFileName("Drivers", Resources.Drivers);

                    if (File.Exists(file))
                    {
                        string output = Form1.ExecuteCommand(file);
                    }

                    break;
                }
                case "WINDOWS UPDATES":
                {
                    string file = Form1.GetFileName("WindowsUpdates", Resources.WindowsUpdates);
                    if (File.Exists(file))
                    {
                        string output = Form1.ExecuteCommand(file);
                    }

                    break;
                }
            }
        }
    }
}