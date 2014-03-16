#region

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

public class WizardTabControl : TabControl
{
    private bool _tabsVisible;

    [DefaultValue(false)]
    public bool TabsVisible
    {
        get { return _tabsVisible; }
        set
        {
            if (_tabsVisible == value) return;
            _tabsVisible = value;
            RecreateHandle();
        }
    }

    protected override void WndProc(ref Message m)
    {
        // Hide tabs by trapping the TCM_ADJUSTRECT message
        if (m.Msg == 0x1328)
        {
            if (!_tabsVisible && !DesignMode)
            {
                m.Result = (IntPtr) 1;
                return;
            }
        }
        base.WndProc(ref m);
    }
}