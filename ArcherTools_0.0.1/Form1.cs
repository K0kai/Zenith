namespace ArcherTools_0._0._1
{
    public partial class MainArcherWindow : Form
    {

        private PageHandler pageHandler;
        public static string programVersion = "0.1.0";

        public MainArcherWindow()
        {
            InitializeComponent();
            this.FindForm().Text += $" {programVersion}";
            PageHandler pagehandler = PageHandler.GetInstance(mainPanel);
            pagehandler.LoadUserControl(new ToolHub());
        }
    }
}
