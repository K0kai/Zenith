using ArcherTools_0._0._1.boxes;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.controllers;
using ArcherTools_0._0._1.errors;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ArcherTools_0._0._1.methods
{
    internal class VPNConnect
    {
        // Sys Methods

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(nint hWnd);

        // VPN Information
        const string vpnPath = "C:\\Program Files\\SonicWall\\Global VPN Client";
        const string vpnShortcutName = "SWGVC.exe";
        static string vpnShortcutPath = Path.Combine(vpnPath, vpnShortcutName);

        static internal void ConnectToVPN()
        {
            if (ConfigData._userConfig == null)
            {
                MessageBox.Show("Seems like you haven't set up your VPN Configurations yet, so we'll begin with that");
                var returnCode = SetUpVPNConfig();
                if (returnCode != (byte)ErrorEnum.ErrorCode.Success)
                {
                    MessageBox.Show("There was an error setting up your VPN, please try again.", $"{ (ErrorEnum.ErrorCode) returnCode}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                Process[] processes = Process.GetProcessesByName(vpnShortcutName);
                if (processes.Length <= 0)
                {
                    Process.Start(vpnShortcutPath);
                    Thread.Sleep(500);
                    processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(vpnShortcutName));

                }
                nint hWnd = new nint(nint.Zero);
                while (hWnd == nint.Zero && !processes[0].HasExited)
                {
                    Thread.Sleep(100);
                    hWnd = processes[0].MainWindowHandle;
                }
                Thread.Sleep(500);
                SetForegroundWindow(hWnd);
                Rectangle rect = WindowHandler.GetWindowRectFromHandle(hWnd);
                Debug.WriteLine(rect.Y);
                Thread.Sleep(1000);
                Point findIp = ScreenImageHandler.SearchImageOnRegion("C:\\Users\\Archer\\source\\repos\\ArcherTools_0.0.1\\ArcherTools_0.0.1\\img\\find\\vpn_ip.png", rect, 0.95);
                if (findIp != new Point(0, 0))
                {
                    MouseHandler.SetCursorPos(findIp.X + 20, findIp.Y + 5);
                    MouseHandler.MouseClick(clickType.DoubleLeftClick);
                }
                else
                {
                    findIp = ScreenImageHandler.SearchImageOnRegion("C:\\Users\\Archer\\source\\repos\\ArcherTools_0.0.1\\ArcherTools_0.0.1\\img\\find\\vpn_ipActivated.png", rect, 0.95);
                    MouseHandler.SetCursorPos(findIp.X + 20, findIp.Y + 5);
                    MouseHandler.MouseClick(clickType.DoubleLeftClick);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        public static byte SetUpVPNConfig()
        {
            if (ConfigData._userConfig == null)
            {
                Form mainForm = ToolHub._mainForm;
                mainForm.Enabled = false;
                List<string> boxNames = new List<string>
                {
                    "VPN Username",
                    "VPN Password"
                };
                var UserInputs = DynamicInputBoxForm.Show("Enter these values:", boxNames);
                mainForm.Enabled = true;
                if (UserInputs != null)
                {
                    foreach (var data in UserInputs)
                    {
                        if (data.Equals("VPN Username") || data.Equals("VPN Password"))
                        {
                            return (byte)ErrorEnum.ErrorCode.InvalidInput;
                        }
                    }
                }
                if (UserInputs?.Count == 2)
                {
                    try
                    {
                        UserConfig newUserConfig = new UserConfig(UserInputs[0], UserInputs[1]);
                        ConfigData.setUserConfig(newUserConfig);
                        ConfigData configData = new ConfigData(ConfigData._userConfig, ConfigData._receivingConfig, ConfigData._toolConfig);
                        configData.PrepareForSerialization();
                        ConfigData.SerializeConfigData();
                        return (byte)ErrorEnum.ErrorCode.Success;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("An error ocurred while setting up VPN Connections...");
                        Debug.WriteLine(ex.Message);
                        return (byte)ErrorEnum.ErrorCode.UnknownError;


                    }
                }

            }
            return (byte)ErrorEnum.ErrorCode.InvalidInput;
        }
    }
}
    

