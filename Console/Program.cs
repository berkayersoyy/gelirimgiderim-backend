
using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using Core.DataAccess.FirebaseStorage;
using FluentValidation;


namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            AuthManager manager = new AuthManager();
            manager.Login("aaaaa@hotmail.com","123456").Wait();
        }
    }
}
