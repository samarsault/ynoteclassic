using System;

namespace WeifenLuo.WinFormsUI.Docking
{
    public partial class DockPanel
    {
        private DockPanelSkin m_dockPanelSkin = VS2012LightTheme.CreateVisualStudio2012Light();

        private ThemeBase m_dockPanelTheme = new VS2012LightTheme();

        [LocalizedCategory("Category_Docking")]
        [LocalizedDescription("DockPanel_DockPanelSkin")]
        [Obsolete("Please use Theme instead.")]
        public DockPanelSkin Skin
        {
            get { return m_dockPanelSkin; }
            set { m_dockPanelSkin = value; }
        }

        [LocalizedCategory("Category_Docking")]
        [LocalizedDescription("DockPanel_DockPanelTheme")]
        public ThemeBase Theme
        {
            get { return m_dockPanelTheme; }
            set
            {
                if (value == null)
                {
                    return;
                }

                if (m_dockPanelTheme.GetType() == value.GetType())
                {
                    return;
                }

                m_dockPanelTheme = value;
                m_dockPanelTheme.Apply(this);
            }
        }
    }
}