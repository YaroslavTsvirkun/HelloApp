using Hello.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.BL.Abstract
{
    public interface IGreeter
    {
        Greeter SayHello();
    }
}