namespace Simple3DGraphics.Demo
{
    partial class DemoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.view3DControl1 = new Simple3DGraphics.Lib.View3DControl();
            this.SuspendLayout();
            // 
            // view3DControl1
            // 
            this.view3DControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view3DControl1.Location = new System.Drawing.Point(0, 0);
            this.view3DControl1.Name = "view3DControl1";
            this.view3DControl1.Size = new System.Drawing.Size(800, 450);
            this.view3DControl1.TabIndex = 0;
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.view3DControl1);
            this.Name = "DemoForm";
            this.Text = "DemoForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.View3DControl view3DControl1;
    }
}