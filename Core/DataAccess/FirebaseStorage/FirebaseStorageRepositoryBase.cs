

using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Constants;
using Firebase.Auth;


namespace Core.DataAccess.FirebaseStorage
{
    public class FirebaseStorageRepositoryBase : IStorageRepository
    {
        public async Task<List<string>> Upload(string path, string roomId)
        {
            var stream = new MemoryStream(File.ReadAllBytes(path));
            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebasePaths.ApiKey));
            var login = await auth.SignInWithEmailAndPasswordAsync("forusestorage@hotmail.com", "x03121998X+");
            var cancellation = new CancellationTokenSource();
            FileInfo info = new FileInfo(path);
            var upload = new Firebase.Storage.FirebaseStorage(FirebasePaths.AppBucket)
                .Child("images")
                .Child(roomId)
                .Child(info.Name)
                .PutAsync(stream, cancellation.Token);
            List<string> list = new List<string>();
            list.Add(await upload);
            list.Add(info.Name);
            return list;
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