using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioService _service = new InventarioService();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.LeerTodo());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var producto = _service.LeerTodo().FirstOrDefault(p => p.Id == id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] Producto producto)
        {
            var productos = _service.LeerTodo();

            producto.Id = productos.Count > 0 ? productos.Max(x => x.Id) + 1 : 1;

            productos.Add(producto);
            _service.GuardarTodo(productos);

            return Ok(producto);
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] Producto productoActualizado)
        {
            var productos = _service.LeerTodo();
            var producto = productos.FirstOrDefault(p => p.Id == id);

            if (producto == null) return NotFound();

            producto.Nombre = productoActualizado.Nombre;
            producto.Descripcion = productoActualizado.Descripcion;
            producto.Categoria = productoActualizado.Categoria;
            producto.Precio = productoActualizado.Precio;
            producto.CantidadStock = productoActualizado.CantidadStock;
            producto.FechaVencimiento = productoActualizado.FechaVencimiento;

            _service.GuardarTodo(productos);

            return Ok(producto);
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var productos = _service.LeerTodo();
            var producto = productos.FirstOrDefault(p => p.Id == id);

            if (producto == null) return NotFound();

            productos.Remove(producto);
            _service.GuardarTodo(productos);

            return Ok();
        }
    }
}