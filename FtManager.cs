using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fieldtool.Annotations;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace fieldtool
{
    class FtManager : INotifyPropertyChanged
    {
        private static FtManager _instance;
        public static FtManager Instance()
        {
            if(_instance == null)
                _instance = new FtManager();
            return _instance;
        }

        public FtProject Projekt { get; private set; }


        public void ShowProjectPropertiesDialog()
        {
            if (!IsProjectLoaded())
            {
                MessageBox.Show("Die Projekteigenschaften können nicht angezeigt werden, da kein Projekt geöffnet ist.");
                return;
            }

            FrmProjectProperties frm = new FrmProjectProperties(Projekt);
            frm.ShowDialog();
        }

        public void ShowImportMovebankDialog()
        {
            if (!IsProjectLoaded())
            {
                MessageBox.Show("Ein Import kann nicht ausgeführt werden, weil kein Projekt geladen ist.");
                return;
            }

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.ShowDialog();
            //FrmProjectProperties frm = new FrmProjectProperties();
            //frm.ShowDialog();
        }

        public void ShowSettingsDialog()
        {
            //FrmProjectProperties frm = new FrmProjectProperties();
            //frm.ShowDialog();
        }



        public void NewProject()
        {
            if (IsProjectLoaded())
            {
                MessageBox.Show("Bitte schließen Sie zunächst das geöffnete Projekt.");
                return;
            }
            
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "FieldTool-Projekt|*.ftproj";

            DialogResult dr = dialog.ShowDialog();
            if (dr != DialogResult.OK)
                return;

            Projekt = new FtProject(dialog.FileName);
        }

        public void OpenProject()
        {
            if (IsProjectLoaded())
            {
                MessageBox.Show("Bitte schließen Sie zunächst das geöffnete Projekt.");
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "FieldTool-Projekt|*.ftproj";
            DialogResult dr = dialog.ShowDialog();
            if (dr != DialogResult.OK)
                return;

            Projekt = FtProject.Deserialize(dialog.FileName);
        }


        public void SaveProject()
        {
            if (!IsProjectLoaded())
            {
                MessageBox.Show("Es ist kein Projekt geöffnet.");
                return;
            }
            Projekt.Save();
        }

        public void CloseProject()
        {
            if (!IsProjectLoaded())
            {
                MessageBox.Show("Es ist kein Projekt geöffnet.");
                return;
            }

            DialogResult dr = MessageBox.Show("Soll das Projekt geschlossen werden?", "Schließen?",
                MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Projekt = null;
            }
                
        }

        public bool IsProjectLoaded()
        {
            if (Projekt == null)
                return false;
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
