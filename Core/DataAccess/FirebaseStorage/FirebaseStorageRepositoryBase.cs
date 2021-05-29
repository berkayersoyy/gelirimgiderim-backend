

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Firebase.Auth;


namespace Core.DataAccess.FirebaseStorage
{
    public class FirebaseStorageRepositoryBase : IStorageRepository
    {
        public async Task<string> Upload(string path,string roomId,string fileName)
        {
            var fromBase64 = Convert.FromBase64String(path);
            var stream = new MemoryStream(fromBase64);
            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebasePaths.ApiKey));
            var login = await auth.SignInWithEmailAndPasswordAsync("forusestorage@hotmail.com", "x03121998X+");
            var cancellation = new CancellationTokenSource();
            var upload = new Firebase.Storage.FirebaseStorage(FirebasePaths.AppBucket)
                .Child("images")
                .Child(roomId)
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);

            return await upload;
        }

        public async void Delete(string fileName,string roomId)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebasePaths.ApiKey));
            var login = await auth.SignInWithEmailAndPasswordAsync("forusestorage@hotmail.com", "x03121998X+");
            var result = new Firebase.Storage.FirebaseStorage(FirebasePaths.AppBucket).Child("images").Child(roomId).Child(fileName)
                .DeleteAsync();
        }

        public async Task<string> Get(string fileName, string roomId)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebasePaths.ApiKey));
            var login = await auth.SignInWithEmailAndPasswordAsync("forusestorage@hotmail.com", "x03121998X+");
            var result = new Firebase.Storage.FirebaseStorage(FirebasePaths.AppBucket).Child("images").Child(roomId).Child(fileName)
                .GetDownloadUrlAsync();
            return await result;
        }
    }
}