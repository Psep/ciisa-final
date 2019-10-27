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
            ViewBag.posicion = 1;

            this.init(1);

            return View();
        }

        [HttpPost]
        public ActionResult Index(string posicion)
        {
            int pos = Int32.Parse(posicion);
            if (pos == 3) pos = 0;
            ViewBag.posicion = pos + 1;
            this.init(pos);
            return View();
        }

        private void init(int posicion)
        {
            this.loadAtenciones();
            this.loadCarrusel(posicion);
            ViewBag.listaAtencion = this.listAtencion;
            ViewBag.listaCarrusel = this.listAtencionCarrusel;
        }

        private void loadCarrusel(int posicion)
        {
            this.atencionRepository = new AtencionRepository();
            this.listAtencionCarrusel = this.atencionRepository.listarCarrusel(posicion);
        }

        private void loadAtenciones()
        {
            this.atencionRepository = new AtencionRepository();
            this.listAtencion = this.atencionRepository.listarUltimasAtenciones();
            
        }
        
    }
}