using ArcherTools_0._0._1.cfg;
using NPOI.SS.Formula.Functions;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Windows.Forms;

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
