using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.classes
    {
    internal class Item
    {
        public string owner {  get; set; }
        public string style {  get; set; }
        public string itemCode { get; set; }
        public string color {  get; set; }
        public string colorCode { get; set; }
        public string size { get; set; }
        public string description { get; set; }

        internal Item(string itemCode, string style = "", string color = "", string colorCode = "", string size = "", string description = "", string owner = "") 
        {
            this.owner = owner;
            this.style = style;
            this.itemCode = itemCode;
            this.color = color;
            this.colorCode = colorCode;
            this.size = size;
            this.description = description;

        }

        public string getStyleFromCode(string code)
        {            
           string [] splitItem =  code.Split(new char[] {'-', '\n', ' '}, StringSplitOptions.RemoveEmptyEntries);
           return splitItem[0];
        }

        public static bool compareSizes(string size1, string size2)
        {
            try
            {
                if (size1.Equals(size2))
                {
                    return true;
                }
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);                
            }
            return false;
        }
    }
}
