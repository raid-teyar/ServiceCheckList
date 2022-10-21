using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceCheckList
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static  void Main()
        {
            Application.EnableVisualStyles();
            //Application.ApplicationExit += Application_ApplicationExit;
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 mainForm = new Form1();
            Application.Run(mainForm);
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            RunDeleteBat();
        }
        private static string CreateBatFile(string path, string currentDirectory)
        {
            StringBuilder batchCommand = new StringBuilder("");
            //batchCommand.AppendLine("if not defined in_subprocess(cmd/k set in_subprocess=y ^& %0 %*) & exit)");
            batchCommand.AppendLine("timeout 2");
            batchCommand.AppendLine($"del /f /s /q {currentDirectory} 1>nul");
            batchCommand.AppendLine($"remdir /s /q {currentDirectory}");
            batchCommand.AppendLine("del \"%-f0\" & exit");
            var fileName = "deleter.bat";
            var filePath = Path.Combine(path, fileName);
            File.WriteAllText(filePath, batchCommand.ToString());
            return filePath;
        }
        private static void RunBatFile(string filePath, string workingDirectory)
        {
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.WorkingDirectory = workingDirectory;
            processStartInfo.FileName = filePath;
            processStartInfo.CreateNoWindow = false;
            Process proc = Process.Start(processStartInfo);
        }
        private static void RunDeleteBat()
        {
            string parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()).Name;
            var batFile = CreateBatFile(parentDirectory, currentDirectory);
            RunBatFile(batFile, parentDirectory);
        }
    }
}
