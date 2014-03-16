#region

using System;
using System.Windows.Forms;
using FastColoredTextBoxNS;

#endregion

namespace SS.Ynote.Classic.UI
{
    public partial class BookmarksInfos : Form
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
            foreach (var bookmark in tb.Bookmarks)
            {
                var iline = bookmark.LineIndex + 1;
                var item = new ListViewItem(new[] {bookmark.Name, iline.ToString(), tb[bookmark.LineIndex].Text});
                item.Tag = bookmark;
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