using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Web.Security;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using ServiceCheckList.Properties;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Xml;
using System.Management;

namespace ServiceCheckList
{
    public partial class Form1 : Form
    {
        public string ComputerName { get; set; }
        public static string BIOSVersion { get; set; }
        public string MakeProp { get; set; }
        public string ModelProp { get; set; }
        public string SerialNumber { get; set; }
        public static bool IsSSD { get; set; }
        public static bool IsActivated { get; set; }
        public static List<Control> ControlsList = new List<Control>();
        public static int dropBoxCount = 0;
        public static bool IsLoaded { get; set; } = false;

        public Form1()
        {
            InitializeComponent();
            var args = Environment.GetCommandLineArgs();
            if (args?.Length > 0)
            {
                WriteFileLog = args.LastOrDefault().ToLower() == "y";
            }

            if (!WriteFileLog && Application.ExecutablePath.ToLower().EndsWith("-log.exe"))
            {
                WriteFileLog = true;
            }
            //WriteFileLog = writeLogFile;

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(rbNotes, "Check Services");

            InitTask().ContinueWith((r) =>
            {
                if (r.IsFaulted)
                {
                    MessageBox.Show(r.Exception.ToString());
                }

                Invoke(new Action(() =>
                {
                    serialBox.Text = SerialNumber;
                    tbMake.Text = MakeProp;
                    tbModel.Text = ModelProp;
                }));

            });
        }

        private async Task InitTask()
        {
            await Task.Run(() =>
            {

                string output8 = PowerShellHandler.Command("Start-Service -Name 'ScreenConnect Client (fcee6fd16678952d)'");

                ManagementClass objInst = new ManagementClass("Win32_Bios");

                objInst.Get();
                foreach (ManagementObject obj in objInst.GetInstances())
                {
                    BIOSVersion = obj["SMBIOSBIOSVersion"].ToString();
                }
                objInst.Dispose();

                // checking if the windows is activated
                IsActivated = OsChecker.IsGenuineWindows();

                // setting Agent Installed
                string output5 = PowerShellHandler.Command("Get-Service -Name 'ScreenConnect Client (fcee6fd16678952d)'");
                List<Control> controls = GetAll(this, typeof(UserControlCheck)).ToList();

                foreach (Control control in controls)
                {
                    UserControlCheck userControl = (UserControlCheck)control;
                    if (userControl.TaskName == "AGENT INSTALLED")
                    {
                        if (output5.Contains("Running"))
                        {
                            userControl.Status = 1;
                            userControl.UpdateTaskStatus();
                        }
                        else
                        {
                            userControl.Status = 0;
                            userControl.UpdateTaskStatus();
                        }
                    }
                }

                // checking if the Os drive is of type SSD 
                string output2 = PowerShellHandler.Command("Get-PhysicalDisk | Select FriendlyName, MediaType");
                string trimmedString = GetLine(output2, 3);
                IsSSD = trimmedString.ToLower().Contains("ssd");
                controls = GetAll(this, typeof(UserControlCheckFailedHalfSize)).ToList();

                foreach (Control control in controls)
                {
                    UserControlCheckFailedHalfSize userControl = (UserControlCheckFailedHalfSize)control;
                    if (userControl.TaskName == "SSD")
                    {
                        if (IsSSD)
                        {
                            userControl.Status = 1;
                            userControl.UpdateTaskStatus();
                        }
                        else
                        {
                            userControl.Status = 0;
                            userControl.UpdateTaskStatus();
                        }
                    }
                }

                objInst = new ManagementClass("Win32_ComputerSystem");
                objInst.Get();

                foreach (ManagementObject obj in objInst.GetInstances())
                {
                    ModelProp = obj["Model"].ToString();
                    MakeProp = obj["Manufacturer"].ToString();
                    ComputerName = obj["Name"].ToString();
                }

                // when the pc is a desktop
                if (ModelProp.Contains("O.E.M.") || MakeProp.Contains("O.E.M."))
                {
                    MakeProp = "Custom Pc";
                    // get motherboard model
                    objInst.Dispose();
                    objInst = new ManagementClass("Win32_BaseBoard");
                    objInst.Get();

                    foreach (ManagementObject obj in objInst.GetInstances())
                    {
                        ModelProp = obj["Product"].ToString();
                        SerialNumber = obj["SerialNumber"].ToString();
                    }
                    
                    //string output = PowerShellHandler.Command("Get-WmiObject Win32_BaseBoard | Select Manufacturer, Product, SerialNumber");
                    //string[] lines = output.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    //string trimmedString1 = GetLine(output, 2);
                    //string trimmedString2 = GetLine(output, 3);
                    //string trimmedString3 = GetLine(output, 4);
                    //ModelProp = trimmedString1 + " " + trimmedString2;
                    //SerialNumber = trimmedString3;
                    
                    return;
                }

                objInst.Dispose();
                objInst = new ManagementClass("win32_bios");
                objInst.Get();
                foreach (ManagementObject obj in objInst.GetInstances())
                {
                    SerialNumber = obj["SerialNumber"].ToString();
                }
            });



        }

        // gets a line from a string
        string GetLine(string text, int lineNo)
        {
            string[] lines = text.Replace("\r", "").Split('\n');
            return lines.Length >= lineNo ? lines[lineNo - 1] : null;
        }

        static object locker = new object();

        public static Ticket LoadedTicket = new Ticket();
        public static DataClass Data = new DataClass();
        public static string DataFile = @"C:\Users\CSBYKP";
        public static string CCleanerFile = @"C:\Program Files\CCleaner\ccleaner.ini";
        public static string DeleteDirectory = "essential,kenny's toolkit";
        public static string AppStartUpPath = string.Empty;

        bool FormLoad = true;
        bool IsLoadUICall = false;
        bool IsRadioButtonChange = false;
        bool PasswordChanged = false;
        bool LoadGUI = false;

        //string SizeToIncrease = "*MEMTEST*EURO*BIOS UPDATE*CCLEANER*MALWAREBYTES*TWEAKING TOOL*TEAMVIEWER*AVG FREE*SFC / SCANNOW*DRIVERS*7ZIP*INTEL DRIVER UTILITY*";
        string SizeToIncrease =
            "*MEMTEST*EURO*CCLEANER*MALWAREBYTES*TWEAKING TOOL*INTEL DRIVER UTILITY*ADOBE READER*VLC PLAYER*WINDOWS UPDATES*RESET BROWSERS*ACTIVATED*AGENT INSTALLED*";

        string SizeToDecrease = "*MRI*SELF*";

        List<string> NamesList = new List<string>()
        {
            //"SERVICED", "MRI", "SELF", "DFT", "SSD", "MEMTEST", "ADWCLEANER", "CCLEANER", "DEFRAGGLER", "MALWAREBYTES", "CHKDSK", "TWEAKING TOOL", "POWER SETTINGS", "TEAMVIEWER", "ADOBE READER", "AVG FREE", "VLC PLAYER", "SFC / SCANNOW", "WINDOWS UPDATES", "BIOS UPDATE", "RESET BROWSERS", "DRIVERS", "ACTIVATED", "7ZIP"
            "SERVICED", "MRI", "MEMTEST", "DFT", "SSD", "SELF", "EURO", "ADWCLEANER", "CCLEANER",
            "INTEL DRIVER UTILITY", "MALWAREBYTES", "CHKDSK", "TWEAKING TOOL",
            "POWER SETTINGS", "ADOBE READER", "AVG FREE", "VLC PLAYER", "SFC / SCANNOW", "WINDOWS UPDATES",
            "BIOS UPDATE", "RESET BROWSERS", "DRIVERS", "ACTIVATED", "7ZIP", "AGENT INSTALLED"
        };

