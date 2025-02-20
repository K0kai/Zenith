using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.enums;
using ArcherTools_0._0._1.forms;

namespace ArcherTools_0._0._1.classes
{
    public class Container
    {
        public static Container SelectedContainer
        {
            get => selectedContainer;
            set
            {
                selectedContainer = value;
                OnStaticPropertyChanged(nameof(SelectedContainer));
            }
        }
      
   
        private static Container selectedContainer;
        public static int SelectedRelease
        {
            get => selectedRelease;
            set
            {
                selectedRelease = value;
                OnStaticPropertyChanged(nameof(SelectedRelease));
            }
        }

        private static int selectedRelease;

        public static List<Container> AllContainers { get; set; } = new List<Container>();
        public string ContainerId { get; set; }
        public string Owner {  get; set; }

        [JsonConverter(typeof(ConcurrentDictionaryConverter))]
        public ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> ReleasesAndItems { get; set; }
        public ConcurrentDictionary<int, ConcurrentDictionary<int, string>> AttachedConfigurations
        {
            get => attachedConfigurations;
            set
            {                
                attachedConfigurations = value;
                OnPropertyChanged(nameof(AttachedConfigurations));
            }
        }
        private ConcurrentDictionary<int, ConcurrentDictionary<int, string>> attachedConfigurations = new ConcurrentDictionary<int, ConcurrentDictionary<int, string>>();
        public int ExpectedSize
        {
            get => expectedSize;
            set
            {
                expectedSize = value;
                OnPropertyChanged(nameof(ExpectedSize));
            }
        }
        private int expectedSize;
        public string ContainerStatus
        {
            get => containerStatus;
            set
            {
                containerStatus = value;
                OnPropertyChanged(nameof(ContainerStatus));
            }
        }
        private string containerStatus;

        public static event PropertyChangedEventHandler? StaticPropertyChanged;

        public event PropertyChangedEventHandler? PropertyChanged;


        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }
        public static void OnStaticPropertyChanged([CallerMemberName] string propertyName = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
            if (propertyName == "SelectedRelease")
            {
                if (SelectedContainer != null)
                {
                    if (ValidateSelectedContainerAndRelease() == 0)
                    {
                        SelectedContainer.CalculateExpectedSize();
                        SelectedContainer.UpdateContainerStatus();
                    }
                }
            }
        }

