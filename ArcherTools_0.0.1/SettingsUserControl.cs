using System.Diagnostics;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.methods;

namespace ArcherTools_0._0._1
{
    public partial class SettingsUserControl : UserControl
    {
        public static SettingsUserControl _instance;
        private static BindingSource presetBs;
        public SettingsUserControl()
        {
            InitializeComponent();
            this.Load += onLoad;
            presetBs = new BindingSource();
            presetBs.DataSource = ColorPresets._instance.Presets;
            
        }

        private void onLoad(object sender, EventArgs e)
        {
            _instance = this;
            this.FindForm().FormClosing += form_OnClosing;
            presetList.Height = presetList.MinimumSize.Height;
            selPreset_lbl.Location = new Point(selPreset_lbl.Location.X, presetList_dropbtn.Location.Y + presetList_dropbtn.Size.Height + presetList.Size.Height + selPreset_lbl.Size.Height);
            var toolCfg = ConfigData._toolConfig;
            this.Invalidated += form_Invalidated;
            this.presetList.DataSource = ColorPresets._instance.Presets;
            if (toolCfg != null)
            {
                voicelines_checkbtn.Checked = toolCfg.EnableVoiceLines;
                autocreatecfg_checkbox.Checked = toolCfg.AutomaticCreateConfig;
                checkfordefault_checkbox.Checked = toolCfg.CheckForDefault;

            }
            
            overlayTip_lbl.Visible = false;
            updateSelectedPreset();



        }

        private void form_OnClosing(object sender, FormClosingEventArgs e)
        {
            _instance.Dispose();
            _instance = null;
        }

        private void form_Invalidated(object sender, EventArgs e)
        {
            updateSelectedPreset();
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


        private void updateSelectedPreset()
        {
            ColorPresets presetInstance = ColorPresets._instance;
            presetBs.ResetBindings(false);
            if (presetInstance != null && presetInstance.Presets.Count > 0)
            {
                selPreset_lbl.Text = selPreset_lbl.Text.Split(':')[0] += $": {presetInstance.SelectedPreset}";
                Properties.Settings.Default.SelectedPreset = presetInstance.SelectedPreset.PresetName;
                Properties.Settings.Default.Save();

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
                selPreset_lbl.Location = new Point(selPreset_lbl.Location.X, selPreset_lbl.Location.Y + 14);
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
                selPreset_lbl.Location = new Point(selPreset_lbl.Location.X, selPreset_lbl.Location.Y - 14);
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