        List<string> NamesListNewInstall = new List<string>()
        {
            "SERVICED", "MRI", "MEMTEST", "DFT", "SSD", "SELF", "EURO", "POWER SETTINGS", "CCLEANER", "ADOBE READER",
            "MALWAREBYTES", "VLC PLAYER", "WINDOWS UPDATES", "AVG FREE", "GOOGLE CHROME", "BIOS UPDATE",
            "MICROSOFT OFFICE", "DRIVERS", "ACTIVATED", "7ZIP", "DATA MERGED", "INTEL DRIVER UTILITY"

//                "POWER SETTINGS"
//,"ADOBE READER"
//,"VLC PLAYER"
//,"WINDOWS UPDATES"
//,"GOOGLE CHROME"
//,"MICROSOFT OFFICE"
//,"ACTIVATED"
//,"DATA MERGED"
//,"CCLEANER"
//,"MALWAREBYTES"
//,"TEAMVIEWER"
//,"AVG FREE"
//,"BIOS UPDATE"
//,"DRIVERS"
//,"7ZIP"
//,"INTEL DRIVER UTILITY"
        };


        static void SetAttrNormal()
        {
            if (File.Exists(DataFile))
            {
                try
                {
                    File.SetAttributes(DataFile, FileAttributes.Normal);
                }
                catch
                {
                }
            }
        }

        static void SetAttrHidden()
        {
            if (File.Exists(DataFile))
            {
                try
                {
                    File.SetAttributes(DataFile, FileAttributes.Hidden);
                }
                catch
                {
                }
            }
        }

        static void WriteDataFile()
        {
            try
            {
                lock (locker)
                {
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                    SetAttrNormal();

                    string data = serializer.Serialize(Data);
                    File.WriteAllText(DataFile, data);

                    SetAttrHidden();
                }
            }
            catch (Exception exp)
            {
                LogException(exp);
            }
        }

        private void DeleteFile()
        {
            try
            {
                if (File.Exists(DataFile))
                {
                    SetAttrNormal();
                    File.Delete(DataFile);
                }
            }
            catch
            {
            }
        }

        static void ReadDataFile()
        {
            try
            {
                LogInfo("ReadDataFile");
                if (Data == null)
                    LogInfo("ReadDataFile Data is null");

                lock (locker)
                {
                    Data = new DataClass();
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                    SetAttrNormal();

                    Data = serializer.Deserialize<DataClass>(File.ReadAllText(DataFile));

                    SetAttrHidden();
                }
            }
            catch (Exception exp)
            {
                LogInfo("ReadDataFile - Error");
                LogException(exp);
                throw;
            }
            finally
            {
                if (Data == null)
                    LogInfo("ReadDataFile Completed Data still null. Issue in Deserialize<DataClass>");
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //  TopMost = true;

            //tabControl1.SelectedTab = tbNewInstall;
            SystemEvents.UserPreferenceChanged +=
                this.SystemEvents_UserPreferenceChanged;
            KillProcessByName("ccleaner,ccleaner64,ccleaner.exe,ccleaner64.exe");
            if (!Directory.Exists(Path.GetDirectoryName(CCleanerFile)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(CCleanerFile));
            }

            File.WriteAllText(CCleanerFile, Properties.Resources.CCleanerINI);

            //DeleteApp(new ThreadParameter() { ProcessName = Process.GetCurrentProcess().ProcessName, Path = Application.ExecutablePath });
        }

        private void KillProcessByName(string name)
        {
            try
            {
                var allProcess = Process.GetProcesses().ToList();
                foreach (Process proc in allProcess)
                {
                    if (name.Contains(proc.ProcessName.ToLower()))
                    {
                        proc.Kill();
                        proc.WaitForExit(30);
                    }
                }
            }
            catch (Exception exp)
            {
                LogException(exp);
            }
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                LogInfo("======================APP STARTED======================");
                if (File.Exists(DataFile))
                {
                    SetAttrNormal();
                    ReadDataFile();
                    if (Data == null || Data.Tickets == null)
                    {
                        LogInfo("Corrupt data file.");
                        DeleteFile();
                        LogInfo("Data File Deleted.");
                        Application.Restart();
                    }

                    SetAttrHidden();
                    LoadUI();
                }
                else
                {
                    Data = new DataClass();
                    Data.Startup = true;
                    RegisterStartup();
                    WriteDataFile();
                    SetAttrHidden();
                    Application.Restart();
                }


                string StartupPath = @"C:\Users\" + Path.GetFileName(Application.ExecutablePath);
                string StartupValue = "";

                try
                {
                    using (var rk = Registry.LocalMachine.OpenSubKey(
                               @"SOFTWARE\WOW6432NODE\MICROSOFT\WINDOWS\CURRENTVERSION\RUN", true))
                    {
                        var keys = rk.GetValueNames().FirstOrDefault(k =>
                            k.ToUpper().Contains(Path.GetFileNameWithoutExtension(Application.ExecutablePath)
                                .ToUpper()));
                        StartupValue = rk.GetValue(keys).ToString();
                        //StartupValue = rk.GetValue(Path.GetFileNameWithoutExtension(Application.ExecutablePath)).ToString();
                    }
                }
                catch
                {
                }

                if (File.Exists(StartupPath))
                {
                    cbStartup.Checked = true;
                    Data.Startup = true;
                    WriteDataFile();
                }
                else
                {
                    if (Data.Startup)
                    {
                        RegisterStartup();
                    }
                }

                //lblTeamViewerId.Text = GetTeamViewerId(false);
                //if (lblTeamViewerId.Text == "Not Installed")
                //{
                //    timer1.Enabled = true;
                //    timer1.Start();
                //}

                TopMost = false;
                FormLoad = false;

                UpdateLoop();

                SetRadioButtons();

                SettingForHighContrastTheme();

                rdbCleanUp.Enabled = rdbNewInstallation.Enabled = !btnUnlockTicket.Enabled;
            }
            catch (Exception exp)
            {
                LogException(exp);
            }
            finally
            {
                LogInfo("======================SHOWN EVENT COMPLETED======================");
            }
        }

