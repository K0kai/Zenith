using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.classes
{
    internal class Container
    {
        public string ContainerId {  get; set; }
        public ConcurrentDictionary<int, ConcurrentDictionary<int, Item>> ReleasesAndItems { get; set; }

        public ConcurrentDictionary<int, Item>? GetContainerItems(int release)
        {
            try
            {
                if (ReleasesAndItems.TryGetValue(release, out var item)) return item;
                else { throw new Exception("Item list or release might not exist."); }
            }
            catch (Exception ex) {
                Debug.WriteLine($"Did not manage to get item list from container {ContainerId}\nReason: {ex.Message}");
                return null;
            }
        }


    }
}
