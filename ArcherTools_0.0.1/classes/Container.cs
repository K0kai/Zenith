using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.classes
{
    public class Container
    {
        public static List<Container> AllContainers { get; set; }
        public string ContainerId {  get; set; }

        [JsonConverter(typeof(ConcurrentDictionaryConverter))]
        public ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> ReleasesAndItems { get; set; }

        public ConcurrentDictionary<int, Item>? GetContainerItems(int release)
        {
            try
            {
                if (ReleasesAndItems.TryGetValue(release, out var item)) return item;
                else { throw new Exception("Item list or release might not exist."); }
            }
            catch (Exception ex) {
                Debug.WriteLine($"Couldn't get item list from container {ContainerId}\nReason: {ex.Message}");
                return null;
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

        public static void AddContainer(Container container)
        {
            if (AllContainers == null)
            {
                AllContainers = new List<Container>();
            }
            AllContainers.Add(container);
        }

        public static void RemoveContainer(Container container)
        {
            if (AllContainers != null)
            {
                AllContainers.Remove(container);
            }
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
            await File.WriteAllTextAsync(FilePath, json);
        }

        public static async Task<Container> DeserializeFromFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist.", filePath);
            }

            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<Container>(json);
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
