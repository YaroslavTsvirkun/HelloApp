using Hello.BL.Abstract;
using Hello.BL.Models;

namespace Hello.BL.Concrete
{
    public class HiGreeter : IGreeter
    {
        public Greeter SayHello()
        {
            Greeter greeter = new Greeter();
            greeter.GreeterId = 1;
            greeter.Name = "Hi there!";
            return greeter;
        }
    }
}