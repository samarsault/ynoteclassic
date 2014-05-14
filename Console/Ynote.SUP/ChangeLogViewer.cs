using System.Windows.Forms;

namespace SUP.Tester
{
    public partial class ChangeLogViewer : Form
    {
        public ChangeLogViewer(string link)
        {
            InitializeComponent();
            wb.Navigate(link);
        }
    }
}