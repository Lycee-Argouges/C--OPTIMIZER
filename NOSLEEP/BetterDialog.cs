using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPTIMIZER
{
    public partial class BetterDialog : Form
    {
        // the event
        public event EventHandler<bool> DialogReturnEvent;
        // raise the event
        protected virtual void OnProcessCompleted(bool IsSuccessful)
        {
            DialogReturnEvent?.Invoke(this, IsSuccessful);
            this.Close();
        }

        public System.Windows.Forms.RichTextBox label1;
        public System.Windows.Forms.RichTextBox label2;
        public System.Windows.Forms.Button buttonLeft;
        public System.Windows.Forms.Button buttonRight;
        public System.Windows.Forms.PictureBox pictureBox1;


        /// <summary>
        /// This method is part of the dialog.
        /// </summary>
        static public DialogResult ShowDialog(string title,
            string largeHeading,
            string smallExplanation,
            string leftButton,
            string rightButton,
            Image iconSet)
        {
            // Call the private constructor so the users only need to call this
            // function, which is similar to MessageBox.Show.
            // Returns a standard DialogResult.
            using (BetterDialog dialog = new BetterDialog(title, largeHeading,
                smallExplanation, leftButton, rightButton, iconSet))
            {
                DialogResult result = dialog.ShowDialog();
                return result;
            }
        }


        /// <summary>
        /// Use this with the above static method.
        /// </summary>
        public BetterDialog(string title,
            string largeHeading,
            string smallExplanation,
            string leftButton,
            string rightButton,
            Image iconSet)
        {

            this.label1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.RichTextBox();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();

            // Set up some properties.
            this.Font = SystemFonts.MessageBoxFont;
            this.ForeColor = SystemColors.WindowText;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            InitializeComponent();
            this.Width = 500;
            this.Height = 200;

            // Set the title, and some Text properties.
            this.Text = title;

            if (largeHeading != null)
            {
                label1.Text = largeHeading;
                label1.BorderStyle = BorderStyle.None;
                label1.BackColor = SystemColors.Menu;
                label1.ScrollBars = RichTextBoxScrollBars.None;
                label1.ReadOnly = true;
                label1.AutoSize = false;
                label1.Font = new Font(SystemFonts.MessageBoxFont.FontFamily.Name, 8.0f,
                        FontStyle.Bold, GraphicsUnit.Point);
                this.Controls.Add(label1);
                //label1.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.RichTextBox_ContentsResized);
            }

            if (smallExplanation != null) {
                label2.Text = string.IsNullOrEmpty(smallExplanation) ?
                    string.Empty : smallExplanation;
                label2.BorderStyle = BorderStyle.None;
                label2.BackColor = SystemColors.Menu;
                label2.ScrollBars = RichTextBoxScrollBars.None;
                label2.ReadOnly = true;
                label2.AutoSize = false;
                this.Controls.Add(label2);
                //label2.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.RichTextBox_ContentsResized);
            }

            // Set the left button, which is optional.
            if (string.IsNullOrEmpty(leftButton) == false)
            {
                this.buttonLeft.Text = leftButton;
                this.buttonLeft.Size = new Size(100, 25);
                this.Controls.Add(buttonLeft);
                //buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
                buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            }
            else
            {
                this.AcceptButton = buttonRight;
                this.buttonLeft.Visible = false;
                this.buttonLeft.Hide();
            }

            if (rightButton == null) rightButton = "OK";
            this.buttonRight.Text = rightButton;
            this.buttonRight.Size = new Size(100, 25);
            this.Controls.Add(buttonRight);
            buttonRight.Click += new System.EventHandler(this.buttonRight_Click);

            if (iconSet != null)
            {
                // Set the PictureBox and the icon.
                pictureBox1.Image = iconSet;
                this.pictureBox1.Location = new Point(30, 30);
                this.pictureBox1.Size = new Size(75, 75);
                this.Controls.Add(pictureBox1);
            }

            //Autosize the form and the richtextboxes
            int bigSize = AutoSizeControl(label1, 1);
            int smallSize = AutoSizeControl(label2, 1);
            this.Height = smallSize + bigSize + 125;
            this.label1.Location = new Point(130, 30);
            this.label2.Location = new Point(130, 30 + bigSize + 5);

            // Establish a minimum height.
            if (this.Height < 200)
            {
                this.Height = 200;
            }
            this.buttonRight.Location = new Point(360, this.Height - 75);
            this.buttonLeft.Location = new Point(260, this.Height - 75);

        }
        private int AutoSizeControl(Control control, int textPadding)
        {
            // Create a Graphics object for the Control.
            Graphics g = control.CreateGraphics();

            // Get the Size needed to accommodate the formatted Text.
            Size preferredSize = g.MeasureString(
               control.Text, control.Font).ToSize();

            //Define the size of the form and box
            int widthForm = this.Width;
            int widthBox = widthForm - 170;
            int heightBox = preferredSize.Height + ((int)preferredSize.Width / (widthBox-10)) * preferredSize.Height;
            if (heightBox > widthBox)
            {
                do
                {
                    this.Width = widthForm + 100;
                    widthBox = widthBox + 100;
                    heightBox = preferredSize.Height + ((int)preferredSize.Width / (widthBox-10)) * preferredSize.Height;
                } while (widthBox < heightBox);
            }

            // Pad the text and resize the control.
            control.ClientSize = new Size(
               widthBox + (textPadding * 2),
               heightBox + (textPadding * 2));

            // Clean up the Graphics object.
            g.Dispose();
            return heightBox;
        }
        private void buttonLeft_Click(object Sender, EventArgs e)
        {
            //MessageBox.Show("Clic on Annuler");
            OnProcessCompleted(false);
        }
        private void buttonRight_Click(object Sender, EventArgs e)
        {
            //MessageBox.Show("Clic on OK");
            OnProcessCompleted(true);
        }

        private void RichTextBox_ContentsResized(Object sender, ContentsResizedEventArgs e)
        {

            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            messageBoxCS.AppendFormat("{0} = {1}", "NewRectangle", e.NewRectangle);
            messageBoxCS.AppendLine();
            //MessageBox.Show(messageBoxCS.ToString(), "ContentsResized Event");
        }

    }
}
