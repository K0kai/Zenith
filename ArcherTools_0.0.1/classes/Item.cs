using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.classes
    {
    public class Item
    {
        public int pieces { get; set; }
        public int total_pieces { get; set; }
        public int po { get; set; }
        public string? owner {  get; set; }
        public string? style {  get; set; }
        public string? itemCode { get; set; }
        public string? color {  get; set; }
        public string? colorCode { get; set; }
        public string? size { get; set; }
        public string? description { get; set; }

        public Item(string itemCode, int po = 0, string style = "", string color = "", string colorCode = "", string size = "", string description = "", string owner = "") 
        {
            this.po = po;
            this.owner = owner;
            this.style = style;
            this.itemCode = itemCode;
            this.color = color;
            this.colorCode = colorCode;
            this.size = size;
            this.description = description;

        }

        public Item()
        {
        }

        public Item(string owner, string itemCode)
        {
            this.owner = owner;
            this.itemCode = itemCode;
        }

        public Item(string itemCode)
        {
            this.itemCode = itemCode;
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

        public string GetItemInformation()
        {
            var bodyString = "";
            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                bodyString += $"{propertyInfo.Name} : {propertyInfo.GetValue(this)}\n";
            }
            return bodyString;
        }
    }
}
