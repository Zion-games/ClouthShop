using System.Collections.Generic;

namespace ClouthShop
{

    public class ClothingItem
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Price { get; set; }
        public bool Owned { get; set; }
        public uint index { get; set; }
        public uint texture { get; set; }
    }

    public class ClothingData
    {
        public List<ClothingItem> Top { get; set; }
        public List<ClothingItem> Bottom { get; set; }

        
    }
}
