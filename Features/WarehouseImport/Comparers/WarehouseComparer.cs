using System.Collections.Generic;

namespace warehouse_task
{
    public class WarehouseComparer : IComparer<KeyValuePair<string, Warehouse>>
    {
        public int Compare(KeyValuePair<string, Warehouse> warehouseOne, KeyValuePair<string, Warehouse> warehouseTwo)
        {
            if (warehouseOne.Value.MaterialsAmount != warehouseTwo.Value.MaterialsAmount)
            {
                return warehouseTwo.Value.MaterialsAmount - warehouseOne.Value.MaterialsAmount;
            }
            else
            {
                return -string.Compare(warehouseOne.Key, warehouseTwo.Key);
            }
        }
    }
}