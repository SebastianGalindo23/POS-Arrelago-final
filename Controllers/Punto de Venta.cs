using Microsoft.AspNetCore.Mvc;
using POS.Data;

namespace POS.Controllers
{
    public class Punto_de_Venta : Controller
    {

        private readonly ApplicationDbContext _context;

        public Punto_de_Venta(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? search, string? filter)
        {
            var productos = _context.Productos.AsQueryable();

            // Filtrar por el tipo de búsqueda
            if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(filter))
            {
                switch (filter.ToLower())
                {
                    case "nombre":
                        productos = productos.Where(p => p.Nombre.Contains(search));
                        break;

                    case "codigo":
                        productos = productos.Where(p => p.Codigo.Contains(search));
                        break;

                    case "precio":
                        if (decimal.TryParse(search, out var precio))
                        {
                            productos = productos.Where(p => p.Precio == precio);
                        }
                        break;

                    default:
                        break;
                }
            }

            var listaProductos = productos.ToList();
            return View(listaProductos);
        }
    }
}
