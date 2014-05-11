namespace SS.Ynote.Classic.Features.Syntax
{
    partial class SearchResults
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
            this.lvresults = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvresults
            // 
            this.lvresults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvresults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvresults.FullRowSelect = true;
            this.lvresults.GridLines = true;
            this.lvresults.Location = new System.Drawing.Point(0, 0);
            this.lvresults.Name = "lvresults";
            this.lvresults.Size = new System.Drawing.Size(669, 201);
            this.lvresults.TabIndex = 1;
            this.lvresults.UseCompatibleStateImageBehavior = false;
            this.lvresults.View = System.Windows.Forms.View.Details;
            this.lvresults.DoubleClick += lvresults_DoubleClick;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File";
            this.columnHeader1.Width = 561;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Line";
            this.columnHeader2.Width = 71;
            // 
            // FindInFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 201);
            this.Controls.Add(this.lvresults);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FindInFiles";
            this.ShowIcon = false;
            this.Text = "Search Results";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvresults;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;

    }
}