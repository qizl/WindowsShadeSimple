namespace WindowsShadeSimple
{
    partial class FormShade
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // FormShade
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 450);
            Cursor = Cursors.SizeAll;
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormShade";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WindowsShadeSimple";
            TopMost = true;
            MouseDoubleClick += FormShade_MouseDoubleClick;
            MouseDown += FormShade_MouseDown;
            MouseMove += FormShade_MouseMove;
            ResumeLayout(false);
        }

        #endregion
    }
}
