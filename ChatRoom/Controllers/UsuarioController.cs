using ChatRoom.Datos.Entidades;
using ChatRoom.Dominio;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private IHubContext<ChatHub> _hubContext;
        private readonly UserManager<Usuario> _userManager;


        public UsuarioController(IUsuarioService usuarioService, IHubContext<ChatHub> hubContext, UserManager<Usuario> userManager)
        {
            _usuarioService = usuarioService;
            _hubContext = hubContext;
            _userManager = userManager;
        }
        // GET: UsuarioController

        public IActionResult Perfil()
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (User.Identity.IsAuthenticated)
            {
                ViewData["AuthenticatedUser"] = user;
                return View();
            }
            return View();
        }



        public IActionResult MisCompras()
        {

            var user = _userManager.GetUserAsync(User).Result;
            var compras = _usuarioService.GetCompras(user.Id);

            return View(compras);
        }
        public IActionResult MisVentas()
        {

            var user = _userManager.GetUserAsync(User).Result;
            var ventas = _usuarioService.GetVentas(user.Id);

            return View(ventas);
        }














        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

    }
}
