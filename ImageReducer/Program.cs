using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrushingLogic;
using System.IO;

namespace ImageReducer
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo DI = new DirectoryInfo(args[1]);
            ClearDirectory(DI);
            DirectoryInfo SourceData = new DirectoryInfo(args[0]);

            CrushingService cs = new CrushingService(SourceData.FullName, DI.FullName, @"C:\Workshop\ImageReducer", "exiftool.exe", "guetzli_windows_x86-64.exe");
            cs.CrushingExecute();

            //foreach (DirectoryInfo ggg in SourceData.GetDirectories())
            //{
            //    cs = new CrushingService(ggg.FullName, DI.FullName, @"C:\Workshop\ImageReducer", "exiftool.exe", "guetzli_windows_x86-64.exe");
            //    cs.CrushingExecute();

            //    //CopyDirectory(DI.FullName, ggg.FullName);

            //    //ClearDirectory(DI);
            //}
            
        }


        static void ClearDirectory(DirectoryInfo DI)
        {
            foreach (FileInfo file in DI.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in DI.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private static void CopyDirectory(string sourcePath, string destPath)
        {
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            foreach (string file in Directory.GetFiles(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(file));
                File.Copy(file, dest, true);
            }

            foreach (string folder in Directory.GetDirectories(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(folder));
                CopyDirectory(folder, dest);
            }
        }
    }
}
