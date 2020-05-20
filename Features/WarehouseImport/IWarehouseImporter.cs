using System.Collections.Generic;

namespace warehouse_task
{
    public interface IWarehouseImporter<T>
    {
        List<KeyValuePair<string, Warehouse>> Import(T data);
    }
}