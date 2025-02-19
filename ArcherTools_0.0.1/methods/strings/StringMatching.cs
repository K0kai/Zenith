using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.methods.strings
{
    internal class StringMatching
    {
        public static double CosineSimilarity(string s1, string s2)
        {
            var words1 = s1.ToLower().Split(' ').GroupBy(w => w).ToDictionary(g => g.Key, g => g.Count());
            var words2 = s2.ToLower().Split(' ').GroupBy(w => w).ToDictionary(g => g.Key, g => g.Count());

            var uniqueWords = new HashSet<string>(words1.Keys.Concat(words2.Keys));

            double dotProduct = 0, magnitude1 = 0, magnitude2 = 0;

            foreach (var word in uniqueWords)
            {
                int freq1 = words1.ContainsKey(word) ? words1[word] : 0;
                int freq2 = words2.ContainsKey(word) ? words2[word] : 0;

                dotProduct += freq1 * freq2;
                magnitude1 += Math.Pow(freq1, 2);
                magnitude2 += Math.Pow(freq2, 2);
            }

            return (magnitude1 == 0 || magnitude2 == 0) ? 0 : (dotProduct / (Math.Sqrt(magnitude1) * Math.Sqrt(magnitude2))) * 100;
        }
    }

}
