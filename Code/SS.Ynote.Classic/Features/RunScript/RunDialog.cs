using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SS.Ynote.Classic.Features.RunScript;
using WeifenLuo.WinFormsUI.Docking;

namespace SS.Ynote.Classic
{
    internal class RunDialog : Form
    {
        #region Designer

        private Label _label1;
        private Button button1;
        private Button button2;
        private Button button3;
        private GroupBox groupBox1;
        private ComboBox pgname;

        private void InitializeComponent()
        {
            _label1 = new Label();
            groupBox1 = new GroupBox();
            pgname = new ComboBox();
            button2 = new Button();
            button3 = new Button();
            button1 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            //
            // _label1
            //
            _label1.AutoSize = true;
            _label1.Location = new Point(22, 38);
            _label1.Name = "_label1";
            _label1.Size = new Size(43, 13);
            _label1.TabIndex = 1;
            _label1.Text = "Script : ";
            //
            // groupBox1
            //
            groupBox1.Controls.Add(pgname);
            groupBox1.Controls.Add(_label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(329, 90);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Run";
            //
            // pgname
            //
            pgname.AutoCompleteMode = AutoCompleteMode.Suggest;
            pgname.AutoCompleteSource = AutoCompleteSource.ListItems;
            pgname.DropDownStyle = ComboBoxStyle.DropDownList;
            pgname.FormattingEnabled = true;
            pgname.Location = new Point(71, 33);
            pgname.Name = "pgname";
            pgname.Size = new Size(248, 21);
            pgname.TabIndex = 3;
            //
            // button2
            //
            button2.Location = new Point(67, 118);
            button2.Name = "button2";
            button2.Size = new Size(82, 22);
            button2.TabIndex = 4;
            button2.Text = "Run";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            //
            // button3
            //
            button3.Location = new Point(257, 117);
            button3.Name = "button3";
            button3.Size = new Size(82, 22);
            button3.TabIndex = 5;
            button3.Text = "Cancel";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            //
            // button1
            //
            button1.Location = new Point(155, 117);
            button1.Name = "button1";
            button1.Size = new Size(96, 23);
            button1.TabIndex = 6;
            button1.Text = "Editor";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            //
            // RunDialog
            //
            ClientSize = new Size(363, 170);
            Controls.Add(button1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "RunDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Run";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
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
            foreach (var file in RunConfiguration.GetConfigurations())
                pgname.Items.Add(RunConfiguration.ToRunConfig(file));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var item = pgname.SelectedItem as RunConfiguration;
            if (item == null) return;
            item.ProcessConfiguration(_file);
            var temp = Path.GetTempFileName() + ".bat";
            File.WriteAllText(temp, item.ToBatch());
            var console = new Shell("cmd.exe", "/k " + temp);
            console.Show(_panel, DockState.DockBottom);
            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var form = new RunScriptEditor {StartPosition = FormStartPosition.CenterScreen};
            form.ShowDialog();
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}