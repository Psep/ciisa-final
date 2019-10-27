using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LlamadoPacientes.Models;
using LlamadoPacientes.Models.Repository;
using System.Timers;

namespace LlamadoPacientes.Controllers
{
    public class HomeController : Controller
    {
        private AtencionRepository atencionRepository;
        static List<Atencion> ListAtencionCarrusel;

        [HttpGet]
        public ActionResult Index()
        {
            this.init(0);
            ViewBag.lastId = this.LastId();
            this.SetTimer();
            return View();
        }

        [HttpPost]
        public ActionResult Index(string lastId)
        {
            int id = Int32.Parse(lastId);
            this.init(id);
            ViewBag.lastId = this.LastId();

            return View();
        }

        private int LastId()
        {
            if (ListAtencionCarrusel.Count() < 4)
            {
                return 0;
            }
            else
            {
                return ListAtencionCarrusel.Last().id;
            }
        }

        private void SetTimer()
        {
            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            //TODO: 40000 = 40 segundos, 20 min = 1200000
            timer.Interval = 40000;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (ListAtencionCarrusel != null && ListAtencionCarrusel.Count() > 0)
            {
                foreach(Atencion item in ListAtencionCarrusel)
                {
                    AtencionRepository repository = new AtencionRepository();
                    repository.Update(item.id);
                }
            }
        }

        private void init(int lastId)
        {
            ViewBag.listaAtencion = this.loadAtenciones();
            ViewBag.listaCarrusel = this.loadCarrusel(lastId);
        }

        private List<Atencion> loadCarrusel(int lastId)
        {
            this.atencionRepository = new AtencionRepository();
            ListAtencionCarrusel = this.atencionRepository.FindCarrusel(lastId);
            return ListAtencionCarrusel;
        }

        private List<Atencion> loadAtenciones()
        {
            this.atencionRepository = new AtencionRepository();
            return this.atencionRepository.FindAtenciones();
        }
        
    }
}