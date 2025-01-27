using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            presetList.Height = presetList.MinimumSize.Height;
            var toolCfg = ConfigData._toolConfig;
            if (toolCfg != null)
            {
                voicelines_checkbtn.Checked = toolCfg.EnableVoiceLines;
                autocreatecfg_checkbox.Checked = toolCfg.AutomaticCreateConfig;
                checkfordefault_checkbox.Checked = toolCfg.CheckForDefault;

            }
            updatePresetList();



        }

        private void rcvSet_Btn_Click(object sender, EventArgs e)
        {
            Receiving.TrainCall();
        }

        private void return_Btn_Click(object sender, EventArgs e)
        {
            ToolConfig toolConfig = new ToolConfig(voicelines_checkbtn.Checked, autocreatecfg_checkbox.Checked, checkfordefault_checkbox.Checked);
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

        private void presetList_dropbtn_Click(object sender, EventArgs e)
        {
            presetDropTimer.Start();
            
        }

        private void updatePresetList()
        {
            ColorPresets presetInstance = ColorPresets._instance;
            if (presetInstance != null)
            {
                presetList.Items.Add(presetInstance.Presets[0]);
            }
        }

        bool expand = false;
        private void presetDropTimer_Tick(object sender, EventArgs e)
        {

            if (!expand)
            {
                presetList.Height += 15;
                if (presetList.Height >= presetList.MaximumSize.Height)
                {
                    presetList.Height = presetList.MaximumSize.Height;
                    presetDropTimer.Stop();
                    expand = true;
                }
            }
            else
            {
                presetList.Height -= 15;
                if (presetList.Height <= presetList.MinimumSize.Height)
                {
                    presetList.Height = presetList.MinimumSize.Height;
                    presetDropTimer.Stop();
                    expand = false;
                }
            }
        }
    }
}
