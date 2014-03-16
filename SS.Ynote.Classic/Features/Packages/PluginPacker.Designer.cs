namespace SS.Ynote.Classic.Features.Packages
{
    partial class PluginPacker
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstrefs = new System.Windows.Forms.ListBox();
            this.txtpluginfile = new System.Windows.Forms.TextBox();
            this.txtoutfile = new System.Windows.Forms.TextBox();
            this.pluginfilebtn = new System.Windows.Forms.Button();
            this.btnaddref = new System.Windows.Forms.Button();
            this.btdelref = new System.Windows.Forms.Button();
            this.outfilebtn = new System.Windows.Forms.Button();
            this.createpackagebtn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Plugin File : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "References : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Output File :";
            // 
            // lstrefs
            // 
            this.lstrefs.FormattingEnabled = true;
            this.lstrefs.Location = new System.Drawing.Point(97, 74);
            this.lstrefs.Name = "lstrefs";
            this.lstrefs.Size = new System.Drawing.Size(212, 56);
            this.lstrefs.TabIndex = 3;
            // 
            // txtpluginfile
            // 
            this.txtpluginfile.Location = new System.Drawing.Point(97, 35);
            this.txtpluginfile.Name = "txtpluginfile";
            this.txtpluginfile.Size = new System.Drawing.Size(212, 20);
            this.txtpluginfile.TabIndex = 4;
            // 
            // txtoutfile
            // 
            this.txtoutfile.Location = new System.Drawing.Point(77, 149);
            this.txtoutfile.Name = "txtoutfile";
            this.txtoutfile.Size = new System.Drawing.Size(233, 20);
            this.txtoutfile.TabIndex = 5;
            // 
            // pluginfilebtn
            // 
            this.pluginfilebtn.Location = new System.Drawing.Point(314, 33);
            this.pluginfilebtn.Name = "pluginfilebtn";
            this.pluginfilebtn.Size = new System.Drawing.Size(27, 23);
            this.pluginfilebtn.TabIndex = 6;
            this.pluginfilebtn.Text = "...";
            this.pluginfilebtn.UseVisualStyleBackColor = true;
            this.pluginfilebtn.Click += new System.EventHandler(this.pluginfilebtn_Click);
            // 
            // btnaddref
            // 
            this.btnaddref.Location = new System.Drawing.Point(315, 74);
            this.btnaddref.Name = "btnaddref";
            this.btnaddref.Size = new System.Drawing.Size(27, 23);
            this.btnaddref.TabIndex = 7;
            this.btnaddref.Text = "+";
            this.btnaddref.UseVisualStyleBackColor = true;
            this.btnaddref.Click += new System.EventHandler(this.btnaddref_Click);
            // 
            // btdelref
            // 
            this.btdelref.Location = new System.Drawing.Point(314, 103);
            this.btdelref.Name = "btdelref";
            this.btdelref.Size = new System.Drawing.Size(27, 23);
            this.btdelref.TabIndex = 8;
            this.btdelref.Text = "-";
            this.btdelref.UseVisualStyleBackColor = true;
            // 
            // outfilebtn
            // 
            this.outfilebtn.Location = new System.Drawing.Point(316, 146);
            this.outfilebtn.Name = "outfilebtn";
            this.outfilebtn.Size = new System.Drawing.Size(26, 23);
            this.outfilebtn.TabIndex = 9;
            this.outfilebtn.Text = "...";
            this.outfilebtn.UseVisualStyleBackColor = true;
            this.outfilebtn.Click += new System.EventHandler(this.outfilebtn_Click);
            // 
            // createpackagebtn
            // 
            this.createpackagebtn.Location = new System.Drawing.Point(125, 223);
            this.createpackagebtn.Name = "createpackagebtn";
            this.createpackagebtn.Size = new System.Drawing.Size(117, 29);
            this.createpackagebtn.TabIndex = 10;
            this.createpackagebtn.Text = "Create Package ";
            this.createpackagebtn.UseVisualStyleBackColor = true;
            this.createpackagebtn.Click += new System.EventHandler(this.createpackagebtn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(259, 223);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(95, 29);
            this.cancel_btn.TabIndex = 11;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtoutfile);
            this.groupBox1.Controls.Add(this.pluginfilebtn);
            this.groupBox1.Controls.Add(this.txtpluginfile);
            this.groupBox1.Controls.Add(this.btdelref);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.outfilebtn);
            this.groupBox1.Controls.Add(this.btnaddref);
            this.groupBox1.Controls.Add(this.lstrefs);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 205);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Package Details";
            // 
            // PluginPacker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 264);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.createpackagebtn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PluginPacker";
            this.Text = "Plugin Packager";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstrefs;
        private System.Windows.Forms.TextBox txtpluginfile;
        private System.Windows.Forms.TextBox txtoutfile;
        private System.Windows.Forms.Button pluginfilebtn;
        private System.Windows.Forms.Button btnaddref;
        private System.Windows.Forms.Button btdelref;
        private System.Windows.Forms.Button outfilebtn;
        private System.Windows.Forms.Button createpackagebtn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}