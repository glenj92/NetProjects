using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        //Referencia a nuestro contexto, y la instancia se llamara _context
        private readonly ProductContext _context; 

        //Se crea un constructor que resive como parametro un ProductContext y se asigna al _context el contexto que recibe el constructor
        //Asi queda inicialidad esta instancia de mi contexto para crear los metodos
        public ProductController(ProductContext productContext)
        {
            _context = productContext;
        }

        // GET: api/products/{id}

        //Se crea un metodo GetProduct del tipo HttpGet para que liste todos los productos
        //Sera un metodo asincrono, va a hacer un tarea de ActionResult y que sea del tipo IEnumerable y que sera del tipo de mi modelo Product
        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            //Se llama al metodo de la clase ProductContext que se llama Products y se devuelve el resultado
            //Se hace un return await de mi contexto _context el DbSet Products y se utiliza el metodo ToListAsync para listar los productos.
            //Aca se utiliza el paquete de EntityFrameworkCore para que el ToListAsync funcione
            return await _context.Products.ToListAsync();
        }

        //Se crea un metodo del tipo HttpPost para que liste todos los productos
        //Sera un metodo asincrono, va a hacer un tarea de ActionResult y que sea del tipo Product para poder insertar un producto 
        //A este metodo o ActionResult lo voy a llamar PostProduct(), y le paso como parametro un objeto del tipo Product que se llamara product
        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product){
            //Se crea un nuevo producto 
            //Se llama al contexto _context para tomar mi DbSet el metodo Add y le mando el producto que estoy recibiendo
            //Luego se hace un await al contexto y se guardanm los cambios
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            //Se redirecciona al usuario si fuece un proyecto MVC [NO ES EL CASO] //return RedirectToAction("Index");
            
            //Se devuelve el producto-resgistro que se acaba de insertar 
            //Se manda un CreatedAtAction y action que se esta llamando es el metodo GetProduct(), y este GetProduct() lo filtramos enviandole el identificador que viene en mi product ID, y el modelo product
            //Entonce el insertar un producto mostrarra como resultado
            return CreatedAtAction(nameof(GetProduct), new{id=product.Id}, product);  
        }
    }
}