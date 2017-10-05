using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iUPDATE_sw
{
    static class Program
    {
        static string pathToCAD;
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {

            pathToCAD = FindPathToCadDECOR();
            if ( pathToCAD == String.Empty)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form1 form1 = new Form1();
                Application.Run(form1);
                pathToCAD = form1.PathToCAD;
            }

            InstalliUPDATE(pathToCAD);



        }

        private static void InstalliUPDATE(string pathTo_iUPDATE_EXE)
        {
            string currentPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);

            if (pathTo_iUPDATE_EXE == String.Empty)
            {
                MessageBox.Show("Nie znaleziono ścieżki do CadDEcor");
                return;
            }

            //StreamReader file = File.OpenText();

            string pathToiUPDATE_bat = Path.ChangeExtension(pathTo_iUPDATE_EXE, "bat");
            string pathToCad = Path.GetDirectoryName(pathTo_iUPDATE_EXE) ;
            string pathDesktopCurrentUser = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 

            MoveFile(currentPath + "\\iUPDATE.ex_", pathToCad + "\\iUPDATE.exe");
            MoveFile(currentPath + "\\SendEmailsw.ex_", pathToCad + "\\SendEmailsw.exe");
           
            File.AppendAllText(pathToiUPDATE_bat, "\"" + pathTo_iUPDATE_EXE + "\"");
            File.AppendAllText(pathToiUPDATE_bat, " | ");
            File.AppendAllText(pathToiUPDATE_bat, "\"" + pathToCad + "\\SendEmailsw.exe\"");

            ShortcutHelper.CreateShortcut(
                 pathDesktopCurrentUser + "\\Aktualizacja CADDecor.lnk",
                 "C:\\Windows\\System32\\runas.exe",
                 "c:\\Users\\Admin\\Desktop\\",
                 " /env /user:Admin /savecred \"" + pathToiUPDATE_bat + "\""
                 );
            Process proc = new Process();
            proc.StartInfo.FileName = pathDesktopCurrentUser + "\\Aktualizacja CADDecor.lnk";
            proc.Start();
        }

        private static bool MoveFile(string pathSrc, string pathDest)
        {
            if (File.Exists(pathDest))
                File.Delete(pathDest);

            if (File.Exists(pathSrc))
                File.Copy(pathSrc, pathDest);
            else
            {
                MessageBox.Show("Nie znalazłem SenEmailsw.exe");
                return false;
            }
            return true;
        }

        private static string FindPathToCadDECOR()
        {
            string[] paths =
            {
                "c:\\CadProjekt\\CAD Decor Paradyz v. 2.5.0\\iUPDATE.exe",
                "d:\\CadProjekt\\CAD Decor Paradyz v. 2.5.0\\iUPDATE.exe",
                "c:\\CadProjekt\\CAD Decor Paradyz v. 2.4.0\\iUPDATE.exe",
                "d:\\CadProjekt\\CAD Decor Paradyz v. 2.4.0\\iUPDATE.exe",
                "c:\\CadProjekt\\CAD Decor Paradyz v. 2.3.0\\iUPDATE.exe",
                "d:\\CadProjekt\\CAD Decor Paradyz v. 2.3.0\\iUPDATE.exe",
                "c:\\CadProjekt\\CAD Decor Paradyz v. 2.2.0\\iUPDATE.exe",
                "d:\\CadProjekt\\CAD Decor Paradyz v. 2.2.0\\iUPDATE.exe",
                "c:\\CadProjekt\\CAD Decor Paradyz v. 2.1.0\\iUPDATE.exe",
                "d:\\CadProjekt\\CAD Decor Paradyz v. 2.1.0\\iUPDATE.exe",
                "c:\\CadProjekt\\CAD Decor Paradyz v. 2.0.0\\iUPDATE.exe",
                "d:\\CadProjekt\\CAD Decor Paradyz v. 2.0.0\\iUPDATE.exe",
                "c:\\CadProjekt\\iUPDATE.exe",
                "d:\\CadProjekt\\iUPDATE.exe"

            };

            foreach (var path in paths)
            {
                if (File.Exists(path))               
                    return path;
            }

            return String.Empty; 
        }

    }
}
