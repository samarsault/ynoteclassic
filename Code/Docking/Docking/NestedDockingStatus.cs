using System.Drawing;

namespace WeifenLuo.WinFormsUI.Docking
{
    public sealed class NestedDockingStatus
    {
        private readonly DockPane m_dockPane;
        private DockAlignment m_alignment = DockAlignment.Left;
        private DockAlignment m_displayingAlignment = DockAlignment.Left;
        private DockPane m_displayingPreviousPane;
        private double m_displayingProportion = 0.5;
        private bool m_isDisplaying;
        private Rectangle m_logicalBounds = Rectangle.Empty;

        private NestedPaneCollection m_nestedPanes;
        private Rectangle m_paneBounds = Rectangle.Empty;

        private DockPane m_previousPane;
        private double m_proportion = 0.5;
        private Rectangle m_splitterBounds = Rectangle.Empty;

        internal NestedDockingStatus(DockPane pane)
        {
            m_dockPane = pane;
        }

        public DockPane DockPane
        {
            get { return m_dockPane; }
        }

        public NestedPaneCollection NestedPanes
        {
            get { return m_nestedPanes; }
        }

        public DockPane PreviousPane
        {
            get { return m_previousPane; }
        }

        public DockAlignment Alignment
        {
            get { return m_alignment; }
        }

        public double Proportion
        {
            get { return m_proportion; }
        }

        public bool IsDisplaying
        {
            get { return m_isDisplaying; }
        }

        public DockPane DisplayingPreviousPane
        {
            get { return m_displayingPreviousPane; }
        }

        public DockAlignment DisplayingAlignment
        {
            get { return m_displayingAlignment; }
        }

        public double DisplayingProportion
        {
            get { return m_displayingProportion; }
        }

        public Rectangle LogicalBounds
        {
            get { return m_logicalBounds; }
        }

        public Rectangle PaneBounds
        {
            get { return m_paneBounds; }
        }

        public Rectangle SplitterBounds
        {
            get { return m_splitterBounds; }
        }

        internal void SetStatus(NestedPaneCollection nestedPanes, DockPane previousPane, DockAlignment alignment,
            double proportion)
        {
            m_nestedPanes = nestedPanes;
            m_previousPane = previousPane;
            m_alignment = alignment;
            m_proportion = proportion;
        }

        internal void SetDisplayingStatus(bool isDisplaying, DockPane displayingPreviousPane,
            DockAlignment displayingAlignment, double displayingProportion)
        {
            m_isDisplaying = isDisplaying;
            m_displayingPreviousPane = displayingPreviousPane;
            m_displayingAlignment = displayingAlignment;
            m_displayingProportion = displayingProportion;
        }

        internal void SetDisplayingBounds(Rectangle logicalBounds, Rectangle paneBounds, Rectangle splitterBounds)
        {
            m_logicalBounds = logicalBounds;
            m_paneBounds = paneBounds;
            m_splitterBounds = splitterBounds;
        }
    }
}