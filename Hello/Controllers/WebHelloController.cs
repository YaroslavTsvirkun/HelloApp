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
using System.Web.Mvc;

namespace Hello.Controllers
{
    public class WebHelloController : ApiController
    {
        private IGreeter greeter = null;

        public WebHelloController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IGreeter>().To<HelloGreeter>();
            greeter = ninjectKernel.Get<IGreeter>();
        }

        public Greeter Get()
        {
            Greeter greeting = new Greeter();
            greeting = greeter.SayHello();
            return greeting;
        }
    }
}