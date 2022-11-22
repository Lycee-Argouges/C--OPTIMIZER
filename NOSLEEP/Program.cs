using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPTIMIZER
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process[] localByName = Process.GetProcessesByName("OPTIMIZER");
            int detectProc = 0;
            foreach (Process p in localByName)
            {
                detectProc++;
                if (detectProc > 1) {
                    try
                    {
                        BetterDialog bl = new BetterDialog("OPTIMISATEUR ETABLISSEMENT", "L'application est déjà en cours d'exécution.", "Cette fenêtre va se fermer.", null, "OK", Properties.Resources._75px_emoticon_sleep);
                        bl.DialogReturnEvent += bl_ProcessCompleted; // register with an event
                        bl.ShowDialog();
                        //Application.Run(new BetterDialog("OPTIMISATEUR ETABLISSEMENT", "L'application est déjà en cours d'exécution." , "Cette fenêtre va se fermer.", "Annuler", "OK", Properties.Resources._75px_emoticon_sleep));
                    }
                    catch
                    {
                        MessageBox.Show("Une erreur s'est produite. L'application va s'arrêter.", "OPTIMISATEUR ETABLISSEMENT", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                    //Environment.Exit(0);
                }
            }
            Application.Run(new NotifyZone());
        }

        // event handler
        public static void bl_ProcessCompleted(object sender, bool IsSuccessful)
        {
            if (IsSuccessful == true) Environment.Exit(0);
            //if (IsSuccessful == false) MessageBox.Show("False !");
        }



    }
}
