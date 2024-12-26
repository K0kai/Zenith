namespace ArcherTools_0._0._1
{
    public class PageHandler
    {
        private static PageHandler instance;
        private Panel _mainPanel;

        private static readonly object Lock = new object();
        public PageHandler(Panel mainPanel) { 

        _mainPanel = mainPanel;

        }

        public static PageHandler GetInstance(Panel mainPanel = null)
        {
            if (instance == null)
            {
                if (mainPanel == null)
                {
                    throw new InvalidOperationException("You must provide a Panel when initializing PageHandler for the first time.");
                }

                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new PageHandler(mainPanel);
                    }
                }
            }

            return instance;
        }

        public Panel GetMainPanel()
        {
            return _mainPanel;
        }


        public void LoadUserControl(UserControl userControl)
        {
            _mainPanel.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            _mainPanel.Controls.Add(userControl);
        }
    }
}
