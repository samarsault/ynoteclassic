using System;
using System.Windows.Forms;

namespace AutocompleteMenuNS
{
    internal class AutocompleteMenuHost : ToolStripDropDown
    {
        private IAutocompleteListView listView;
        public ToolStripControlHost Host { get; set; }

        public IAutocompleteListView ListView { 
            get { return listView; }
            set {
                if (value == null)
                    listView = new AutocompleteListView();
                else
                {
                    if (!(value is Control))
                        throw new Exception("ListView must be derived from Control class");

                    listView = value;
                }

                Host = new ToolStripControlHost(ListView as Control);
                Host.Margin = new Padding(2, 2, 2, 2);
                Host.Padding = Padding.Empty;
                Host.AutoSize = false;
                Host.AutoToolTip = false;

                (ListView as Control).MaximumSize = Menu.MaximumSize;
                (ListView as Control).Size = Menu.MaximumSize;

                CalcSize();
                base.Items.Clear();
                base.Items.Add(Host);
                (ListView as Control).Parent = this;
            }
        }
        public readonly AutocompleteMenu Menu;

        public AutocompleteMenuHost(AutocompleteMenu menu)
        {
            AutoClose = false;
            AutoSize = false;
            Margin = Padding.Empty;
            Padding = Padding.Empty;

            Menu = menu;
            ListView = new AutocompleteListView();
        }

        internal void CalcSize()
        {
            Host.Size = (ListView as Control).Size;
            Size = new System.Drawing.Size((ListView as Control).Size.Width + 4, (ListView as Control).Size.Height + 4);
        }

        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
                (ListView as Control).RightToLeft = value;
            }
        }
    }
}
