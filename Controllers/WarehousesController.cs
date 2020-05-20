using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace warehouse_task.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WarehousesController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<List<KeyValuePair<string, Warehouse>>>> Post([FromBody] string data)
        {
            var importer = new StringWarehouseImporter();

            var parsedData = importer.Import(data);

            return parsedData;
        }
    }
}
