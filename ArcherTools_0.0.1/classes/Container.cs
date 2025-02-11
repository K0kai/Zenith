using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.enums;
using ArcherTools_0._0._1.forms;
using MathNet.Numerics;
using Org.BouncyCastle.Bcpg.OpenPgp;

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
            SerializeToFileAsync(Path.Combine(ConfigData.appContainersFolder, this.ContainerId));
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
        public void AddItemToRelease(int release, int line, Item item)
        {
            if (this.ReleasesAndItems != null)
            {
                var ReleaseItems = this.ReleasesAndItems[release];
                ReleaseItems.TryAdd(line, item);
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

        }

        public override string ToString()
        {
            return this.ContainerId;
        }

        public static void ClearContainers()
        {
            AllContainers.Clear();
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
                            return (byte) ErrorEnum.ErrorCode.Success;
                        }
                    }
                }
            }
            Debug.WriteLine($"Error validating: {SelectedContainer}'s release: {selectedRelease}");
            return (byte) ErrorEnum.ErrorCode.UnknownError;
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
                        return;
                    }
                    else
                    {
                        if (SelectedContainer.ContainerId == this.ContainerId)
                        {
                            if (ReleasesAndItems[SelectedRelease].Count == 0)
                            {
                                ContainerStatus = "Empty";
                                return;
                            }
                            else if (ReleasesAndItems[SelectedRelease].Count >= ExpectedSize)
                            {
                                ContainerStatus = "Complete";
                            }
                            else
                            {
                                ContainerStatus = "Incomplete";
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
                default:
                    intRelease = int.Parse(release);
                    break;
            }
            return intRelease;
        }
        

        internal void CalculateExpectedSize()
        {
            if (SelectedContainer.attachedConfigurations != null)
            {
                var expectedSize = this.AttachedConfigurations[SelectedRelease].Count;
                this.ExpectedSize = expectedSize;
            }
        }

        public static void AddContainer(Container container)
        {
            if (AllContainers == null)
            {
                AllContainers = new List<Container>();
            }
            List<string> ContainerNames = new List<string>();
            foreach (var cont in AllContainers)
            {
                ContainerNames.Add(cont.ContainerId);
            }
            if (ContainerNames.Contains(container.ContainerId)) {return; } 
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
                            Debug.WriteLine("Before: "+ AllContainers.Count);
                            AllContainers.Remove(cont);
                            Debug.WriteLine("After: " + AllContainers.Count);
                            break;
                        }
                    }
                }
                try
                {
                    ReceivingGUI.containerListForm.Invalidate();
                }
                catch { }
            }
        }

        public static void SetSelectedContainer(Container container)
        {
            SelectedContainer = container;
            if (SelectedContainer.ReleasesAndItems.Count > 0)
            {
                SetSelectedRelease(SelectedRelease = SelectedContainer.ReleasesAndItems.Keys.ToArray()[0]);
            }
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
