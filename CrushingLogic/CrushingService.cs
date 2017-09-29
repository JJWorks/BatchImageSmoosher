using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace CrushingLogic
{
    public class CrushingService
    {

        //HardCoded program was changed for Guetzli which does not do well with ParallelOptions.  Should be default or -1 on others.
        private int DegreeOfParallelism;

        public CrushingService(string DirectoryToCrush, string tempTransferFolder, string WorkLocation, string PNGCrusherFile, string JPGCrusherFile, int degreeOfParallelism = -1)
        {
            workinglocation = WorkLocation;
            PNGExeName = PNGCrusherFile;
            JPGExeName = JPGCrusherFile;
            CrushingLocation = new DirectoryInfo(DirectoryToCrush);
            transferFolder = tempTransferFolder;
            DegreeOfParallelism = degreeOfParallelism;
        }

        private string workinglocation;
        private string PNGExeName;
        private string JPGExeName;
        private DirectoryInfo CrushingLocation;
        private string transferFolder;


        public void CrushingExecute()
        {
            CrushingLogic(CrushingLocation);
        }


        private void CrushingLogic(DirectoryInfo Root)
        {
            foreach (DirectoryInfo DIParent in Root.GetDirectories())
            {
                CrushingLogic(DIParent);
            }
            Parallel.ForEach(Root.GetFiles(), new ParallelOptions { MaxDegreeOfParallelism = DegreeOfParallelism }, fi =>
                {
                    LogicalCrusher.BaseCrusher BC = null;
                    int Horizon = 0;
                    switch (fi.Extension.ToLower())
                    {
                        //case ".png": BC = new LogicalCrusher.PNGCrusher(PNGExeName, workinglocation);
                        //    break;
                        case ".jpg": BC = new LogicalCrusher.Guetzli(JPGExeName, workinglocation);
                            Image i = Image.FromFile(fi.FullName);
                            Horizon =  (int) Math.Round(i.HorizontalResolution);
                            break;
                        default:
                            break;
                    }
                    if (BC != null)
                    {
                        if (!Directory.Exists(fi.DirectoryName.Replace(CrushingLocation.FullName, transferFolder)))
                            Directory.CreateDirectory(fi.DirectoryName.Replace(CrushingLocation.FullName, transferFolder));
                        BC.ExecuteCommands(fi.FullName.Replace(CrushingLocation.FullName, transferFolder), fi.FullName);
                        BC = new LogicalCrusher.EXIFDPIChange(PNGExeName, workinglocation);
                        BC.ExecuteCommands(Horizon.ToString(), fi.FullName.Replace(CrushingLocation.FullName, transferFolder));
                        if (File.Exists(fi.FullName.Replace(CrushingLocation.FullName, transferFolder) + "_original"))
                            File.Delete(fi.FullName.Replace(CrushingLocation.FullName, transferFolder) + "_original");
                    }
                });
            /*
            foreach (FileInfo fi in Root.GetFiles())
            {
                LogicalCrusher.BaseCrusher BC = null;
                switch (fi.Extension.ToLower())
                {
                    case ".png": BC = new LogicalCrusher.PNGCrusher(PNGExeName, workinglocation);
                        break;
                    case ".jpg": BC = new LogicalCrusher.JPGCrusher(JPGExeName, workinglocation);
                        break;
                    default:
                        break;
                }
                if (BC != null)
                {

                    if (!Directory.Exists(fi.DirectoryName.Replace(CrushingLocation.FullName, transferFolder)))
                        Directory.CreateDirectory(fi.DirectoryName.Replace(CrushingLocation.FullName, transferFolder));
                    BC.ExecuteCommands(fi.FullName.Replace(CrushingLocation.FullName, transferFolder), fi.FullName);
                }
            }
             * */
        }

    }
}
