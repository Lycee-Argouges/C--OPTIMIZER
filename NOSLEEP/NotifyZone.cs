using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPTIMIZER
{
    public class NotifyZone : System.Windows.Forms.Form
    {
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.ComponentModel.IContainer components;
        public static bool someFlag = false;

        public NotifyZone()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();

            // Initialize contextMenu1
            this.contextMenu1.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { this.menuItem1 });
            this.contextMenu1.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { this.menuItem2 });
            this.contextMenu1.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { this.menuItem3 });


            // Initialize menuItem1
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "S&tart";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // Initialize menuItem1
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "P&ause";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // Initialize menuItem1
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "E&xit";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);

            // Set up how the form should be displayed.
            this.ShowInTaskbar = false;
            this.ShowIcon = false;
            this.WindowState = FormWindowState.Minimized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ClientSize = new System.Drawing.Size(300, 155);
            this.Text = "OPTIMISATEUR ARGOUGES";

            // Create the NotifyIcon.
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            //notifyIcon1.Icon = new Icon("appicon.ico");
            notifyIcon1.Icon = Properties.Resources.laboratory;

            // The ContextMenu property sets the menu that will
            // appear when the systray icon is right clicked.
            notifyIcon1.ContextMenu = this.contextMenu1;

            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyIcon1.Text = "OPTIMISATEUR ARGOUGES";
            notifyIcon1.Visible = true;

            // Handle the DoubleClick event to activate the form.
            notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);

            // Start the prevent sleeping mode.
            someFlag = false;
            ServerClass serverObject = new ServerClass();
            Thread InstanceCaller = new Thread(new ThreadStart(serverObject.InstanceMethod));
            InstanceCaller.Start();
        }

        protected override void Dispose(bool disposing)
        {
            // Clean up any components being used.
            if (disposing)
                if (components != null)
                    components.Dispose();

            base.Dispose(disposing);
        }

        private void notifyIcon1_DoubleClick(object Sender, EventArgs e)
        {
            // Show the form when the user double clicks on the notify icon.

            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            // Activate the form.
            this.Activate();
        }

        private void menuItem1_Click(object Sender, EventArgs e)
        {
            if (someFlag == true)
            {
                someFlag = false;
                // Start the prevent sleeping mode.
                BetterDialog.ShowDialog("OPTIMISATEUR ETABLISSEMENT", "L'application démarre.", "Cliquez sur OK pour continuer.", null, "OK", Properties.Resources._75px_emoticon_sleep);
                ServerClass serverObject = new ServerClass();
                Thread InstanceCaller = new Thread(new ThreadStart(serverObject.InstanceMethod));
                InstanceCaller.Start();
            } else
            {
                BetterDialog.ShowDialog("OPTIMISATEUR ETABLISSEMENT", "L'application est déjà en cours d'exécution.", "Cette fenêtre va se fermer.", null, "OK", Properties.Resources._75px_emoticon_sleep);
            }
        }
        private void menuItem2_Click(object Sender, EventArgs e)
        {
            if (someFlag == false)
            {
                someFlag = true;
                // Stop the prevent sleeping mode.
                ServerClass serverObject = new ServerClass();
                Thread InstanceCaller = new Thread(new ThreadStart(serverObject.StopInstanceMethod));
                InstanceCaller.Start();
                BetterDialog.ShowDialog("OPTIMISATEUR ETABLISSEMENT", "L'application a été mise en pause...", "Vous pouvez toujours la relancer !", null, "OK", Properties.Resources._75px_emoticon_sleep);
            }
        }
        private void menuItem3_Click(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            someFlag = true;
            Task.Delay(12000);
            this.Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NotifyZone
            // 
            this.ClientSize = new System.Drawing.Size(284, 111);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NotifyZone";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);

        }

    }
}
