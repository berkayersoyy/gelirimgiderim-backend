

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Core.Utilities.FileReader;
using Firebase.Auth;


namespace Core.DataAccess.FirebaseStorage
{
    public class FirebaseStorageRepositoryBase<T>:IStorageRepository<T>
    {
        public async Task Upload()
        {
            var stream = new MemoryStream(File.ReadAllBytes(@"C:\Users\BERKAY\Desktop\1.mp4"));
            var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCoX8qD3H53-IzUv5h97-W-FlF47qIbPug"));
            var a = await auth.SignInWithEmailAndPasswordAsync("afafa@hotmail.com", "123456");
            var cancellation = new CancellationTokenSource();
            var upload = new Firebase.Storage.FirebaseStorage("gelirimgiderim-d0b53.appspot.com").Child("images").
                Child("hehehehehe").
                PutAsync(stream,cancellation.Token);
            var url = await upload;
        }

        public async void Upload(string path, string collection, string email, string password)
        {
            var stream = new MemoryStream(File.ReadAllBytes(path));
            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebasePaths.ApiKey));
            var login = await auth.SignInWithEmailAndPasswordAsync(email, password);
            var cancellation = new CancellationToken();
            var result = new Firebase.Storage.FirebaseStorage(FirebasePaths.AppBucket)
                .Child(collection)
                .Child(FileReader.ReadFileName(path));
        }

        public async void Delete(string path, string collection, string email, string password)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebasePaths.ApiKey));
            var login = await auth.SignInWithEmailAndPasswordAsync(email, password);
            var result = new Firebase.Storage.FirebaseStorage(FirebasePaths.AppBucket).Child(collection).Child(path)
                .DeleteAsync();
        }

        public async Task<string> Get(string path, string collection,string email, string password)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebasePaths.ApiKey));
            var login = await auth.SignInWithEmailAndPasswordAsync(email, password);
            var result = new Firebase.Storage.FirebaseStorage(FirebasePaths.AppBucket).Child(collection).Child(path)
                .GetDownloadUrlAsync();
            return await result;
        }

        public async Task<List<string>> GetAll(string collection,string email,string password)
        {
            List<string> urlList = new List<string>();
            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebasePaths.ApiKey));
            var login = await auth.SignInWithEmailAndPasswordAsync(email, password);
            var result = new Firebase.Storage.FirebaseStorage(FirebasePaths.AppBucket).Child(collection).GetDownloadUrlAsync();
            //TODO auth managing need to be added
            //TODO need to test
            throw new NotImplementedException();
        }
    }
}