
namespace LastEpochBuildPlanner
{
    partial class Passive
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
            this.primalistSubClassPanel = new System.Windows.Forms.Panel();
            this.primalistPassivePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // primalistSubClassPanel
            // 
            this.primalistSubClassPanel.Location = new System.Drawing.Point(12, 12);
            this.primalistSubClassPanel.Name = "primalistSubClassPanel";
            this.primalistSubClassPanel.Size = new System.Drawing.Size(298, 643);
            this.primalistSubClassPanel.TabIndex = 0;
            // 
            // primalistPassivePanel
            // 
            this.primalistPassivePanel.BackColor = System.Drawing.Color.Transparent;
            this.primalistPassivePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.primalistPassivePanel.Location = new System.Drawing.Point(316, 12);
            this.primalistPassivePanel.Name = "primalistPassivePanel";
            this.primalistPassivePanel.Size = new System.Drawing.Size(956, 643);
            this.primalistPassivePanel.TabIndex = 1;
            // 
            // Passive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 667);
            this.Controls.Add(this.primalistPassivePanel);
            this.Controls.Add(this.primalistSubClassPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Passive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Passives";
            this.Load += new System.EventHandler(this.Passive_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel primalistSubClassPanel;
        private System.Windows.Forms.Panel primalistPassivePanel;
    }
}