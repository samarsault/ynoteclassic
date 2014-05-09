namespace SS.Ynote.Classic.UI
{
    partial class MacroExecDialog
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
            this.cmbMacros = new System.Windows.Forms.ComboBox();
            this.times = new System.Windows.Forms.NumericUpDown();
            this.btnExec = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.times)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbMacros
            // 
            this.cmbMacros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMacros.FormattingEnabled = true;
            this.cmbMacros.Location = new System.Drawing.Point(64, 13);
            this.cmbMacros.Name = "cmbMacros";
            this.cmbMacros.Size = new System.Drawing.Size(200, 21);
            this.cmbMacros.TabIndex = 0;
            // 
            // times
            // 
            this.times.Location = new System.Drawing.Point(94, 49);
            this.times.Name = "times";
            this.times.Size = new System.Drawing.Size(70, 20);
            this.times.TabIndex = 1;
            this.times.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnExec
            // 
            this.btnExec.Location = new System.Drawing.Point(99, 82);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(85, 23);
            this.btnExec.TabIndex = 2;
            this.btnExec.Text = "Execute";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(190, 82);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Macro : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "No. of Times : ";
            // 
            // MacroExecDialog
            // 
            this.AcceptButton = this.btnExec;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(286, 117);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.times);
            this.Controls.Add(this.cmbMacros);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MacroExecDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Execute Macro Multiple Times";
            ((System.ComponentModel.ISupportInitialize)(this.times)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMacros;
        private System.Windows.Forms.NumericUpDown times;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}