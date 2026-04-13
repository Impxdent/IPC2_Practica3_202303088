using Microsoft.AspNetCore.Mvc;
using IPC2_Practica3_202303088.Models;
using IPC2_Practica3_202303088.Services;

namespace IPC2_Practica3_202303088.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly InventarioService _service = new InventarioService();

        [HttpGet]
        public IActionResult Get() => Ok(_service.LeerTodo());

        [HttpPost]
        public IActionResult Post([FromBody] Producto nuevo)
        {
            var productos = _service.LeerTodo();
            int nuevoId = productos.Count > 0 ? productos.Max(p => p.GetId()) + 1 : 1;
            nuevo.SetId(nuevoId);
            
            productos.Add(nuevo);
            _service.GuardarTodo(productos);
            return Ok(nuevo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var productos = _service.LeerTodo();
            var producto = productos.FirstOrDefault(p => p.GetId() == id);
            
            if (producto == null) return NotFound();

            productos.Remove(producto);
            _service.GuardarTodo(productos);
            return Ok();
        }
    }
}