using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CrushingLogic.LogicalCrusher
{
    abstract public class BaseCrusher
    {
        public BaseCrusher(string WorkingFolder)
        {
            WorkingLocation = WorkingFolder;
        }

        public abstract string EXELocation { set; get; }
        public abstract string WorkingFolder { set; get; }


        public abstract string BaseArgs { set; get;  }

        private string WorkingLocation;


        public void ExecuteCommands(string DestinationFileLocation, string SourceFileLocation)
        {
            RunEXE(EXELocation, string.Format(BaseArgs, SourceFileLocation, DestinationFileLocation));
        }


        private void RunEXE(string LocationofExe, string Arguments)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.FileName = LocationofExe;
            psi.WorkingDirectory = WorkingLocation;
            psi.Arguments = Arguments;
            process.StartInfo = psi;
            process.Start();
            process.WaitForExit();
        }
    }
}
