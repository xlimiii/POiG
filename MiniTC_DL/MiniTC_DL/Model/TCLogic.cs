using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiniTC_DL.Model
{
    class TCLogic
    {

        public string[] Drives { get; set; }
        public List<string>[] Directories { get; set; }
        public List<string>[] Files { get; set; }
        public int[] Drive { get; set; }
        public string[] CurrPath { get; set; }
        
        public TCLogic()
        {
            Drives = Directory.GetLogicalDrives();
            UpdateDrives();
            Directories = new List<string>[2];
            Files = new List<string>[2];
            Drive = new int[2];
            CurrPath = new string[2];
        }

        public void UpdateDrives()
        {
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            string[] readyDrives = new string[drives.Length];
            int i = 0;
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    readyDrives[i]=drive.ToString();
                    i += 1;
                }
            }
            Drives = readyDrives;
        }
        public void DirChanged(string path, int number)
        {
            string lastPath = CurrPath[number];
            CurrPath[number] = path;
            Directories[number] = new List<string>();

            if (CurrPath[number].Substring(Path.GetPathRoot(CurrPath[number]).Length).Length != 0)
                Directories[number].Add("..");

            try
            {
                foreach (var directory in Directory.GetDirectories(CurrPath[number]))
                {
                    var dirName = new DirectoryInfo(directory).Name;
                    Directories[number].Add("<D>" + dirName);
                }

                Files[number] = new List<string>();

                foreach (var file in Directory.GetFiles(CurrPath[number]))
                {
                    var fileName = new FileInfo(file).Name;
                    Directories[number].Add(fileName);
                }
            }
            catch (UnauthorizedAccessException error)
            {
                MessageBox.Show(error.Message);
                DirChanged(lastPath,number);
            }
        }

        public void Copy(string source, string destination)
        {
            if (File.Exists(destination))
            {
                MessageBoxResult dialogResult = MessageBox.Show("Taki plik już istnieje w tym miejscu", "Error", MessageBoxButton.OK);
            }
            else
            {
                try
                {
                    File.Copy(source, destination);
                }
                catch (IOException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (UnauthorizedAccessException e)
                {
                    MessageBox.Show("Nie masz dostępu", "Error",MessageBoxButton.OK);
                }
            }
        }

    }
}