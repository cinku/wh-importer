using System;
using System.Collections.Generic;
using System.Linq;

namespace warehouse_task
{
    public class StringWarehouseImporter : IWarehouseImporter<string>
    {
        public List<KeyValuePair<string, Warehouse>> Import(string data)
        {
            var warehouseMaterials = new List<MagazineMaterial>();

            foreach (var line in data.SplitToLines())
            {
                if (IsLineValid(line))
                {
                    var columns = line.Split(";");
                    var magazineAmounts = columns.Last().Split("|");
                    foreach (var magazineAmount in magazineAmounts)
                    {
                        var parsedValue = magazineAmount.Split(",");

                        var magazineMaterial = new MagazineMaterial()
                        {
                            MagazineName = parsedValue[0],
                            MaterialId = columns[1],
                            MaterialAmount = Int32.Parse(parsedValue[1])
                        };

                        warehouseMaterials.Add(magazineMaterial);
                    }
                }
            }

            var warehouses = new Dictionary<string, Warehouse>();

            var materialsByWarehouse = warehouseMaterials
                .GroupBy(m => m.MagazineName)
                .ToDictionary(grp => grp.Key, grp => grp.ToList());

            foreach (var item in materialsByWarehouse)
            {
                Warehouse warehouse = new Warehouse();

                if (warehouses.ContainsKey(item.Key))
                {
                    warehouse = warehouses[item.Key];
                }

                foreach (var material in item.Value)
                {
                    var materialAmount = 0;
                    if (warehouse.Materials.ContainsKey(material.MaterialId))
                    {
                        materialAmount = warehouse.Materials[material.MaterialId];
                    }
                    materialAmount += material.MaterialAmount;
                    warehouse.MaterialsAmount += material.MaterialAmount;
                    warehouse.Materials[material.MaterialId] = materialAmount;
                }

                // we could sort the materials alphebetically here, but as we are sorting them on frontend it's not necessary.
                // However in case we would like to use the ordered list on the backend it would be helpful to uncomment that line
                // warehouse.Materials.OrderBy(m => m.Key);

                warehouses[item.Key] = warehouse;
            }

            var warehousesInOrder = warehouses.OrderBy(x => x, new WarehouseComparer());

            return warehousesInOrder.ToList();
        }

        // Lines starting with # or empty are considered not valid
        private bool IsLineValid(string line)
        {
            line = line.Trim();

            if (line.StartsWith("#") || string.IsNullOrEmpty(line))
            {
                return false;
            }

            return true;
        }
    }
}