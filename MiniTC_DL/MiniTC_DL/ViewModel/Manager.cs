using MiniTC_DL.Model;
using MiniTC_DL.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniTC_DL.ViewModel
{
    class Manager : ViewModelBase
    {

        public string LPath{get{return Model.CurrPath[0];} set{ Model.CurrPath[0] = value; }}

        public string RPath{get{return Model.CurrPath[1];}set{Model.CurrPath[1] = value;}}

        public string[] Drives
        {get{return Model.Drives;}}

        public List<string> LFiles
        {
            get{List<string> files = Model.Directories[0];
                try{
                    foreach (var item in Model.Files[0])
                        files.Add(item);}
                catch { }
                return files;}}

        public List<string> RFiles{get{
                List<string> files = Model.Directories[1];
                try
                {
                    foreach (var item in Model.Files[1])
                        files.Add(item);
                }
                catch { }
                return files;}}

        public int LDriveInd{ get{return Model.Drive[0];} set{ Model.Drive[0] = value; }  }

        public int RDriveInd {get{return Model.Drive[1];}set{ Model.Drive[1] = value;}}

        public int CurrLeft { get; set; } = -1;
        public int CurrRight { get; set; } = -1;
        public int LastSelected { get; set; } = -1;

        public Manager()
        {
            LDriveInd = -1;
            RDriveInd = -1;
            LPath = "";
            RPath = "";
        }
        TCLogic Model = new TCLogic();

        private ICommand lDrvChanged = null;
        public ICommand LDrvChanged
        {
            get
            {
                if (lDrvChanged == null)
                    lDrvChanged = new RelayCommand(
                        arg => { Model.DirChanged(Drives[LDriveInd], 0); OnPropertyChanged(nameof(LFiles), nameof(LPath)); },
                        arg => true
            );
                return lDrvChanged;
            }
        }

        private ICommand rDirChanged = null;
        public ICommand RDirChanged
        {
            get
            {
                if (rDirChanged == null)
                    rDirChanged = new RelayCommand(
                        arg => { Model.DirChanged(Drives[RDriveInd], 1); OnPropertyChanged(nameof(RFiles), nameof(RPath)); },
                        arg => true
            );
                return rDirChanged;

            }
        }

        private ICommand lDirChanged = null;
        public ICommand LDirChanged
        {
            get
            {
                if (lDirChanged == null)
                    lDirChanged = new RelayCommand(
                        arg => {
                            string current = LFiles[CurrLeft];
                            if (current == "..")
                            {
                                Model.DirChanged(Path.GetDirectoryName(LPath), 0);
                                CurrLeft = -1;
                                OnPropertyChanged(nameof(LFiles), nameof(LPath), nameof(CurrLeft));
                                if (LastSelected == 0)
                                    LastSelected = -1;
                            }
                            else if (Directory.Exists(LPath + @"\" + current.Substring(3)))
                            {
                                Model.DirChanged(LPath + @"\" + current.Substring(3), 0);
                                CurrLeft = -1;
                                OnPropertyChanged(nameof(LFiles), nameof(LPath), nameof(CurrLeft));
                                if (LastSelected == 0)
                                    LastSelected = -1;
                            }
                            else
                                LastSelected = 0;
                        },
                        arg => CurrLeft >= 0
            );
                return lDirChanged;

            }
        }

        private ICommand rightDirChanged = null;
        public ICommand RightDirChanged
        {
            get
            {
                if (rightDirChanged == null)
                    rightDirChanged = new RelayCommand(
                        arg => {
                            string current = RFiles[CurrRight];
                            if (current == "..")
                            {
                                Model.DirChanged(Path.GetDirectoryName(RPath), 1);
                                CurrRight = -1;
                                OnPropertyChanged(nameof(RFiles), nameof(RPath), nameof(CurrRight));
                                if (LastSelected == 1)
                                    LastSelected = -1;
                            }
                            else if (Directory.Exists(RPath + @"\" + current.Substring(3)))
                            {
                                Model.DirChanged(RPath + @"\" + current.Substring(3), 1);
                                CurrRight = -1;
                                OnPropertyChanged(nameof(RFiles), nameof(RPath), nameof(CurrRight));
                                if (LastSelected == 1)
                                    LastSelected = -1;
                            }
                            else
                                LastSelected = 1;
                        },
                        arg => CurrRight >= 0
            );
                return rightDirChanged;

            }
        }

        private ICommand copy = null;
        public ICommand Copy
        {
            get
            {
                if (copy == null)
                    copy = new RelayCommand(
                        arg => {
                            if (LastSelected == 0)
                            {
                                Model.Copy(LPath + @"\" + LFiles[CurrLeft], RPath + @"\" + LFiles[CurrLeft]);
                                Model.DirChanged(RPath, 1);
                            }
                            else
                            {
                                Model.Copy(RPath + @"\" + RFiles[CurrRight], LPath + @"\" + RFiles[CurrRight]);
                                Model.DirChanged(LPath, 0);
                            }

                            OnPropertyChanged(nameof(RFiles), nameof(LFiles));
                        },
                        arg => LastSelected != -1 && LPath.Length > 0 && RPath.Length > 0
            );
                return copy;

            }
        }
    }
}
