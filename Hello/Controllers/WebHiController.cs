using Hello.BL.Abstract;
using Hello.BL.Concrete;
using Hello.BL.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hello.Controllers
{
    public class WebHiController : ApiController
    {
        private IGreeter greeter = null;

        public WebHiController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IGreeter>().To<HiGreeter>();
            greeter = ninjectKernel.Get<IGreeter>();
        }

        public Greeting Get()
        {
            Greeting greeting = new Greeting();
            greeting = greeter.SayHello();
            return greeting;
        }

    }
}
