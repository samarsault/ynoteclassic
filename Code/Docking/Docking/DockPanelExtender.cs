using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

namespace WeifenLuo.WinFormsUI.Docking
{
    public sealed class DockPanelExtender
    {
        private readonly DockPanel m_dockPanel;
        private IDockOutlineFactory m_DockOutlineFactory;
        private IPaneIndicatorFactory m_PaneIndicatorFactory;
        private IPanelIndicatorFactory m_PanelIndicatorFactory;
        private IAutoHideStripFactory m_autoHideStripFactory;
        private IAutoHideWindowFactory m_autoHideWindowFactory;
        private IDockPaneCaptionFactory m_dockPaneCaptionFactory;

        private IDockPaneFactory m_dockPaneFactory;

        private IDockPaneSplitterControlFactory m_dockPaneSplitterControlFactory;
        private IDockPaneStripFactory m_dockPaneStripFactory;
        private IDockWindowFactory m_dockWindowFactory;

        private IDockWindowSplitterControlFactory m_dockWindowSplitterControlFactory;

        private IFloatWindowFactory m_floatWindowFactory;

        internal DockPanelExtender(DockPanel dockPanel)
        {
            m_dockPanel = dockPanel;
        }

        private DockPanel DockPanel
        {
            get { return m_dockPanel; }
        }

        public IDockPaneFactory DockPaneFactory
        {
            get
            {
                if (m_dockPaneFactory == null)
                    m_dockPaneFactory = new DefaultDockPaneFactory();

                return m_dockPaneFactory;
            }
            set
            {
                if (DockPanel.Panes.Count > 0)
                    throw new InvalidOperationException();

                m_dockPaneFactory = value;
            }
        }

        public IDockPaneSplitterControlFactory DockPaneSplitterControlFactory
        {
            get
            {
                return m_dockPaneSplitterControlFactory ??
                       (m_dockPaneSplitterControlFactory = new DefaultDockPaneSplitterControlFactory());
            }

            set
            {
                if (DockPanel.Panes.Count > 0)
                {
                    throw new InvalidOperationException();
                }

                m_dockPaneSplitterControlFactory = value;
            }
        }

        public IDockWindowSplitterControlFactory DockWindowSplitterControlFactory
        {
            get
            {
                return m_dockWindowSplitterControlFactory ??
                       (m_dockWindowSplitterControlFactory = new DefaultDockWindowSplitterControlFactory());
            }

            set
            {
                m_dockWindowSplitterControlFactory = value;
                DockPanel.ReloadDockWindows();
            }
        }

        public IFloatWindowFactory FloatWindowFactory
        {
            get
            {
                if (m_floatWindowFactory == null)
                    m_floatWindowFactory = new DefaultFloatWindowFactory();

                return m_floatWindowFactory;
            }
            set
            {
                if (DockPanel.FloatWindows.Count > 0)
                    throw new InvalidOperationException();

                m_floatWindowFactory = value;
            }
        }

        public IDockWindowFactory DockWindowFactory
        {
            get { return m_dockWindowFactory ?? (m_dockWindowFactory = new DefaultDockWindowFactory()); }
            set
            {
                m_dockWindowFactory = value;
                DockPanel.ReloadDockWindows();
            }
        }

        public IDockPaneCaptionFactory DockPaneCaptionFactory
        {
            get
            {
                if (m_dockPaneCaptionFactory == null)
                    m_dockPaneCaptionFactory = new DefaultDockPaneCaptionFactory();

                return m_dockPaneCaptionFactory;
            }
            set
            {
                if (DockPanel.Panes.Count > 0)
                    throw new InvalidOperationException();

                m_dockPaneCaptionFactory = value;
            }
        }

        public IDockPaneStripFactory DockPaneStripFactory
        {
            get
            {
                if (m_dockPaneStripFactory == null)
                    m_dockPaneStripFactory = new DefaultDockPaneStripFactory();

                return m_dockPaneStripFactory;
            }
            set
            {
                if (DockPanel.Contents.Count > 0)
                    throw new InvalidOperationException();

                m_dockPaneStripFactory = value;
            }
        }

