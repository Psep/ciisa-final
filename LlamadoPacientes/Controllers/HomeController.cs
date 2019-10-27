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
        private List<Atencion> ListAtencionCarrusel;
        static Stack<Atencion> Pila;

        [HttpGet]
        public ActionResult Index()
        {
            Pila = new Stack<Atencion>();
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
            timer.Interval = 1200000;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            int aux = 0;

            while (Pila.Count() > 0 && aux < 4)
            {
                Atencion item = Pila.Pop();
                AtencionRepository repository = new AtencionRepository();
                repository.Update(item.id);
                aux++;
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
            this.ListAtencionCarrusel = this.atencionRepository.FindCarrusel(lastId);
            foreach (Atencion atencion in this.ListAtencionCarrusel) Pila.Push(atencion);
            
            return ListAtencionCarrusel;
        }

        private List<Atencion> loadAtenciones()
        {
            this.atencionRepository = new AtencionRepository();
            return this.atencionRepository.FindAtenciones();
        }
        
    }
}