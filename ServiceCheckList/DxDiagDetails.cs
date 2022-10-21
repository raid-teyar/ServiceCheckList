using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceCheckList
{
    public partial class DxDiagDetails : Form
    {
        public DxDiagDetails()
        {
            InitializeComponent();
        }
        Dictionary<string, string> dictDxDiag = new Dictionary<string, string>();
        //DataSet dsDxDiag = new DataSet();
        private void DxDiagDetails_Load(object sender, EventArgs e)
        {
            //RunDxDiag();
            lblLoading.Visible = true;
            backgroundWorker1.RunWorkerAsync();
            SystemEvents.UserPreferenceChanged +=
                    this.SystemEvents_UserPreferenceChanged;
            SettingForHighContrastTheme();
            dataGridView1.AutoGenerateColumns = false;
            //dataGridView1.Columns.Add("Key", "Key Name");
            //dataGridView1.Columns.Add("Value", "Key Value");
            dataGridView1.Columns["Key"].DataPropertyName = "Key";
            dataGridView1.Columns["Value"].DataPropertyName = "Value";

            //dataGridView1.DataSource = GetDxDiag().Select(d => new { Key = d.Key, Value = d.Value }).ToList();

        }

        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            SettingForHighContrastTheme();
        }

        private void SettingForHighContrastTheme()
        {
            this.BackColor = Color.WhiteSmoke;
            this.dataGridView1.BackgroundColor = Color.White;
            this.dataGridView1.ForeColor = Color.Black;
            this.Invalidate();
        }


        public Dictionary<string, string> GetDxDiag()
        {
            try
            {

                //((obj.TotalPhysicalMemory / 1024) / 1024)

                string osName = string.Empty;
                System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("SELECT Caption,OSArchitecture,BuildNumber FROM Win32_OperatingSystem");
                foreach (System.Management.ManagementObject os in searcher.Get())
                {
                    var prop = os.Properties;
                    osName = os["Caption"].ToString();
                    osName += " " + os["OSArchitecture"].ToString();
                    osName += " Build(" + os["BuildNumber"].ToString() + ")";
                    break;
                }
                //osName += (Environment.Is64BitOperatingSystem ? " 64" : " 32") + "-bit";


                //select LoadPercentage from Win32_Processor
                string processor = string.Empty;
                searcher = new System.Management.ManagementObjectSearcher("SELECT Name FROM Win32_Processor");
                foreach (System.Management.ManagementObject os in searcher.Get())
                {
                    processor = os["Name"].ToString();
                    break;
                }
                string memory = (((new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024) / 1024) / 1000).ToString() + " GB";
                //Computer\HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\BIOS
                
                string regPath = Environment.Is64BitOperatingSystem ? @"HARDWARE\DESCRIPTION\System\BIOS" : @"HARDWARE\DESCRIPTION\System\BIOS";
                var dicKeysValues = new Dictionary<string, string>();


                var listKey = new List<string>();
                listKey.Add("BaseBoardManufacturer");
                listKey.Add("BaseBoardProduct");
                listKey.Add("SystemManufacturer");
                listKey.Add("SystemProductName");
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath))
                {
                    foreach (var item in listKey)
                    {
                        dicKeysValues.Add(item, key.GetValue(item).ToString());
                    }
                }

                listKey.Clear();
                listKey.Add("ReleaseId");
                listKey.Add("ProductName");
                //listKey.Add("Processor");
                //listKey.Add("Memory");
                regPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
                foreach (var item in listKey)
                {
                    dicKeysValues.Add(item, Registry.GetValue(regPath, item, "").ToString());
                }
                listKey.Clear();
                listKey.Add("Memory");
                listKey.Add("Processor");
                //foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
                //{
                //    foreach (var key in listKey)
                //    {
                //        dicKeysValues.Add(key, item[key].ToString());
                //    }
                //}
                foreach (var item in listKey)
                {
                    dicKeysValues.Add(item, "");
                }
                dicKeysValues["ProductName"] = osName;
                dicKeysValues["Memory"] = memory;
                dicKeysValues["Processor"] = processor;
                return dicKeysValues;

            }
            catch (Exception exp)
            {

                throw;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            dictDxDiag = GetDxDiag();
            //dsDxDiag = RunDxDiag();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblLoading.Visible = false;
            //dictDxDiag = GetDxDiag();
            //var keys = dictDxDiag.Keys.ToList();
            //var tableName = "SystemInformation";
            //if (dsDxDiag?.Tables.Count > 0 && dsDxDiag.Tables.Contains(tableName))
            //{
            //    try
            //    {
            //        dictDxDiag["ProductName"] = dsDxDiag.Tables[tableName].Rows[0]["OperatingSystem"].ToString();
            //        dictDxDiag["Memory"] = dsDxDiag.Tables[tableName].Rows[0]["Memory"].ToString();
            //        dictDxDiag["Processor"] = dsDxDiag.Tables[tableName].Rows[0]["Processor"].ToString();
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}

            dataGridView1.DataSource = dictDxDiag.Select(d => new { Key = d.Key, Value = d.Value }).ToList();

        }
    }
}
