using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.methods;

namespace ArcherTools_0._0._1
{
    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();
            this.Load += onLoad;
        }

        private void onLoad(object sender, EventArgs e)
        {
            var toolCfg = ConfigData._toolConfig;
            if (toolCfg != null)
            {
                voicelines_checkbtn.Checked = toolCfg.EnableVoiceLines;
            }
            

            
        }

        private void rcvSet_Btn_Click(object sender, EventArgs e)
        {
            Receiving.TrainCall();
        }

        private void return_Btn_Click(object sender, EventArgs e)
        {
            ToolConfig toolConfig = new ToolConfig(voicelines_checkbtn.Checked);
            if (toolConfig.ConfigIsDifferent(ConfigData._toolConfig))
            {
                ConfigData.setToolConfig(toolConfig);
                ConfigData cfgData = new ConfigData(ConfigData._userConfig, ConfigData._receivingConfig, ConfigData._toolConfig);
                cfgData.PrepareForSerialization();
                ConfigData.SerializeConfigData();
            }
            
            PageHandler pageHandler = PageHandler.GetInstance();
            this.FindForm().Size = ToolHub.ucSize;
            pageHandler.LoadUserControl(new ToolHub());
        }
    }
}
