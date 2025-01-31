namespace ArcherTools_0._0._1
{
    public partial class MainArcherWindow : Form
    {

        private PageHandler pageHandler;
        

        public MainArcherWindow()
        {
            InitializeComponent();
            PageHandler pagehandler = PageHandler.GetInstance(mainPanel);
            pagehandler.LoadUserControl(new ToolHub());
        }
    }
}