        public ConcurrentDictionary<int, Item>? GetContainerItems(int release)
        {
            try
            {
                if (ReleasesAndItems.TryGetValue(release, out var item)) return item;
                else { throw new Exception("Item list or release might not exist."); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Couldn't get item list from container {ContainerId}\nReason: {ex.Message}");
                return null;
            }
        }
        public static void SetSelectedRelease(object? release)
        {
            try
            {
                var convSelectedRelease = (int)release;
                if (SelectedContainer != null)
                {
                    if (SelectedContainer.ReleasesAndItems != null)
                    {
                        foreach (var releases in SelectedContainer.ReleasesAndItems.Keys.ToArray())
                        {
                            if (releases == convSelectedRelease)
                            {
                                SelectedRelease = convSelectedRelease;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine($"Failed to select release from string: {release}");
            }
        }

        public Container(string containerId, string owner, ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> releasesAndItems)
        {
            this.ContainerId = containerId;
            this.Owner = owner.ToUpper();
            this.ReleasesAndItems = releasesAndItems;
            if (AllContainers == null)
            {
                AllContainers = new List<Container>();
            }
            if (AttachedConfigurations == null)
            {
                AttachedConfigurations = new ConcurrentDictionary<int, ConcurrentDictionary<int, string>>();
            }
            AddContainer(this);
        }
        public Container()
        {
        }
        public Container (string containerId, string owner)
        {
            this.ContainerId = containerId;
            this.Owner = owner.ToUpper();
            if (AllContainers == null)
            {
                AllContainers = new List<Container>();
            }
            if (AttachedConfigurations == null)
            {
                AttachedConfigurations = new ConcurrentDictionary<int, ConcurrentDictionary<int, string>>();
            }
            if (ReleasesAndItems == null)
            {
                ReleasesAndItems = new ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> ();
            }
            AddContainer(this);

        }

        public override string ToString()
        {
            return this.ContainerId;
        }

        public static void ClearContainers()
        {
            AllContainers.Clear();
        }

        internal byte ValidateContainerAndRelease(int release)
        {
            if (this != null)
            {
                if (release != 0)
                {
                    if (this.ReleasesAndItems.ContainsKey(release))
                    {
                        return (byte)ReturnCodeEnum.ReturnCode.Success;
                    }
                }
            }
            Debug.WriteLine($"Error validating: {this.ToString()}'s release: {release}");
            return (byte)ReturnCodeEnum.ReturnCode.UnknownError;
        }
        internal static byte ValidateSelectedContainerAndRelease()
        {
            if (SelectedContainer != null)
            {
                if (SelectedRelease != null || SelectedRelease != 0)
                {
                    foreach (var releases in SelectedContainer.ReleasesAndItems.Keys.ToArray())
                    {
                        if (releases == SelectedRelease)
                        {
                            return (byte) ReturnCodeEnum.ReturnCode.Success;
                        }
                    }
                }
            }
            Debug.WriteLine($"Error validating selected container: {SelectedContainer?.ToString()}'s release: {selectedRelease}");
            return (byte) ReturnCodeEnum.ReturnCode.UnknownError;
        }

        public void UpdateContainerStatus(int release)
        {
            if (this.ValidateContainerAndRelease(release) == 0)
            {
                if (this.ContainerId != null && !string.IsNullOrWhiteSpace(this.ContainerId))
                {
                    if (this.ReleasesAndItems.Keys.Contains(release))
                    {
                        this.CalculateExpectedSize(release);
                        if (this.ReleasesAndItems[release].Count == 0)
                        {
                            if (this.ContainerStatus != "Empty")
                            {
                                this.ContainerStatus = "Empty";
                                this.OnPropertyChanged(nameof(ContainerStatus));
                                SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, this.ContainerId));
                                return;
                            }
                        }
                        else if (this.ReleasesAndItems[release].Count >= this.ExpectedSize)
                        {
                            if (!string.Equals(this.ContainerStatus, "Complete", StringComparison.CurrentCultureIgnoreCase))
                            {
                                this.ContainerStatus = "Complete";
                                this.OnPropertyChanged(nameof(ContainerStatus));
                                SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, this.ContainerId));
                                return;
                            }
                        }
                        else
                        {
                            if (!string.Equals(this.ContainerStatus, "Incomplete",StringComparison.CurrentCultureIgnoreCase))
                            {
                                this.ContainerStatus = "Incomplete";
                                this.OnPropertyChanged(nameof(ContainerStatus));
                                SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, this.ContainerId));
                                return;
                            }
                        }
                    }
                }                
            }
            else
            {
                //throw new InvalidOperationException($"Unable to validate {this.ToString()} and release {release}.");
            }
        }
        public void UpdateContainerStatus()
        {
            if (this.ContainerId == SelectedContainer.ContainerId)
            {
                if (SelectedContainer != null)
                {
                    if (SelectedContainer != null && SelectedRelease == null)
                    {
                        ContainerStatus = "No Release Selected";
                        this.OnPropertyChanged(nameof(ContainerStatus));
                        return;
                    }
                    else
                    {
                        if (SelectedContainer.ContainerId == this.ContainerId)
                        {
                            if (ReleasesAndItems[SelectedRelease].Count == 0)
                            {
                                if (ContainerStatus != "Empty")
                                {
                                    ContainerStatus = "Empty";
                                    this.OnPropertyChanged(nameof(ContainerStatus));
                                }
                                return;
                            }
                            else if (ReleasesAndItems[SelectedRelease].Count >= ExpectedSize)
                            {
                                if (ContainerStatus != "Complete")
                                {
                                    ContainerStatus = "Complete";
                                    this.OnPropertyChanged(nameof(ContainerStatus));
                                    return;
                                }
                            }
                            else
                            {
                                if (ContainerStatus != "Incomplete")
                                {
                                    ContainerStatus = "Incomplete";
                                    this.OnPropertyChanged(nameof(ContainerStatus));
                                }
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Failed to calculate container status, the passed container does not match the currently selected container", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        public void SetExpectedSize(int expectedSize)
        {
            if (expectedSize > 0)
            {
                ExpectedSize = expectedSize;
            }
        }

        public static string? ReleaseToString(int release)
        {
            string stringRelease;
            switch (release)
            {
                case 100:
                    stringRelease = "ND";
                    break;
                case 101:
                    stringRelease = "TEF";
                    break;
                case 102:
                    stringRelease = "BYD";
                    break;
                case 103:
                    stringRelease = "IMA";
                    break;
                case 104:
                    stringRelease = "BIO";
                    break;
                default:
                    stringRelease = release.ToString();
                    break;
            }
            return stringRelease;
        }

        public static int? ReleaseToInt(string release)
        {
            int intRelease = 0;
            switch (release.ToLower())
            {
                case "nd":
                    intRelease = 100;
                    break;
                case "tef":
                    intRelease = 101;
                    break;
                case "byd":
                    intRelease = 102;
                    break;
                case "ima":
                    intRelease = 103;
                    break;
                case "bio":
                    intRelease = 104;
                    break;
                default:
                    intRelease = int.Parse(release);
                    break;
            }
            return intRelease;
        }
        

        internal void CalculateExpectedSize()
        {
            if (SelectedContainer.AttachedConfigurations != null)
            {
                try
                {
                    var expectedSize = !this.AttachedConfigurations.ContainsKey(selectedRelease) || this.AttachedConfigurations[SelectedRelease].Count == 0 ? 0 : this.AttachedConfigurations[SelectedRelease].Count;
                    this.ExpectedSize = expectedSize;
                }
                catch (Exception ex)
                {
                }
            }
        }

        public async void CalculateExpectedSize(int release)
        {
            if (this.AttachedConfigurations != null)
            {
                try
                {
                    var expectedSize = !this.AttachedConfigurations.ContainsKey(release) || this.AttachedConfigurations[release].Count == 0 ? 0 : this.AttachedConfigurations[release].Count;
                    if (this.ExpectedSize != expectedSize)
                    {
                        this.ExpectedSize = expectedSize;
                        this.UpdateContainerStatus(release);
                        await SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, this.ContainerId));
                    }
                }
                catch(Exception ex)
                {

                }
            }
        }

        public async void AddItem(int release, int line, Item value)
        {
            if (this.ReleasesAndItems[release].TryAdd(line, value))
            {                
                Debug.WriteLine("Adding item");
                this.OnPropertyChanged(nameof(ReleasesAndItems));
                this.UpdateContainerStatus(release);
                await SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, this.ContainerId));

            }           
        }
        public static void AddContainer(Container container)
        {
            if (AllContainers == null)
            {
                AllContainers = new List<Container>();
            }
            foreach (var cont in AllContainers)
            {
                if (cont.ContainerId == container.ContainerId)
                {
                    try
                    {
                        if (cont.Owner?.Trim() != container.Owner?.Trim())
                        {
                            MessageBox.Show($"This container already exists and is owned by another customer\n(Old:{cont.ContainerId}-{cont.Owner} vs New:{container.ContainerId}-{container.Owner}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        var newRelease = new ConcurrentDictionary<int, Item>();
                        cont.ReleasesAndItems.TryAdd(container.ReleasesAndItems.Keys.ToArray()[0], newRelease);
                        OnStaticPropertyChanged(nameof(AllContainers));
                        return;
                    }
                    
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Failed to add new release.");
                    }
                }
            }
             
            AllContainers.Add(container);
            try
            {
                if (ReceivingGUI.containerListForm != null)
                {
                    ReceivingGUI.containerListForm.Invalidate();
                }
            }
            catch { }
        }


        public static void RemoveContainer(Container container)
        {
            if (AllContainers != null)
            {
                if (SelectedContainer != null && container.ContainerId == SelectedContainer.ContainerId)
                {
                    UndoSelectedContainer();
                }
               var containerNames = AllContainers.Select(x => x.ContainerId).ToList();
                if (containerNames.Contains(container.ContainerId))
                {
                    foreach(var cont in AllContainers)
                    {
                        if (cont.ContainerId == container.ContainerId)
                        {
                            AllContainers.Remove(cont);
                            break;
                        }
                    }
                }
                try
                {
                    ReceivingGUI.containerListForm.Invalidate(false);
                }
                catch { }
            }
        }

        public static void SetSelectedContainer(Container container)
        {
            SelectedContainer = container;
            
            if (SelectedContainer.ReleasesAndItems.Count > 0)
            {
                if (SelectedContainer.ReleasesAndItems.Keys.ToArray().Contains(SelectedRelease))
                {
                    return;
                }
                else
                {
                    SetDefaultRelease();
                }
            }
            if (ValidateSelectedContainerAndRelease() == 0)
            {
                container.UpdateContainerStatus();
            }
        }

        private static void SetDefaultRelease()
        {
            if (SelectedContainer != null)
            {
                if (SelectedContainer.ReleasesAndItems.Count > 0)
                {
                    Debug.WriteLine("setting default");
                    SetSelectedRelease(SelectedRelease = SelectedContainer.ReleasesAndItems.Keys.ToArray()[0]);
                }
            }
        }

        public static List<Container> GetContainersByStatus(string Status)
        {
            List<Container> sortedContainers = new List<Container>();
            Debug.WriteLine(AllContainers.Count);
            foreach (var container in AllContainers)
            {
                if (container?.ContainerStatus == Status)
                {
                    sortedContainers.Add(container);
                }
            }
            return sortedContainers;
        }

        public static void UndoSelectedContainer()
        {
            SelectedRelease = 0;
            SelectedContainer = null;            
        }

        public async Task SerializeToFileAsync(string FilePath, bool overwrite = true)
        {
            if (File.Exists(FilePath) && !overwrite)
            {
                throw new InvalidOperationException("File already exists and overwrite was disabled for this action.");
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            string json = JsonSerializer.Serialize(this, options);
            Debug.WriteLine($"Serializing {Path.GetFileName(FilePath)}");
            await File.WriteAllTextAsync(FilePath + ".json", json);
        }

        public static async Task<Container> DeserializeFromFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.WriteLine($"Couldn't find specified file: {filePath}");
                throw new FileNotFoundException("The specified file does not exist.", filePath);
            }
            Debug.WriteLine($"{filePath}");
            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<Container>(json);
        }

        public static async Task DeleteContainerFileAsync(Container container)
        {
            string FilePath = ConfigData.appContainersFolder;
            if (Directory.Exists(FilePath))
            {
                Debug.WriteLine(Path.Combine(FilePath, container.ToString() + ".json"));
                if (container != null && File.Exists(Path.Combine(FilePath, container.ToString() +".json")))
                {
                    var fileToDelete = Path.Combine(FilePath, container.ToString() + ".json");
                    Debug.WriteLine("deleting file");
                    File.Delete(fileToDelete);
                }
            }
        }

        internal static async void DeserializeAllContainers()
        {
            var files = Directory.GetFiles(ConfigData.appContainersFolder);
            if (files.Length > 0)
            {
                foreach (var file in files)
                {

                    try
                    {
                        Debug.WriteLine("deserializing");
                        Container cont = await DeserializeFromFileAsync(file);
                        Debug.WriteLine("deserialized");
                        Container.AddContainer(cont);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Failure to deserialize container {ex.Message}");
                    }
                }
            }

        }

        public class ConcurrentDictionaryConverter : JsonConverter<ConcurrentDictionary<int, ConcurrentDictionary<int, Item>>>
        {
            public override ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var dictionary = JsonSerializer.Deserialize<Dictionary<int, Dictionary<int, Item>>>(ref reader, options);
                var concurrentDictionary = new ConcurrentDictionary<int, ConcurrentDictionary<int, Item>>();

                foreach (var kvp in dictionary)
                {
                    concurrentDictionary[kvp.Key] = new ConcurrentDictionary<int, Item>(kvp.Value);
                }

                return concurrentDictionary;
            }

            public override void Write(Utf8JsonWriter writer, ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> value, JsonSerializerOptions options)
            {
                var dictionary = new Dictionary<int, Dictionary<int, Item>>();

                foreach (var kvp in value)
                {
                    dictionary[kvp.Key] = new Dictionary<int, Item>(kvp.Value);
                }

                JsonSerializer.Serialize(writer, dictionary, options);
            }
        }
    }
}