        public IAutoHideStripFactory AutoHideStripFactory
        {
            get
            {
                if (m_autoHideStripFactory == null)
                    m_autoHideStripFactory = new DefaultAutoHideStripFactory();

                return m_autoHideStripFactory;
            }
            set
            {
                if (DockPanel.Contents.Count > 0)
                    throw new InvalidOperationException();

                if (m_autoHideStripFactory == value)
                    return;

                m_autoHideStripFactory = value;
                DockPanel.ResetAutoHideStripControl();
            }
        }

        public IAutoHideWindowFactory AutoHideWindowFactory
        {
            get { return m_autoHideWindowFactory ?? (m_autoHideWindowFactory = new DefaultAutoHideWindowFactory()); }
            set
            {
                if (DockPanel.Contents.Count > 0)
                {
                    throw new InvalidOperationException();
                }

                if (m_autoHideWindowFactory == value)
                {
                    return;
                }

                m_autoHideWindowFactory = value;
                DockPanel.ResetAutoHideStripWindow();
            }
        }

        public IPaneIndicatorFactory PaneIndicatorFactory
        {
            get { return m_PaneIndicatorFactory ?? (m_PaneIndicatorFactory = new DefaultPaneIndicatorFactory()); }
            set { m_PaneIndicatorFactory = value; }
        }

        public IPanelIndicatorFactory PanelIndicatorFactory
        {
            get { return m_PanelIndicatorFactory ?? (m_PanelIndicatorFactory = new DefaultPanelIndicatorFactory()); }
            set { m_PanelIndicatorFactory = value; }
        }

        public IDockOutlineFactory DockOutlineFactory
        {
            get { return m_DockOutlineFactory ?? (m_DockOutlineFactory = new DefaultDockOutlineFactory()); }
            set { m_DockOutlineFactory = value; }
        }

        public class DefaultDockOutlineFactory : IDockOutlineFactory
        {
            public DockOutlineBase CreateDockOutline()
            {
                return new DockPanel.DefaultDockOutline();
            }
        }

        public class DefaultPaneIndicatorFactory : IPaneIndicatorFactory
        {
            public DockPanel.IPaneIndicator CreatePaneIndicator()
            {
                return new DockPanel.DefaultPaneIndicator();
            }
        }

        public class DefaultPanelIndicatorFactory : IPanelIndicatorFactory
        {
            public DockPanel.IPanelIndicator CreatePanelIndicator(DockStyle style)
            {
                return new DockPanel.DefaultPanelIndicator(style);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public interface IAutoHideStripFactory
        {
            AutoHideStripBase CreateAutoHideStrip(DockPanel panel);
        }

        public interface IAutoHideWindowFactory
        {
            DockPanel.AutoHideWindowControl CreateAutoHideWindow(DockPanel panel);
        }

        public interface IDockOutlineFactory
        {
            DockOutlineBase CreateDockOutline();
        }

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public interface IDockPaneCaptionFactory
        {
            DockPaneCaptionBase CreateDockPaneCaption(DockPane pane);
        }

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public interface IDockPaneFactory
        {
            DockPane CreateDockPane(IDockContent content, DockState visibleState, bool show);

            [SuppressMessage("Microsoft.Naming", "CA1720:AvoidTypeNamesInParameters", MessageId = "1#")]
            DockPane CreateDockPane(IDockContent content, FloatWindow floatWindow, bool show);

            DockPane CreateDockPane(IDockContent content, DockPane previousPane, DockAlignment alignment,
                double proportion, bool show);

            [SuppressMessage("Microsoft.Naming", "CA1720:AvoidTypeNamesInParameters", MessageId = "1#")]
            DockPane CreateDockPane(IDockContent content, Rectangle floatWindowBounds, bool show);
        }

        public interface IDockPaneSplitterControlFactory
        {
            DockPane.SplitterControlBase CreateSplitterControl(DockPane pane);
        }

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public interface IDockPaneStripFactory
        {
            DockPaneStripBase CreateDockPaneStrip(DockPane pane);
        }

        public interface IDockWindowFactory
        {
            DockWindow CreateDockWindow(DockPanel dockPanel, DockState dockState);
        }

        public interface IDockWindowSplitterControlFactory
        {
            SplitterBase CreateSplitterControl();
        }

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public interface IFloatWindowFactory
        {
            FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane);

            FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds);
        }

