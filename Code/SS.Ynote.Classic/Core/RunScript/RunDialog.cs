using System;
using System.Windows.Forms;
using SS.Ynote.Classic.Core.RunScript;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic
{
    internal class RunDialog : Form
    {
        #region Designer

        private Label _label1;
        private Button button2;
        private Button button3;
        private GroupBox groupBox1;
        private ComboBox pgname;

        private void InitializeComponent()
        {
            this._label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pgname = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _label1
            // 
            this._label1.AutoSize = true;
            this._label1.Location = new System.Drawing.Point(22, 38);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(43, 13);
            this._label1.TabIndex = 1;
            this._label1.Text = "Script : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pgname);
            this.groupBox1.Controls.Add(this._label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 90);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Run";
            // 
            // pgname
            // 
            this.pgname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.pgname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.pgname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pgname.FormattingEnabled = true;
            this.pgname.Location = new System.Drawing.Point(71, 33);
            this.pgname.Name = "pgname";
            this.pgname.Size = new System.Drawing.Size(248, 21);
            this.pgname.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(157, 117);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 22);
            this.button2.TabIndex = 4;
            this.button2.Text = "Run";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += button2_Click;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(257, 117);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 22);
            this.button3.TabIndex = 5;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += button3_Click;
            // 
            // RunDialog
            // 
            this.ClientSize = new System.Drawing.Size(363, 170);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Name = "RunDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Run";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Designer

        #region Constructor

        private readonly string _file;
        private readonly DockPanel _panel;

        public RunDialog(string file, DockPanel panel)
        {
            InitializeComponent();
            PopulateListItems();
            pgname.SelectedIndex = 0;
            _file = file;
            _panel = panel;
        }

        #endregion Constructor

        private void PopulateListItems()
        {
            foreach (var file in RunScript.GetConfigurations())
                pgname.Items.Add(RunScript.Get(file));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var item = pgname.SelectedItem as RunScript;
            if (item == null) return;
            item.Run();
            Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}