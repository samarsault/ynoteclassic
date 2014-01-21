using System;
using System.ComponentModel;
using System.Windows.Forms;

public class WizardTabControl : TabControl
{
    private bool tabsVisible;

    [DefaultValue(false)]
    public bool TabsVisible
    {
        get { return tabsVisible; }
        set
        {
            if (tabsVisible == value) return;
            tabsVisible = value;
            RecreateHandle();
        }
    }
    protected override void WndProc(ref Message m)
    {
        // Hide tabs by trapping the TCM_ADJUSTRECT message
        if (m.Msg == 0x1328)
        {
            if (!tabsVisible && !DesignMode)
            {
                m.Result = (IntPtr)1;
                return;
            }
        }
        base.WndProc(ref m);
    }
}