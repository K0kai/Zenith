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
          
            overlayTip_lbl.Visible = false;
            updatePresetList();



        }

        private void rcvSet_Btn_Click(object sender, EventArgs e)
        {
            overlayTip_lbl.Visible = true;
            Task receiveSet = Task.Run(() =>
            {                
                Receiving.TrainCall();
            });
            Task.WaitAll(receiveSet);
            overlayTip_lbl.Visible = false;
            ReceivingConfig newRcvCfg = new ReceivingConfig(ConfigData._receivingConfig);
            DialogResult excelConfirmation = MessageBox.Show("Lastly, we are going to set up your excel that will be used for the item configurations", "Last Step", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (excelConfirmation != DialogResult.OK)
            {
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel Sheet (*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.Multiselect = false;
            string filePath;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
                newRcvCfg.setExcelFilePath(filePath);

                if (newRcvCfg.ConfigIsDifferent(ConfigData._receivingConfig))
                {
                    ConfigData cfgData = new ConfigData(ConfigData._userConfig, newRcvCfg, ConfigData._toolConfig);
                    cfgData.PrepareForSerialization();
                    ConfigData.SerializeConfigData();
                }
            }
            else
            {
                return;
            }
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

        private void presetDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (presetList.SelectedItem != null)
                {
                    ColorPresets._instance.SetPreset(presetList.SelectedItem);
                    Debug.WriteLine(ColorPresets._instance.GetCurrentPreset().ToString());
                }
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
