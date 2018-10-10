using Hello.BL.Abstract;
using Hello.BL.Concrete;
using Hello.BL.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hello.Controllers
{
    public class HiController : Controller
    {
        private IGreeter greeter = null;

        public HiController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IGreeter>().To<HiGreeter>();
            greeter = ninjectKernel.Get<IGreeter>();
        }

        public ActionResult Index()
        {
            Greeting greeting = new Greeting();
            greeting = greeter.SayHello();
            return View(greeting);
        }
    }
}