        private async void UpdateLoop()
        {
            await Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (LoadedTicket != null)
                    {
                        if (LoadedTicket.TicketDisabled)
                        {
                            if (LoadedTicket.TicketNo.Length > 0)
                            {
                                try
                                {
                                    Invoke((MethodInvoker)(() =>
                                    {
                                        try
                                        {
                                            ConnectTicket(true);
                                        }
                                        catch (WebException)
                                        {
                                        }
                                    }));
                                }
                                catch (ObjectDisposedException)
                                {
                                }
                            }
                        }
                    }

                    Thread.Sleep(5000);
                }
            });
        }


        void LoadUI()
        {
            try
            {
                LogInfo("Line: 227 - Start", true);
                cbPrevChecklists.Items.Clear();
                LogInfo("Line: 227 - cbPrevChecklists.Items.Clear");

                if (Data == null)
                    LogInfo("Data is null");
                if (Data?.Tickets == null)
                    LogInfo("Data.Tickets is null");
                else
                    LogInfo("Data.Ticketes Count is:" + Data.Tickets.Count);

                if (Data.Tickets.Count(x => x.Completed == false) > 0)
                {
                    LogInfo("Current Ticket Added - if");
                    cbPrevChecklists.Items.Add("Current Ticket");
                    cbPrevChecklists.SelectedIndex = 0;
                    LogInfo("Current Ticket Selected - if");
                }
                else
                {
                    LogInfo("Creating New Ticket");
                    LoadedTicket = CreateNewTicket();
                    LogInfo("Adding New Ticket");
                    Data.Tickets.Add(LoadedTicket);
                    cbStartup.Checked = true;
                    Data.Startup = true;
                    LogInfo("Register Startup");
                    RegisterStartup();
                    WriteDataFile();
                    LogInfo("Data Writtern - 1");
                    LogInfo("Current Ticket Added - else");
                    cbPrevChecklists.Items.Add("Current Ticket");
                    cbPrevChecklists.SelectedIndex = 0;
                    LogInfo("Current Ticket Selected - else");
                }

                IsLoadUICall = true;
                LogInfo("Loading previous tickets");
                foreach (var t in Data.Tickets.Where(x => x.Completed == true).OrderByDescending(x => x.Id))
                {
                    cbPrevChecklists.Items.Add(t.Name);
                }

                try
                {
                    LogInfo("Loading previous uncompleted ticketed and assinge to LoadedTicket:321");
                    LoadedTicket = Data.Tickets.OrderByDescending(x => x.Id).First(x => x.Completed == false);
                    LogInfo("Assinge LoadedTicket data");
                    LoadTicket(LoadedTicket);
                }
                catch (Exception exp)
                {
                    LogException(exp);
                }

                LogInfo("Assinge LoadedTicket data to controls");

                try
                {
                    tbTicketNo.Text = LoadedTicket.TicketNo;
                }
                catch (Exception exp)
                {
                    LogException(exp);
                }

                try
                {
                    tbPassword.Text = LoadedTicket.Password;
                }
                catch (Exception exp)
                {
                    LogException(exp);
                }

                try
                {
                    tbUserName.Text = LoadedTicket.UserName;
                }
                catch (Exception exp)
                {
                    LogException(exp);
                }

                try
                {
                    rbDropOffNotes.Text = LoadedTicket.DropOffNote.Text;
                }
                catch (Exception exp)
                {
                    LogException(exp);
                }
            }
            catch (Exception exp)
            {
                LogException(exp);
            }

            dropBoxCount = cbPrevChecklists.Items.Count;
            IsLoaded = true;
        }

        public static void UpdateTaskChange(string TaskName, int Status)
        {
            var t = LoadedTicket.Tasks.Find(x => x.TaskName == TaskName);
            t.EventDate = DateTime.Now;
            t.Status = Status;
            WriteDataFile();
        }


        Ticket CreateNewTicket()
        {
            LogInfo("CreateNewTicket - Started");
            Ticket ticket = new Ticket();
            Data.TicketsId++;
            ticket.Id = Data.TicketsId;
            ticket.Name = "";
            ticket.Note = new NoteClass();
            ticket.Note.Text = "";
            ticket.DropOffNote = new NoteClass();
            ticket.DropOffNote.Text = "";
            AddNewTasks(ticket);
            LogInfo("CreateNewTicket - Completed");

            return ticket;
        }

        private void AddNewTasks(Ticket ticket)
        {
            LogInfo("AddNewTasks - Started");

            try
            {
                HashSet<string> namesHash = new HashSet<string>();
                NamesList.ForEach(n => namesHash.Add(n));
                NamesListNewInstall.ForEach(n => namesHash.Add(n));
                LogInfo("Tasks List in progress");

                ticket.Tasks.ForEach(t =>
                {
                    if (t.TaskName == "DEFRAGGLER")
                    {
                        t.TaskName = "EURO";
                    }
                });

                var listToRemove = ticket.Tasks.Where(t => namesHash.Contains(t.TaskName)).Select(t => t.TaskName)
                    .ToList();
                listToRemove.ForEach(t => namesHash.Remove(t));

                foreach (var name in namesHash)
                {
                    TaskClass task = new TaskClass();
                    task.TaskName = name;

                    ticket.Tasks.Add(task);
                }

                LogInfo("Tasks List Generated - 1");
            }
            catch (Exception exp)
            {
                LogInfo("AddNewTasks - Error");
                LogException(exp);
            }

            LogInfo("AddNewTasks - Completed");
        }

        void LoadTicket(Ticket ticket, bool mergeOldNesTasks = false)
        {
            try
            {
                LogInfo("LoadTicket - Started");
                FormLoad = true;

                if (mergeOldNesTasks)
                    AddNewTasks(ticket);

                LoadedTicket = ticket;
                tbUserName.Text = ticket.UserName;
                tbPassword.Text = ticket.Password;
                tbTicketNo.Text = ticket.TicketNo;
                if (!IsRadioButtonChange)
                {
                    rdbCleanUp.Checked = !ticket.NewInstall;
                    rdbNewInstallation.Checked = ticket.NewInstall;
                }

                if (ticket.TicketDisabled)
                {
                    tbTicketNo.Enabled = false;
                    btnConnect.Enabled = false;
                    btnConnect.BackColor = Color.DimGray;
                    btnUnlockTicket.Enabled = true;
                }
                else
                {
                    tbTicketNo.Enabled = true;
                    btnConnect.Enabled = true;
                    btnConnect.BackColor = Color.FromKnownColor(KnownColor.Control);
                    btnUnlockTicket.Enabled = false;
                }

                rbNotes.Text = ticket.Note.Text;

                if (ticket.DropOffNote == null)
                {
                    ticket.DropOffNote = new NoteClass();
                    rbDropOffNotes.Text = ticket.DropOffNote.Text;
                }

                //flowLayoutPanel2.Controls.Clear();
                if (!IsLoadUICall)
                {
                    AddControls(ticket);
                }

                IsLoadUICall = false;
                //LoadUserControls(ticket, flowLayoutPanel2, NamesList);

                FormLoad = false;
            }
            catch (Exception exp)
            {
                LogInfo("LoadTicket - Error");
                LogException(exp);
            }
            finally
            {
                LogInfo("LoadTicket - Completed");
            }
        }

        private void AddControls(Ticket ticket)
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                if (!ticket.NewInstall)
                    LoadUserControls(ticket, flowLayoutPanel1, NamesList);
                else
                    LoadUserControls(ticket, flowLayoutPanel1, NamesListNewInstall);
            }
            catch (Exception exp)
            {
                LogException(exp);
                throw;
            }
        }

        private void LoadUserControls(Ticket ticket, FlowLayoutPanel panel, List<string> namesList)
        {
            try
            {
                foreach (TaskClass t in ticket.Tasks.Where(t => namesList.Contains(t.TaskName))
                             .OrderBy(x => namesList.IndexOf(x.TaskName)))
                {
                    //if (t.TaskName == "MRI" || t.TaskName == "SELF" || t.TaskName == "DFT" || t.TaskName == "SSD")
                    if (t.TaskName == "MRI" || t.TaskName == "MEMTEST" || t.TaskName == "DFT" || t.TaskName == "SSD" ||
                        t.TaskName == "SELF" || t.TaskName == "EURO")
                    {
                        UserControlCheckFailedHalfSize uc1 = new UserControlCheckFailedHalfSize();
                        uc1.TaskName = t.TaskName;
                        uc1.Status = t.Status;
                        uc1.LastEventDate =
                            (t.EventDate == null ? new DateTime(1901, 1, 1, 0, 0, 0) : t.EventDate.Value);
                        //if (!LoadedTicket.NewInstall)
                        if (SizeToIncrease.Contains("*" + t.TaskName + "*"))
                        {
                            uc1.Size = new Size(uc1.Size.Width + 15, uc1.Size.Height);
                            if (TaskNameLabledWidth == 0)
                                TaskNameLabledWidth = uc1.lblTaskName.Size.Width + 25;
                            uc1.lblTaskName.Size = new Size(TaskNameLabledWidth, uc1.lblTaskName.Size.Height);
                        }
                        else if (SizeToDecrease.Contains("*" + t.TaskName + "*"))
                        {
                            uc1.Size = new Size(uc1.Size.Width - 10, uc1.Size.Height);
                        }

                        panel.Controls.Add(uc1);
                    }
                    //else if (t.TaskName == "SERVICED" || t.TaskName == "MEMTEST" || t.TaskName == "BIOS UPDATE")
                    //else if (t.TaskName == "SERVICED" || t.TaskName == "DATA MERGED" || ((t.TaskName == "BIOS UPDATE" || t.TaskName == "INTEL DRIVER UTILITY") && !LoadedTicket.NewInstall))
                    else if (t.TaskName == "SERVICED" || t.TaskName == "DATA MERGED" || t.TaskName == "BIOS UPDATE" ||
                             t.TaskName == "INTEL DRIVER UTILITY")
                    {
                        UserControlCheckFailed uc1 = new UserControlCheckFailed();
                        uc1.TaskName = t.TaskName;
                        uc1.Status = t.Status;
                        uc1.LastEventDate =
                            (t.EventDate == null ? new DateTime(1901, 1, 1, 0, 0, 0) : t.EventDate.Value);
                        if (!LoadedTicket.NewInstall && t.TaskName == "INTEL DRIVER UTILITY")
                            panel.Controls.Add(uc1);
                        else
                        {
                            if (SizeToIncrease.Contains("*" + t.TaskName + "*"))
                                uc1.Size = new Size(uc1.Size.Width + 15, uc1.Size.Height);
                            panel.Controls.Add(uc1);
                        }
                    }
                    else
                    {
                        UserControlCheck uc2 = new UserControlCheck();
                        uc2.TaskName = t.TaskName;
                        uc2.Status = t.Status;
                        uc2.LastEventDate =
                            (t.EventDate == null ? new DateTime(1901, 1, 1, 0, 0, 0) : t.EventDate.Value);
                        //if (!LoadedTicket.NewInstall)
                        if (SizeToIncrease.Contains("*" + t.TaskName + "*"))
                            uc2.Size = new Size(uc2.Size.Width + 15, uc2.Size.Height);
                        panel.Controls.Add(uc2);
                    }
                }
            }
            catch (Exception exp)
            {
                LogException(exp);
                throw;
            }
        }

        private void RegisterStartup()
        {
            LogInfo("RegisterStartup - Started");
            string me = AppDomain.CurrentDomain.BaseDirectory + Path.GetFileName(Application.ExecutablePath);
            string StartupPath = @"C:\Users\" + Path.GetFileName(Application.ExecutablePath);

            if (me != StartupPath)
            {
                try
                {
                    File.Delete(StartupPath);
                    File.Copy(me, StartupPath);
                }
                catch
                {
                }
            }

            try
            {
                using (var rk = Registry.LocalMachine.OpenSubKey(
                           @"SOFTWARE\WOW6432NODE\MICROSOFT\WINDOWS\CURRENTVERSION\RUN", true))
                {
                    rk.SetValue(Path.GetFileNameWithoutExtension(Application.ExecutablePath), StartupPath);
                }
            }
            catch
            {
                try
                {
                    using (var rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\MICROSOFT\WINDOWS\CURRENTVERSION\RUN",
                               true))
                    {
                        rk.SetValue(Path.GetFileNameWithoutExtension(Application.ExecutablePath), StartupPath);
                    }
                }
                catch
                {
                }
            }


            try
            {
                File.SetAttributes(StartupPath, FileAttributes.Hidden);
            }
            catch
            {
            }

            LogInfo("RegisterStartup - Completed");
        }

        private void RemoveFromStartup()
        {
            string me = AppDomain.CurrentDomain.BaseDirectory + Path.GetFileName(Application.ExecutablePath);
            //string StartupPath = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\" + Path.GetFileName(Application.ExecutablePath);
            string StartupPath = @"C:\Users\" + Path.GetFileName(Application.ExecutablePath);

            try
            {
                File.SetAttributes(StartupPath, FileAttributes.Normal);
            }
            catch
            {
            }

            try
            {
                using (var rk = Registry.LocalMachine.OpenSubKey(
                           @"SOFTWARE\WOW6432NODE\MICROSOFT\WINDOWS\CURRENTVERSION\RUN", true))
                {
                    rk.DeleteValue(Path.GetFileNameWithoutExtension(Application.ExecutablePath), false);
                }
            }
            catch
            {
                try
                {
                    using (var rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\MICROSOFT\WINDOWS\CURRENTVERSION\RUN",
                               true))
                    {
                        rk.DeleteValue(Path.GetFileNameWithoutExtension(Application.ExecutablePath), false);
                    }
                }
                catch
                {
                }
            }


            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = "/C choice /C Y /N /D Y /T 5 & Del " + StartupPath;
            Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.CreateNoWindow = true;
            Info.FileName = "cmd.exe";
            Process.Start(Info);

            Application.Exit();
        }


        private void btnRemoveStartup_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(tbTicketNo.Text))
            //{
            //    MessageBox.Show("Enter Ticket No to complete it.", "Ticket No Missing");
            //    tbTicketNo.Focus();
            //    return;
            //}
            if (MessageBox.Show("Are you sure the checklist is finished and everything on this ticket is done?",
                    "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LoadedTicket.Completed = true;
                LoadedTicket.Name = DateTime.Now.ToString("yyyy-MM-dd") + " (" + LoadedTicket.Id + ")";

                WriteDataFile();

                try
                {
                    ConnectTicket();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!\n\n" + ex.Message);
                }

                MessageBox.Show("Ticket marked as Completed!");
                DeleteDirectories();

                RemoveFromStartup();
            }

            //LoadUI();
        }

        private void cbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPrevChecklists.Text == "Current Ticket")
            {
                try
                {
                    LoadedTicket = Data.Tickets.OrderByDescending(x => x.Id).First(x => x.Completed == false);
                    LoadTicket(LoadedTicket, true);
                    if (SystemInformation.HighContrast)
                    {
                        SettingForHighContrastTheme();
                    }
                }
                catch
                {
                }
            }
            else
            {
                LoadTicket(Data.Tickets.Find(x => x.Name == cbPrevChecklists.Text), true);
            }
        }


        //string GetTeamViewerId(bool fromButton)
        //{
        //    try
        //    {
        //        return GetAnyDeskId();

        //        string regPath = Environment.Is64BitOperatingSystem
        //            ? @"SOFTWARE\Wow6432Node\TeamViewer"
        //            : @"SOFTWARE\TeamViewer";
        //        using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath))
        //        {
        //            if (key == null)
        //                return "Not Installed";

        //            object clientId = key.GetValue("ClientID");

        //            if (clientId != null)
        //                return clientId.ToString();

        //            foreach (string subKeyName in key.GetSubKeyNames().Reverse())
        //            {
        //                clientId = key.OpenSubKey(subKeyName).GetValue("ClientID");

        //                if (clientId != null)
        //                    return clientId.ToString();
        //            }

        //            return "Not Installed";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        if (fromButton)
        //            MessageBox.Show(e.Message);

        //        return "Not Installed";
        //    }
        //}

        //string GetAnyDeskId()
        //{
        //    var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
        //        "AnyDesk", "system.conf");
        //    if (File.Exists(filePath))
        //    {
        //        var key = System.IO.File.ReadAllLines(filePath).Where(l => l.ToLower().StartsWith("ad.anynet.id="))
        //            .FirstOrDefault().Split('=').LastOrDefault();
        //        if (!string.IsNullOrWhiteSpace(key))
        //        {
        //            return key;
        //        }
        //    }

        //    return "Not Installed";
        //}

        private void rbNotes_TextChanged(object sender, EventArgs e)
        {
            if (FormLoad)
                return;

            LoadedTicket.Note = new NoteClass();
            LoadedTicket.Note.Text = rbNotes.Text;
            LoadedTicket.Note.LastEventDate = DateTime.Now;
            WriteDataFile();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                LoadedTicket.TicketNo = tbTicketNo.Text;
                var isNewInstallation = rdbNewInstallation.Checked;
                WriteDataFile();

                ConnectTicket();

                PasswordChanged = false;

                LoadedTicket.TicketDisabled = true;
                tbTicketNo.Enabled = false;
                btnConnect.BackColor = Color.DimGray;
                btnConnect.Enabled = false;
                btnUnlockTicket.Enabled = true;

                FormLoad = true;
                rdbCleanUp.Checked = !isNewInstallation;
                rdbNewInstallation.Checked = isNewInstallation;
                LoadedTicket.NewInstall = isNewInstallation;
                AddControls(LoadedTicket);
                FormLoad = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!\n\n" + ex.Message);
            }
        }

        void ConnectTicket(bool IsItrativeCall = false)
        {
            string macAddr = "";

            try
            {
                macAddr = (
                    from nic in NetworkInterface.GetAllNetworkInterfaces()
                    where nic.OperationalStatus == OperationalStatus.Up
                    select nic.GetPhysicalAddress().ToString()
                ).FirstOrDefault();
            }
            catch
            {
            }


            var TaskList = LoadedTicket.Tasks;


            int serviced = 0;
            int mri = 0;
            int dft = 0;
            int self = 0;
            int memtest = 0;
            int ssd = 0;
            int adw = 0;
            int ccleaner = 0;
            int defraggler = 0;
            int malwarebytes = 0;
            int chkdsk = 0;
            int tweaking = 0;
            int power = 0;
            int teamviewer = 0;
            int adobe = 0;
            int avg = 0;
            int vlc = 0;
            int sfc = 0;
            int updates = 0;
            int bios = 0;
            int reset = 0;
            int drivers = 0;
            int activated = 0;
            int sevenzip = 0;

            int chrome = 0;
            int office = 0;
            int datamerged = 0;
            int inteldu = 0;
            int newinstall = LoadedTicket.NewInstall ? 1 : 0;

            // new fields
            int euro = 0;
            int agentinstalled = 0;
            string cname = "";
            string bv = "";
            string mfg = "";
            string model = "";
            string sn = "";
            //string key = "";

            string password = "";
            string completed = "";

            try
            {
                serviced = TaskList.Find(x => x.TaskName == "SERVICED").Status;
            }
            catch
            {
            }

            try
            {
                mri = TaskList.Find(x => x.TaskName == "MRI").Status;
            }
            catch
            {
            }

            try
            {
                dft = TaskList.Find(x => x.TaskName == "DFT").Status;
            }
            catch
            {
            }

            try
            {
                self = TaskList.Find(x => x.TaskName == "SELF").Status;
            }
            catch
            {
            }

            try
            {
                memtest = TaskList.Find(x => x.TaskName == "MEMTEST").Status;
            }
            catch
            {
            }

            try
            {
                ssd = TaskList.Find(x => x.TaskName == "SSD").Status;
            }
            catch
            {
            }

            try
            {
                adw = TaskList.Find(x => x.TaskName == "ADWCLEANER").Status;
            }
            catch
            {
            }

            try
            {
                ccleaner = TaskList.Find(x => x.TaskName == "CCLEANER").Status;
            }
            catch
            {
            }

            try
            {
                malwarebytes = TaskList.Find(x => x.TaskName == "MALWAREBYTES").Status;
            }
            catch
            {
            }

            try
            {
                chkdsk = TaskList.Find(x => x.TaskName == "CHKDSK").Status;
            }
            catch
            {
            }

            try
            {
                tweaking = TaskList.Find(x => x.TaskName == "TWEAKING TOOL").Status;
            }
            catch
            {
            }

            try
            {
                power = TaskList.Find(x => x.TaskName == "POWER SETTINGS").Status;
            }
            catch
            {
            }

            try
            {
                adobe = TaskList.Find(x => x.TaskName == "ADOBE READER").Status;
            }
            catch
            {
            }

            try
            {
                avg = TaskList.Find(x => x.TaskName == "AVG FREE").Status;
            }
            catch
            {
            }

            try
            {
                vlc = TaskList.Find(x => x.TaskName == "VLC PLAYER").Status;
            }
            catch
            {
            }

            try
            {
                sfc = TaskList.Find(x => x.TaskName == "SFC / SCANNOW").Status;
            }
            catch
            {
            }

            try
            {
                updates = TaskList.Find(x => x.TaskName == "WINDOWS UPDATES").Status;
            }
            catch
            {
            }

            try
            {
                bios = TaskList.Find(x => x.TaskName == "BIOS UPDATE").Status;
            }
            catch
            {
            }

            try
            {
                reset = TaskList.Find(x => x.TaskName == "RESET BROWSERS").Status;
            }
            catch
            {
            }

            try
            {
                drivers = TaskList.Find(x => x.TaskName == "DRIVERS").Status;
            }
            catch
            {
            }

            try
            {
                agentinstalled = TaskList.Find(x => x.TaskName == "AGENT INSTALLED").Status;
            }
            catch
            {
            }

            try
            {
                activated = TaskList.Find(x => x.TaskName == "ACTIVATED").Status;
            }
            catch
            {
            }

            try
            {
                sevenzip = TaskList.Find(x => x.TaskName == "7ZIP").Status;
            }
            catch
            {
            }

            try
            {
                inteldu = TaskList.Find(x => x.TaskName == "INTEL DRIVER UTILITY").Status;
            }
            catch
            {
            }

            try
            {
                euro = TaskList.Find(x => x.TaskName == "EURO").Status;
            }
            catch
            {
            }

            try
            {
                chrome = TaskList.Find(x => x.TaskName == "GOOGLE CHROME").Status;
            }
            catch
            {
            }

            try
            {
                office = TaskList.Find(x => x.TaskName == "MICROSOFT OFFICE").Status;
            }
            catch
            {
            }

            try
            {
                datamerged = TaskList.Find(x => x.TaskName == "DATA MERGED").Status;
            }
            catch
            {
            }

            // setting new fields
            cname = ComputerName;
            bv = BIOSVersion;
            mfg = MakeProp;
            model = ModelProp;
            sn = SerialNumber;
            //key = KeyProp;


            if (PasswordChanged)
            {
                password = "&oldpass=" + LoadedTicket.Password + "&newpass=" + tbPassword.Text;
            }

            if (LoadedTicket.Completed)
            {
                completed = "&complete=1";
            }

            string data =
                "serviced=" + serviced +
                "&mri=" + mri +
                "&dft=" + dft +
                "&self=" + self +
                "&memtest=" + memtest +
                "&ssd=" + ssd +
                "&adw=" + adw +
                "&ccleaner=" + ccleaner +
                "&defraggler=" + defraggler +
                "&malwarebytes=" + malwarebytes +
                "&chkdsk=" + chkdsk +
                "&tweaking=" + tweaking +
                "&power=" + power +
                "&teamviewer=" + teamviewer +
                "&adobe=" + adobe +
                "&avg=" + avg +
                "&vlc=" + vlc +
                "&sfc=" + sfc +
                "&updates=" + updates +
                "&bios=" + bios +
                "&reset=" + reset +
                "&drivers=" + drivers +
                "&activated=" + activated +
                "&7zip=" + sevenzip +
                "&mac=" + macAddr +
                "&note=" + LoadedTicket.Note.Text +
                "&inteldu=" + inteldu +
                "&chrome=" + chrome +
                "&office=" + office +
                "&datamerged=" + datamerged +
                "&newinstall=" + newinstall +
                "&isa=" + agentinstalled +
                "&euro=" + euro +
                "&cname=" + cname +
                "&bv=" + bv +
                "&mfg=" + mfg +
                "&mdl=" + model +
                "&sn=" + sn +
                "&ticket_id=" + tbTicketNo.Text + password + completed +
                "&syskey=" + LoadedTicket.SysKey;

            // Process.Start("http://www.csbykp.com/tickets/link.php?" + data);


            //http://www.csbykp.com/tickets/link.php?serviced=2&mri=0&dft=0&self=0&memtest=0&ssd=1&adw=0&ccleaner=0&defraggler=0&malwarebytes=0&chkdsk=0&tweaking=0&power=0&teamviewer=0&adobe=0&avg=0&vlc=0&sfc=0&updates=0&bios=0&reset=0&drivers=0&activated=1&7zip=0&mac=125B5FB36E11&note=&inteldu=0&chrome=0&office=0&datamerged=0&newinstall=0&agentinstalled=1&euro=0&cname=ServiceCheckList&bv=1.19.0&mfg=Dell Inc.&model=Inspiron 3580&sn=2TVRTW2&ticket_id=15516

            string content = "";
            if (string.IsNullOrWhiteSpace(LoadedTicket.TicketNo))
            {
                return;
            }

            using (WebClient wc = new WebClient())
            {
                content = wc.DownloadString("https://www.csbykp.com/tickets/link.php?" + data);
            }

            try
            {
                string name = Regex.Match(content, "\"name\":\".*?\"").Value;
                name = Regex.Matches(name, "\".*?\"")[1].Value.Replace("\"", "");
                tbUserName.Text = name;
                LoadedTicket.UserName = name;
            }
            catch
            {
            }

            try
            {
                string notes = Regex.Match(content, "\"notes\":\".*?\"").Value;
                notes = Regex.Matches(notes, "\".*?\"")[1].Value.Replace("\"", "");
                rbDropOffNotes.Text = notes;
                LoadedTicket.DropOffNote.Text = notes;
            }
            catch
            {
            }

            try
            {
                string pw = Regex.Match(content, "\"password\":\".*?\"").Value;
                pw = Regex.Matches(pw, "\".*?\"")[1].Value.Replace("\"", "");

                if (!PasswordChanged)
                {
                    tbPassword.Text = pw;
                    LoadedTicket.Password = tbPassword.Text;
                    WriteDataFile();
                }
            }
            catch
            {
            }

            try
            {
                string sk = Regex.Match(content, "\"syskey\":\".*?\"").Value;
                sk = Regex.Matches(sk, "\".*?\"")[1].Value.Replace("\"", "");
                LoadedTicket.SysKey = sk;
            }
            catch
            {
            }

            try
            {
                if (!IsItrativeCall)
                {
                    var cleanUp =
                        content.Substring(content.LastIndexOf("\"newinstall\":")).Replace("\"newinstall\":", "")[0]
                            .ToString();
                    var newInstall = 0;
                    int.TryParse(cleanUp, out newInstall);
                    FormLoad = true;
                    LoadedTicket.NewInstall = newinstall == 1;
                    if (!IsRadioButtonChange)
                    {
                        rdbCleanUp.Checked = newInstall == 0;
                        //rdbNewInstallation.Checked = newInstall == 1;
                        //AddControls(LoadedTicket);
                    }

                    FormLoad = false;
                }
            }
            catch
            {
            }
        }

        private void cbStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (FormLoad)
            {
                FormLoad = false;
                return;
            }

            if (cbStartup.Checked)
            {
                RegisterStartup();
                Data.Startup = true;
                WriteDataFile();
            }
            else
            {
                RemoveFromStartup();
                Data.Startup = false;
                WriteDataFile();
            }
        }


        ToolTip tt = new ToolTip();
        private static bool RunExplorer;

        public int TaskNameLabledWidth { get; private set; } = 0;
        public static bool WriteFileLog { get; private set; } = false;

        private void rbNotes_MouseEnter(object sender, EventArgs e)
        {
            var currentTicket = Data.Tickets.Find(x => x.Completed == false);

            if (currentTicket != null)
            {
                if (currentTicket.Note.LastEventDate.Date.Year != 1901)
                {
                    tt.ToolTipTitle = "Notes added: ";
                    tt.InitialDelay = 0;
                    tt.IsBalloon = true;
                    tt.UseAnimation = true;
                    tt.BackColor = Color.DodgerBlue;
                    tt.ForeColor = Color.White;
                    tt.SetToolTip(rbNotes, currentTicket.Note.LastEventDate.ToString());
                }
            }
        }

        private void rbNotes_Leave(object sender, EventArgs e)
        {
        }

        //private void lblTeamViewerId_Click(object sender, EventArgs e)
        //{
        //    GetTeamViewerId(true);
        //}

        //private void label4_Click(object sender, EventArgs e)
        //{
        //    GetTeamViewerId(true);
        //}

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void tbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbPassword.Text != LoadedTicket.Password)
            {
                PasswordChanged = true;
            }
        }

        private void tbTicketNo_TextChanged(object sender, EventArgs e)
        {
            if (FormLoad)
                return;

            else
            {
                LoadedTicket.TicketNo = tbTicketNo.Text;
                WriteDataFile();
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            if (FormLoad)
                return;

            //LoadedTicket.UserName = tbUserName.Text;
            //WriteDataFile();
        }

        private void tbDropOffNotes_TextChanged(object sender, EventArgs e)
        {
            if (FormLoad)
                return;

            //LoadedTicket.DropOffNote = new NoteClass();
            //LoadedTicket.DropOffNote.Text = rbDropOffNotes.Text;
            //LoadedTicket.DropOffNote.LastEventDate = DateTime.Now;
            //WriteDataFile();
        }

        private void rbDropOffNotes_MouseEnter(object sender, EventArgs e)
        {
            //var currentTicket = Data.Tickets.Find(x => x.Completed == false);

            //if (currentTicket != null)
            //{
            //    if (currentTicket.DropOffNote.LastEventDate.Date.Year != 1901)
            //    {
            //        tt.ToolTipTitle = "Drop Off Notes added: ";
            //        tt.InitialDelay = 0;
            //        tt.IsBalloon = true;
            //        tt.UseAnimation = true;
            //        tt.BackColor = Color.DodgerBlue;
            //        tt.ForeColor = Color.White;
            //        tt.SetToolTip(rbDropOffNotes, currentTicket.DropOffNote.LastEventDate.ToString());
            //    }
            //}
        }

        private void tbTicketNo_Click(object sender, EventArgs e)
        {
            //if (!tbTicketNo.Enabled)
            //{
            //    LoadedTicket.TicketDisabled = false;
            //    tbTicketNo.Enabled = true;
            //}
        }

        private void btnUnlockTicket_Click(object sender, EventArgs e)
        {
            LoadedTicket.TicketDisabled = false;
            tbTicketNo.Enabled = true;
            btnConnect.Enabled = true;
            btnConnect.BackColor = Color.Lime;
            btnUnlockTicket.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (LoadedTicket != null)
                if (LoadedTicket.TicketDisabled == true)
                    if (LoadedTicket.TicketNo.Length > 0)
                        try
                        {
                            ConnectTicket();
                        }
                        catch (WebException)
                        {
                        }
        }

        //private void btnNew_Click(object sender, EventArgs e)
        //{
        //    if (Data.Tickets.Count(x => x.Completed == false) == 0)
        //    {
        //        LoadedTicket = CreateNewTicket();
        //        Data.Tickets.Add(LoadedTicket);
        //        WriteDataFile();

        //        LoadUI();

        //        RegisterStartup();
        //    }
        //    else
        //    {
        //        MessageBox.Show("You still have an open ticket.", "New ticket creation failed.");
        //    }
        //}

        // Make sure the appropriate colors are shown following a change in
        // the state of high contrast.

        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            SettingForHighContrastTheme();
        }


        private void SettingForHighContrastTheme()
        {
            // Is a high contrast theme currently active?
            var allLabels = GetAll(this, typeof(Label));

            foreach (Label lbl in allLabels)
            {
                lbl.ForeColor = ContrastColor.LabelActiveColor;
            }

            //var allButton = GetAll(this, typeof(Button));

            //foreach (Button btn in allButton)
            //{
            //    if (btn.Name == "btnPassed")
            //    {
            //        btn.ForeColor = ContrastColor.LabelActiveColor;
            //        if (SystemInformation.HighContrast)
            //        {
            //            btn.Text = "V";
            //        }
            //        else
            //            btn.Text = "";
            //    }
            //}


            foreach (TextBox tb in GetAll(this, typeof(TextBox)))
            {
                tb.BackColor = ContrastColor.ButtonActiveColor;
                tb.ForeColor = ContrastColor.LabelActiveColor;
            }

            foreach (RichTextBox tb in GetAll(this, typeof(RichTextBox)))
            {
                tb.BackColor = ContrastColor.ButtonActiveColor;
                tb.ForeColor = ContrastColor.LabelActiveColor;
            }


            cbPrevChecklists.BackColor = this.BackColor = rdbCleanUp.BackColor =
                rdbNewInstallation.BackColor = cbStartup.BackColor = Color.WhiteSmoke;
            cbPrevChecklists.ForeColor = this.ForeColor =
                rdbCleanUp.ForeColor = rdbNewInstallation.ForeColor = cbStartup.ForeColor = Color.Black;
            if (SystemInformation.HighContrast)
                if (!btnConnect.Enabled)
                {
                    btnConnect.BackColor = Color.Gray;
                    btnConnect.ForeColor = Color.Black;
                }
                else
                {
                    btnConnect.ForeColor = btnTicketCompleted.ForeColor;
                    btnConnect.BackColor = btnTicketCompleted.BackColor;
                }

            fileToolStripMenuItem.BackColor = menuStrip1.BackColor = Color.WhiteSmoke;
            fileToolStripMenuItem.ForeColor = menuStrip1.ForeColor = Color.Black;
            this.Invalidate();
        }

        // used to get controls for UI color change
        public static IEnumerable<Control> GetAll(Control control, Type type)
        {
            try
            {
                var controls = control.Controls.Cast<Control>();

                ControlsList = controls.SelectMany(ctrl => GetAll(ctrl, type))
                    .Concat(controls)
                    .Where(c => c.GetType() == type).ToList();
                return ControlsList;
            }
            catch (Exception)
            {
                return new List<Control>();
            }
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (lblTeamViewerId.Text == "Not Installed")
        //    {
        //        var id = GetTeamViewerId(false);
        //        if (!string.IsNullOrEmpty(id) && id != "Not Installed")
        //        {
        //            lblTeamViewerId.Text = id;
        //            timer1.Enabled = false;
        //            timer1.Stop();
        //        }
        //    }
        //}


        private void rdbNewInstallation_CheckedChanged(object sender, EventArgs e)
        {
            if (FormLoad)
                return;
            //if (rdbNewInstallation.Checked)
            //{

            //}
            //else
            //{

            //}
            LoadedTicket.NewInstall = !rdbCleanUp.Checked;
            //To Keep track of Don't change radio button value from LoadTicket
            IsRadioButtonChange = true;

            LoadUI();
            //WriteDataFile();
            if (SystemInformation.HighContrast)
            {
                SettingForHighContrastTheme();
            }

            IsRadioButtonChange = false;
        }

        private void btnUnlockTicket_EnabledChanged(object sender, EventArgs e)
        {
            SetRadioButtons();
        }

        private void SetRadioButtons()
        {
            rdbCleanUp.Enabled = rdbNewInstallation.Enabled = !btnUnlockTicket.Enabled;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        // opens the dxDiag window
        private void dxDiagDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DxDiagDetails() { StartPosition = FormStartPosition.CenterParent }.ShowDialog(this);
        }

        // runs all .exe files that are in the app base directory
        private void runAllProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var files = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.exe",
                    SearchOption.TopDirectoryOnly);

                var x64Version = "64";
                var x32Version = "32";

                if (Environment.Is64BitOperatingSystem)
                    files = files.Where(f =>
                        !f.Contains(x32Version) &&
                        !Path.GetFileNameWithoutExtension(f).ToUpper().Contains("SERVICECHECKLIST")).ToArray();
                else
                    files = files.Where(f =>
                        !f.Contains(x64Version) &&
                        !Path.GetFileNameWithoutExtension(f).ToUpper().Contains("SERVICECHECKLIST")).ToArray();


                foreach (var file in files)
                {
                    //if (Path.GetFileNameWithoutExtension(file).ToUpper().Contains("SERVICECHECKLIST"))
                    //    continue;
                    //if (file.Contains(x64Version) || file.Contains(x32Version))
                    //{
                    //    if (file.Contains(x32Version) && Environment.Is64BitOperatingSystem)
                    //    {
                    //        continue;
                    //    }
                    //    else if (file.Contains(x64Version) && !Environment.Is64BitOperatingSystem)
                    //    {
                    //        continue;
                    //    }
                    //}

                    //Process.Start(file);
                    RunElevated(file);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        // runs PowerSettings.bat file that restarts explorer.exe process
        private void iConToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ExecuteCommand("echo testing");
                string file = GetFileName("PowerSettings", Resources.PowerSettings);
                RunExplorer = true;
                ExecuteCommand(file);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        // clean disk using command
        private void diskCleanupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string file = GetFileName("DiskCleanup", Resources.DiskCleanup);

                // TODO: might need to change this bool to true
                RunExplorer = false;

                ExecuteCommand(file);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        // downloads .exe from the web application and launchs it
        private void installAgentToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            string URL =
                "https://csbykp.screenconnect.com/Bin/ConnectWiseControl.ClientSetup.exe?h=instance-ec2xxz-relay.screenconnect.com" +
                "&p=443&k=BgIAAACkAABSU0ExAAgAAAEAAQBV7fDkG8Pve%2Fqk16W2FLzUhwHo%2FxYQRVWREHb4hwwYWDBObwLLFGeZUoBnNCWfQTDy2jOxQii4Wun" +
                "y6YkuVr4ik72vXbk6VFUqfNLDVzcK%2BnuZVHQo1UsZ4psbTvj8i76PRm6SBD8u4gTcsfJdW%2F3f9j0wVU0qGVhjZ2pWz7hR5%2B%2FoBqsTZKnJcZJGagW" +
                "li91jLQmQA8oXZeEgeDqWROZ33iVPTEsraWDIrOPY4qMmPOpA4LqyItUJtYIDBw9oeeIubkJrn6pXCTz2dFrOhhwpDEARDk36UP0hh4%2FHSMybO4zxVUXSbgQU" +
                "vkYGkS%2FnvJnrsWuITvVi9%2BsEA2A27h7Y&e=Access&y=Guest&t=TICKET%20%23$TICKET#$%20Customer%3A%20$FIRSTNAME%20$LASTNAME&c=In-St" +
                "ore%20Computers&c=&c=&c=&c=&c=&c=&c=";

            URL = URL.Replace("$TICKET#$", tbTicketNo.Text);
            URL = URL.Replace("$FIRSTNAME%20$LASTNAME", tbUserName.Text);

            WebClient client = new WebClient();

            string file = Path.Combine(Path.GetTempPath(), "Agent.exe");

            if (File.Exists(file))
            {
                try
                {
                    Process.Start(file);
                    return;
                }
                catch (Exception)
                {
                    File.Delete(file);
                }
            }

            client.DownloadFileAsync(new Uri(URL), file);

            client.DownloadProgressChanged += (o, args) =>
            {
                installAgentToolStripMenuItem.Text = "Downloading agent " + args.ProgressPercentage + "%";
            };

            client.DownloadFileCompleted += (o, args) =>
            {
                if (args.Error != null)
                {
                    MessageBox.Show(args.Error.Message);
                    return;
                }

                if (args.Cancelled)
                {
                    MessageBox.Show("Download cancelled");
                    return;
                }

                try
                {
                    Process.Start(Path.Combine(Path.GetTempPath(), "Agent.exe"));
                    installAgentToolStripMenuItem.Text = "Install Agent";
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }

                // to close the menu
                fileToolStripMenuItem.PerformClick();
            };
        }

        // get .bat script as a string using the recource name
        public static string GetFileName(string setting, string content)
        {
            var file = Path.GetTempPath() + "\\" + setting + ".bat";
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            File.WriteAllText(file, content);

            //if (setting == "PowerSettings")
            //    File.WriteAllText(file, Resources.PowerSettings);
            //else if (setting == "KillOpenProcess")
            //    File.WriteAllText(file, Resources.KillOpenProcess);
            //else if (setting == "DeleteShortCut")
            //    File.WriteAllText(file, Resources.DeleteShortCut);
            //else if(setting == "BiosVersion")
            //    File.WriteAllText(file, Resources.BiosVersion);

            return file;
        }

        public static string ExecuteCommand(string command)
        {
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;

            //processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo = new ProcessStartInfo(command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.EnableRaisingEvents = true;
            process.Exited += Process_Exited;
            process.WaitForExit();
            ////process.Exited += Process_Exited;
            //// *** Read the streams ***
            // Warning: This approach can lead to deadlocks, see Edit #2
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            exitCode = process.ExitCode;

            Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            Console.WriteLine("ExitCode: " + exitCode, "ExecuteCommand");
            return output;
            //process.Close();
        }

        private static void Process_Exited(object sender, EventArgs e)
        {
            if (RunExplorer)
            {
                string explorer = string.Format("{0}\\{1}", Environment.GetEnvironmentVariable("WINDIR"),
                    "explorer.exe");
                Process process = new Process();
                process.StartInfo.FileName = explorer;
                process.StartInfo.UseShellExecute = true;
                process.Start();
                RunExplorer = false;
            }
        }

        private void deleteShortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunExplorer = true;
            string file = GetFileName("DeleteShortCut",
                Properties.Resources.DeleteShortCut.Replace("%username%", Environment.UserName));
            ExecuteCommand(file);
        }

        private void killOpenProcessesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunExplorer = true;
            var command = Properties.Resources.KillOpenProcess.Replace("%username%", "%" + Environment.UserName + "%")
                .Replace("%userdomain%", "%" + Environment.UserDomainName + "%");
            string file = GetFileName("KillOpenProcess", command);
            ExecuteCommand(file);
        }

        private void RunElevated(string fileName)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.Verb = "runas";
            processInfo.FileName = fileName;
            try
            {
                Process.Start(processInfo);
            }
            catch (Win32Exception)
            {
                //Do nothing. Probably the user canceled the UAC window
            }
        }


        private void DeleteDirectories()
        {
            try
            {
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                var allDirectories = Directory.GetDirectories(desktopPath)
                    .Where(d => DeleteDirectory.Contains(Path.GetFileName(d).ToLower())).Select(d => d.ToLower());
                foreach (var diretory in allDirectories)
                {
                    var allFiles = Directory.GetFiles(diretory).Select(f => f.ToLower()).ToArray();
                    if (Path.GetFileName(diretory) == "essential" && allFiles.Any(f =>
                            Path.GetFileNameWithoutExtension(f).Contains("servicechecklist")))
                    {
                        if (allFiles.Any(f => f == Application.ExecutablePath.ToLower()))
                        {
                            AppStartUpPath = Application.ExecutablePath.ToLower();
                            DelteAll(diretory, allFiles);
                        }
                        else
                            Directory.Delete(diretory, true);
                    }
                    else if (Path.GetFileName(diretory) == "kenny's toolkit" ||
                             Path.GetFileName(diretory) == "kennys toolkit")
                    {
                        if (allFiles.Any(f => f == Application.ExecutablePath.ToLower()))
                        {
                            AppStartUpPath = Application.ExecutablePath.ToLower();
                            DelteAll(diretory, allFiles);
                        }
                        else
                            Directory.Delete(diretory, true);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Delete kenny's toolkit", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void DelteAll(string diretory, string[] allFiles)
        {
            foreach (var file in allFiles)
            {
                try
                {
                    if (file == Application.ExecutablePath.ToLower())
                    {
                        continue;
                    }

                    File.Delete(file);
                }
                catch (Exception exp)
                {
                    //throw;
                }
            }

            foreach (var dir in Directory.GetDirectories(diretory))
            {
                try
                {
                    Directory.Delete(dir, true);
                }
                catch (Exception)
                {
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AppStartUpPath))
            {
                Thread t1 = new Thread(new ParameterizedThreadStart(DeleteApp));
                //MessageBox.Show("Test");
                t1.Start(new ThreadParameter()
                { ProcessName = Process.GetCurrentProcess().ProcessName, Path = Application.ExecutablePath });
            }

            LogInfo("======================APP CLOSED======================");
        }

        private void P_Exited(object sender, EventArgs e)
        {
            //MessageBox.Show("Exited..." + Process.GetCurrentProcess().ProcessName);
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            Directory.Delete(AppStartUpPath);
        }


        private void DeleteApp(object obj)
        {
            try
            {
                SelfDestruct();

                //ProcessStartInfo psi = new ProcessStartInfo("cmd.exe",
                //    String.Format("/k {0} & {1} & {2}",
                //        "timeout /T 3 /NOBREAK >NUL",
                //        "rmdir /s /q \"" + AppStartUpPath + "\"",
                //        "exit"
                //    )
                //);

                //psi.UseShellExecute = false;
                //psi.CreateNoWindow = true;
                //psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                //var p = Process.Start(psi);
                //p.EnableRaisingEvents = true;
                //p.ErrorDataReceived += P_ErrorDataReceived;
                //p.OutputDataReceived += P_OutputDataReceived;
                //p.Exited += P_Exited;
                //p.WaitForExit();
                //System.Threading.Thread.Sleep(5000);
                //var obj1 = obj as ThreadParameter;
                //var sPath = obj1.Path;
                //var dir = Path.GetDirectoryName(sPath);
                //var proc = Process.GetProcesses().
                //                 Where(pr => pr.ProcessName.ToLower() == obj1.ProcessName.ToLower());

                //foreach (var process in proc)
                //{
                //    process.Kill();
                //}
                //System.Threading.Thread.Sleep(1000);
                //Directory.Delete(dir, true);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void SelfDestruct()
        {
            ////RunDeleteBat();
            //return;
            Process procDestruct = new Process();
            string strName = "destruct.bat";
            string strPath = Path.Combine(Path.GetTempPath(), strName);
            //string strPath = Path.Combine(Directory.GetCurrentDirectory(), strName);
            //MessageBox.Show(Application.StartupPath);
            string strExe = new FileInfo(Application.ExecutablePath)
                .Name;

            StreamWriter swDestruct = new StreamWriter(strPath);

            swDestruct.WriteLine("attrib \"" + strExe + "\"" +
                                 " -a -s -r -h");
            swDestruct.WriteLine(":Repeat");
            swDestruct.WriteLine("del " + "\"" + strExe + "\"");
            swDestruct.WriteLine("if exist \"" + strExe + "\"" +
                                 " goto Repeat");
            swDestruct.WriteLine("del \"" + strName + "\"");
            swDestruct.Close();

            //procDestruct.StartInfo.FileName = "destruct.bat";
            procDestruct.StartInfo.FileName = strPath;

            procDestruct.StartInfo.CreateNoWindow = true;
            procDestruct.StartInfo.UseShellExecute = false;

            try
            {
                //procDestruct.Start();
                var deleteDir = Path.Combine(Path.GetTempPath(), "DeleteDir.bat");
                swDestruct = new StreamWriter(deleteDir);
                //File.WriteAllText(@":Repeat
                //rd /q /s """ + Application.StartupPath + @""" 2>nul
                //IF EXIST """ + Application.StartupPath + @"""
                //goto :Repeat" + Environment.NewLine + "del \"" + Path.GetFileName(deleteDir) + "\"", deleteDir);
                //procDestruct.WaitForExit();

                swDestruct.WriteLine(@":Repeat 
                rd /q /s """ + Application.StartupPath + @""" 2>nul
                IF EXIST """ + Application.StartupPath + @"""
                goto :Repeat" + Environment.NewLine + "del \"" + Path.GetFileName(deleteDir) + "\"");
                swDestruct.Close();

                Process procDel = new Process();
                procDel.StartInfo.FileName = deleteDir;
                procDel.StartInfo.CreateNoWindow = true;
                procDel.StartInfo.UseShellExecute = false;
                procDel.Start();
            }
            catch (Exception)
            {
                //Close();
            }
        }

        private static void LogException(Exception exp)
        {
            if (exp == null || !WriteFileLog) return;
            var filePath = "ServiceCheckList.log";
            using (var writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Date Time:".PadRight(30) + DateTime.Now);
                writer.WriteLine("[ERROR] Message:".PadRight(30) + exp.Message);
                writer.WriteLine("Stack Trace:".PadRight(30) + exp.StackTrace);
                if (exp.InnerException != null)
                {
                    writer.WriteLine();
                    writer.WriteLine("====================INNER EXCEPTION====================");
                    writer.WriteLine();
                }
            }

            if (exp.InnerException != null)
                LogException(exp.InnerException);
        }

        private static void LogInfo(string info,
            bool writeTime = false,
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null)
        {
            if (!WriteFileLog) return;
            var filePath = "ServiceCheckList-info.log";
            using (var writer = new StreamWriter(filePath, true))
            {
                if (writeTime)
                    writer.WriteLine("Date Time:".PadRight(30) + DateTime.Now.ToString());
                writer.WriteLine("[INFO] Message:".PadRight(30) + info);
                if (lineNumber > 0)
                {
                    writer.WriteLine("[LINE NUMBER] :".PadRight(30) + lineNumber.ToString());
                }

                if (caller != null)
                {
                    writer.WriteLine("[CALLER] :".PadRight(30) + caller);
                }
            }
        }

        private static string CreateBatFile(string path, string currentDirectory)
        {
            try
            {
                StringBuilder batchCommand = new StringBuilder("");
                batchCommand.AppendLine("if not defined in_subprocess(cmd/k set in_subprocess=y ^& %0 %*) & exit)");
                batchCommand.AppendLine("timeout 2");
                batchCommand.AppendLine($"del /f /s /q {currentDirectory} 1>nul");
                batchCommand.AppendLine($"remdir /s /q {currentDirectory}");
                batchCommand.AppendLine("del \"%-f0\" & exit");
                var fileName = "deleter.bat";
                var filePath = Path.Combine(path, fileName);
                File.WriteAllText(filePath, batchCommand.ToString());
                return filePath;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static void RunBatFile(string filePath, string workingDirectory)
        {
            try
            {
                var processStartInfo = new ProcessStartInfo();
                processStartInfo.WorkingDirectory = workingDirectory;
                processStartInfo.FileName = filePath;
                processStartInfo.CreateNoWindow = false;
                Process proc = Process.Start(processStartInfo);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private static void RunDeleteBat()
        {
            string parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()).Name;
            var batFile = CreateBatFile(parentDirectory, currentDirectory);
            RunBatFile(batFile, parentDirectory);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
    }
}