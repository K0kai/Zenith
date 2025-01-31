using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.forms;

namespace ArcherTools_0._0._1.classes
{
    public class Container
    {
        public static Container SelectedContainer { get; set; }
        public static int SelectedRelease {  get; set; }
        public static List<Container> AllContainers { get; set; }
        public string ContainerId { get; set; }

        [JsonConverter(typeof(ConcurrentDictionaryConverter))]
        public ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> ReleasesAndItems { get; set; }
        public int ExpectedSize { get; set; }
        public string ContainerStatus { get; set; }



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

        public void AddItemToRelease(int release, int line, Item item)
        {
            if (this.ReleasesAndItems != null)
            {
                var ReleaseItems = this.ReleasesAndItems[release];
                ReleaseItems.TryAdd(line, item);
            }
        }

        public Container(string containerId, ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> releasesAndItems)
        {
            this.ContainerId = containerId;
            this.ReleasesAndItems = releasesAndItems;
        }

        public override string ToString()
        {
            return this.ContainerId;
        }

        public static void ClearContainers()
        {
            AllContainers.Clear();
        }

        public void UpdateContainerStatus()
        {
            if (ReleasesAndItems.Count >= ExpectedSize)
            {
                ContainerStatus = "Complete";
                return;
            }
            if (ReleasesAndItems.Count < ExpectedSize)
            {
                ContainerStatus = "Incomplete";
                return;
            }
        }

        public void SetExpectedSize(int expectedSize)
        {
            if (expectedSize > 0)
            {
                ExpectedSize = expectedSize;
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
                ReceivingGUI.containerListForm.Invalidate();
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
            if (ReceivingGUI.containerListForm != null && SelectedContainer != null)
            {
                ReceivingGUI.containerListForm.Invalidate();
            }
        }

        public static void UndoSelectedContainer()
        {
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
