using System.Collections.Generic;

namespace warehouse_task
{
    public class Warehouse
    {
        public int MaterialsAmount { get; set; }
        public Dictionary<string, int> Materials { get; set; } = new Dictionary<string, int>();
    }
}