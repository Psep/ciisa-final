using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LlamadoPacientes.Models;
using LlamadoPacientes.Models.Repository;

namespace LlamadoPacientes.Controllers
{
    public class HomeController : Controller
    {
        private AtencionRepository atencionRepository;

        public List<Atencion> listAtencion { get; set; }
        public List<Atencion> listAtencionCarrusel { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            this.init(0);
            ViewBag.lastId = 0;

            return View();
        }

        [HttpPost]
        public ActionResult Index(string lastId)
        {
            int id = Int32.Parse(lastId);

            this.init(id);

            if (this.listAtencionCarrusel.Count() <= 4)
            {
                ViewBag.lastId = 0;
            }
            else
            {
                ViewBag.lastId = this.listAtencionCarrusel.Last().id;
            }

            return View();
        }

        private void init(int lastId)
        {
            this.loadAtenciones();
            this.loadCarrusel(lastId);
            ViewBag.listaAtencion = this.listAtencion;
            ViewBag.listaCarrusel = this.listAtencionCarrusel;
        }

        private void loadCarrusel(int lastId)
        {
            this.atencionRepository = new AtencionRepository();
            this.listAtencionCarrusel = this.atencionRepository.FindCarrusel(lastId);
        }

        private void loadAtenciones()
        {
            this.atencionRepository = new AtencionRepository();
            this.listAtencion = this.atencionRepository.FindAtenciones();
        }
        
    }
}