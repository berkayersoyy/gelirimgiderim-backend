

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Firebase.Auth;


namespace Core.DataAccess.FirebaseStorage
{
    public class FirebaseStorageRepositoryBase<T> : IStorageRepository<T>
    {
        public async Task<string> Upload(string path, string collection)
        {
            var stream = new MemoryStream(File.ReadAllBytes(path));
            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebasePaths.ApiKey));
            var login = await auth.SignInWithEmailAndPasswordAsync("forusestorage@hotmail.com", "x03121998X+");
            var cancellation = new CancellationTokenSource();
            FileInfo info = new FileInfo(path);
            var upload = new Firebase.Storage.FirebaseStorage(FirebasePaths.AppBucket)
                .Child("images")
                .Child(collection)
                .Child(info.Name)
                .PutAsync(stream, cancellation.Token);
            return await upload;
        }

        public async void Delete(string collection, string fileName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebasePaths.ApiKey));
            var login = await auth.SignInWithEmailAndPasswordAsync("forusestorage@hotmail.com", "x03121998X+");
            var result = new Firebase.Storage.FirebaseStorage(FirebasePaths.AppBucket).Child("images").Child(collection).Child(fileName)
                .DeleteAsync();
        }

        public async Task<string> Get(string fileName, string collection)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebasePaths.ApiKey));
            var login = await auth.SignInWithEmailAndPasswordAsync("forusestorage@hotmail.com", "x03121998X+");
            var result = new Firebase.Storage.FirebaseStorage(FirebasePaths.AppBucket).Child("images").Child(collection).Child(fileName)
                .GetDownloadUrlAsync();
            return await result;
        }
    }
}