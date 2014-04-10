using System;
using System.Linq;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace SS.Ynote.Classic.UI
{
    internal partial class BookmarksInfos : Form
    {
        private readonly FastColoredTextBox tb;

        public BookmarksInfos(FastColoredTextBox tb)
        {
            InitializeComponent();
            this.tb = tb;
            LoadBookmarks();
        }

        private void LoadBookmarks()
        {
            // Updated using Linq
            // foreach (var bookmark in tb.Bookmarks)
            // {
            //     var iline = bookmark.LineIndex + 1;
            //     var item = new ListViewItem(new[] { bookmark.Name, iline.ToString(), tb[bookmark.LineIndex].Text })
            //     {
            //         Tag = bookmark
            //     };
            //     lstbookmarks.Items.Add(item);
            // }
            foreach (var item in from bookmark in tb.Bookmarks
                let iline = bookmark.LineIndex + 1
                select new ListViewItem(new[] {bookmark.Name, iline.ToString(), tb[bookmark.LineIndex].Text})
                {
                    Tag = bookmark
                })
            {
                lstbookmarks.Items.Add(item);
            }
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lstbookmarks_DoubleClick(object sender, EventArgs e)
        {
            var clickeditem = lstbookmarks.SelectedItems[0];
            var bookmark = clickeditem.Tag as Bookmark;
            if (bookmark != null) bookmark.DoVisible();
        }
    }
}