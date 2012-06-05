namespace SideCraft.menu {
    partial class SettingsMenuScreen {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnBack = new System.Windows.Forms.Button();
            this.lblBlockSize = new System.Windows.Forms.Label();
            this.txtBlockSize = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(14, 19);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblBlockSize
            // 
            this.lblBlockSize.AutoSize = true;
            this.lblBlockSize.Location = new System.Drawing.Point(29, 61);
            this.lblBlockSize.Name = "lblBlockSize";
            this.lblBlockSize.Size = new System.Drawing.Size(60, 13);
            this.lblBlockSize.TabIndex = 1;
            this.lblBlockSize.Text = "Block Size:";
            // 
            // txtBlockSize
            // 
            this.txtBlockSize.Location = new System.Drawing.Point(95, 58);
            this.txtBlockSize.Name = "txtBlockSize";
            this.txtBlockSize.Size = new System.Drawing.Size(100, 20);
            this.txtBlockSize.TabIndex = 2;
            this.txtBlockSize.Text = "24";
            // 
            // SettingsMenuScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtBlockSize);
            this.Controls.Add(this.lblBlockSize);
            this.Controls.Add(this.btnBack);
            this.Name = "SettingsMenuScreen";
            this.Size = new System.Drawing.Size(450, 260);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblBlockSize;
        public System.Windows.Forms.TextBox txtBlockSize;
    }
}
