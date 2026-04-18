using Microsoft.AspNetCore.Mvc;
using frontend.Models;
using System.Net.Http.Json;

namespace frontend.Controllers
{
    public class InventarioController : Controller
    {
        private readonly HttpClient _http;

        public InventarioController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
            _http.BaseAddress = new Uri("http://localhost:5090/");
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _http.GetFromJsonAsync<List<Producto>>("api/inventario");
            return View(productos);
        }

        public IActionResult Formulario()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Modificar(int id)
        {
            var producto = await _http.GetFromJsonAsync<Producto>($"api/inventario/{id}");
            if (producto == null) return RedirectToAction("Index");

            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(Producto producto)
        {
            if (producto.Id > 0)
            {
                await _http.PutAsJsonAsync($"api/inventario/{producto.Id}", producto);
            }
            else
            {
                await _http.PostAsJsonAsync("api/inventario", producto);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _http.DeleteAsync($"api/inventario/{id}");
            return RedirectToAction("Index");
        }
    }
}