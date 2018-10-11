using Hello.BL.Abstract;
using Hello.BL.Models;

namespace Hello.BL.Concrete
{
    public class HelloGreeter : IGreeter
    {
        public Greeter SayHello()
        {
            Greeter greeter = new Greeter();
            greeter.GreeterId = 2;
            greeter.Name = "«Hi everyone!";
            return greeter;
        }
    }
}