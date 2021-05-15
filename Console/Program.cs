

using Core.DataAccess.FirebaseStorage;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            FirebaseStorageRepositoryBase<string> a = new FirebaseStorageRepositoryBase<string>();
            a.Upload(@"C:\Users\BERKAY\Desktop\1.mp4","DENEME").Wait();
        }



    }
}