        public interface IPaneIndicatorFactory
        {
            DockPanel.IPaneIndicator CreatePaneIndicator();
        }

        public interface IPanelIndicatorFactory
        {
            DockPanel.IPanelIndicator CreatePanelIndicator(DockStyle style);
        }

        #region DefaultDockPaneFactory

        private class DefaultDockPaneFactory : IDockPaneFactory
        {
            public DockPane CreateDockPane(IDockContent content, DockState visibleState, bool show)
            {
                return new DockPane(content, visibleState, show);
            }

            public DockPane CreateDockPane(IDockContent content, FloatWindow floatWindow, bool show)
            {
                return new DockPane(content, floatWindow, show);
            }

            public DockPane CreateDockPane(IDockContent content, DockPane prevPane, DockAlignment alignment,
                double proportion, bool show)
            {
                return new DockPane(content, prevPane, alignment, proportion, show);
            }

            public DockPane CreateDockPane(IDockContent content, Rectangle floatWindowBounds, bool show)
            {
                return new DockPane(content, floatWindowBounds, show);
            }
        }

        #endregion DefaultDockPaneFactory

        #region DefaultDockPaneSplitterControlFactory

        private class DefaultDockPaneSplitterControlFactory : IDockPaneSplitterControlFactory
        {
            public DockPane.SplitterControlBase CreateSplitterControl(DockPane pane)
            {
                return new DockPane.DefaultSplitterControl(pane);
            }
        }

        #endregion DefaultDockPaneSplitterControlFactory

        #region DefaultDockWindowSplitterControlFactory

        private class DefaultDockWindowSplitterControlFactory : IDockWindowSplitterControlFactory
        {
            public SplitterBase CreateSplitterControl()
            {
                return new DockWindow.DefaultSplitterControl();
            }
        }

        #endregion DefaultDockWindowSplitterControlFactory

        #region DefaultFloatWindowFactory

        private class DefaultFloatWindowFactory : IFloatWindowFactory
        {
            public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane)
            {
                return new FloatWindow(dockPanel, pane);
            }

            public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds)
            {
                return new FloatWindow(dockPanel, pane, bounds);
            }
        }

        #endregion DefaultFloatWindowFactory

        #region DefaultDockWindowFactory

        private class DefaultDockWindowFactory : IDockWindowFactory
        {
            public DockWindow CreateDockWindow(DockPanel dockPanel, DockState dockState)
            {
                return new DefaultDockWindow(dockPanel, dockState);
            }
        }

        #endregion DefaultDockWindowFactory

        #region DefaultDockPaneCaptionFactory

        private class DefaultDockPaneCaptionFactory : IDockPaneCaptionFactory
        {
            public DockPaneCaptionBase CreateDockPaneCaption(DockPane pane)
            {
                return new VS2012LightDockPaneCaption(pane);
            }
        }

        #endregion DefaultDockPaneCaptionFactory

        #region DefaultDockPaneTabStripFactory

        private class DefaultDockPaneStripFactory : IDockPaneStripFactory
        {
            public DockPaneStripBase CreateDockPaneStrip(DockPane pane)
            {
                return new VS2012LightDockPaneStrip(pane);
            }
        }

        #endregion DefaultDockPaneTabStripFactory

        #region DefaultAutoHideStripFactory

        private class DefaultAutoHideStripFactory : IAutoHideStripFactory
        {
            public AutoHideStripBase CreateAutoHideStrip(DockPanel panel)
            {
                return new VS2012LightAutoHideStrip(panel);
            }
        }

        #endregion DefaultAutoHideStripFactory

        #region DefaultAutoHideWindowFactory

        public class DefaultAutoHideWindowFactory : IAutoHideWindowFactory
        {
            public DockPanel.AutoHideWindowControl CreateAutoHideWindow(DockPanel panel)
            {
                return new DockPanel.DefaultAutoHideWindowControl(panel);
            }
        }

        #endregion DefaultAutoHideWindowFactory
    }
}