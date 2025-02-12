using System.Diagnostics;
using ArcherTools_0._0._1.cfg;

namespace ArcherTools_0._0._1.logging
{
    public class ContainerReport
    {
        private static string ReportFilePath = Path.Combine(ConfigData.appContainersFolder, "Logging");
        internal classes.Container _Container {  get; set; }

        internal int _ContainerRelease;

        internal DateTime ReportTime { get; set; }

        internal string Body { get; set; }

        public ContainerReport(classes.Container container, int ContainerRelease = 0)
        {
            _Container = container;
            _ContainerRelease = ContainerRelease == 0 ? classes.Container.SelectedRelease : ContainerRelease;
        }

        private static void CreateDirectory()
        {
            if (Directory.Exists(ReportFilePath))
            {
                return;
            }
            else
            {
                Directory.CreateDirectory(ReportFilePath);
            }
        }

        public Task GenerateReport()
        {
            if (!Directory.Exists(ReportFilePath))
            {
                CreateDirectory();
            }
            try
            {
                this.ReportTime = DateTime.Now;
                _Container.CalculateExpectedSize();
                _Container.UpdateContainerStatus();
                
                this.Body = $"Ctr#{this._Container.ContainerId}-{classes.Container.ReleaseToString(_ContainerRelease)}";

                if (_Container.ContainerStatus != "Complete")
                {
                    if (_Container.ContainerStatus == "Incomplete")
                    {
                        this.Body += $" is incomplete and missing configurations at lines: ";
                        foreach (var line in _Container.AttachedConfigurations[_ContainerRelease])
                        {
                            if (!_Container.ReleasesAndItems[_ContainerRelease].Keys.Contains(line.Key))
                            {
                                this.Body += $"{line.Key.ToString()} ";
                            }
                        }
                    }
                    else
                    {
                        this.Body += " is empty and waiting to be loaded.";
                    }
                }
                else
                {
                    this.Body += $" is complete, all configurations are done.";
                }
                this.Body += $"\n\nReport Generated at: {ReportTime.ToString()} via Zenith";
                File.WriteAllLinesAsync(Path.Combine(ReportFilePath, $"{_Container.ContainerId}-{_ContainerRelease}.txt"), Body.Split('\n'));
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + "\n While generating report.");
                return Task.FromException(ex);
            }        
        }

        public Task GenerateEmailMessage()
        {
            if (!File.Exists(Path.Combine(ReportFilePath, $"{this._Container.ContainerId}-{_ContainerRelease}.txt")))
            {
                Debug.WriteLine(Path.Combine(ReportFilePath, $"{this._Container.ContainerId}-{_ContainerRelease}"));
                return Task.CompletedTask;
            }
            else
            {
                string[] Message = File.ReadAllLines(Path.Combine(ReportFilePath, $"{this._Container.ContainerId}-{_ContainerRelease}.txt"));
                var _message = string.Empty;

                _message += $"Hello team,\n\n";
                var getSplitMessage = Message[0].Split(' ');
                var ctrLine = $"{getSplitMessage[0]} ";
                _message += ctrLine;
                for (int i = 1; getSplitMessage.Length > i; i++)
                {
                    _message += getSplitMessage[i] + " ";
                }
                try
                {
                    Clipboard.SetText(_message);
                }
                catch { }
                Debug.WriteLine(_message);
                return Task.CompletedTask;
            }
        }
    }

    
}
