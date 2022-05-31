using Cloud.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using FireSharp.Config;
using FireSharp.Interfaces;
using FirebaseAdmin.Auth;
using Firebase.Auth;
using FireSharp.Response;

public interface IDataLayer
{
    string Tokent();
    Task<Root> MovieByName(string name);
    Task<Root> Trending();
    Task<Result> MovieById(int id);
    //==========User interaction======
     Task<bool> LogIn(UserDom user);
     Task AddToFavorites(int movie);
     Task SingUp(UserDom user);
     Task<IList<Result>> AllUserFavorites();
     void LogOut();
}



namespace Cloud.Models.Router
{
    public class DataLayer : IDataLayer
    {
        private static string API_KEY = "AIzaSyDfnAUyhfSYWIppLrahRhuc-ZtcxTG2lQo";
        public static DataLayer instance = null;
        private static readonly object padlock = new object();

        public static string token { get; set; }
        public static string token1 { get; set; }

        public string Tokent()
        {
            return token;
        }
        private static string projectId;
        private FireSharp.Config.FirebaseConfig config;
        FireSharp.FirebaseClient dbclient;

        //==== Firebase configuration ===
        IFirebaseConfig configs = new FireSharp.Config.FirebaseConfig()
        {
            AuthSecret = "0KhwicKoxPo43XLJaP5Ye8yLzY13kagROlUbSm6n",
            BasePath = "https://cloudfire-99e69-default-rtdb.firebaseio.com/"
        };


        //================
        HttpClient client = new HttpClient();
        public DataLayer()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            dbclient = new FireSharp.FirebaseClient(configs);
        }
        public static DataLayer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataLayer();
                    }
                    return instance;
                }
            }
        }
        public async Task<Result> MovieById(int id)
        {
            var uri = "https://cloud-computing-sep6.ew.r.appspot.com/movies/" + id;
            var streamTask = await client.GetStringAsync(uri);
            Result recordDetails = JsonSerializer.Deserialize(streamTask.ToString(), typeof(Result)) as Result;
            return recordDetails;
        }
        public async Task<Root> MovieByName(string name)
        {
            var uri = "https://cloud-computing-sep6.ew.r.appspot.com/search/movie/page/1/name/" + name;
            var streamTask = await client.GetStringAsync(uri);


          //  Root recordDetails = JsonSerializer.Deserialize(streamTask.ToString(), typeof(Root)) as Root;
            Root userCopy = JsonSerializer.Deserialize<Root>(streamTask);
            /*
            Console.WriteLine(recordDetails.page);
            foreach (var item in recordDetails.results)
            {
                Console.WriteLine(item.title);
            }
           */
            return userCopy;
        }
        public async Task<Root> Trending()
        {
            var uri = "https://cloud-computing-sep6.ew.r.appspot.com/movies/trending";
            var streamTask = await client.GetStringAsync(uri);
            Root recordDetails = JsonSerializer.Deserialize(streamTask.ToString(), typeof(Root)) as Root;
            return recordDetails;

        }
       
 
        public async Task<bool> LogIn(UserDom user)
        {
            Console.WriteLine(user.password +"pass") ;
            Console.WriteLine(user.email);
            try
            {
                var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(API_KEY));
                var lionk = await auth.SignInWithEmailAndPasswordAsync(user.email, user.password);
                token = lionk.FirebaseToken;
                token1 = user.email;
               
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw;
            }
            
            
        }
        
        public void LogOut()
        {
            token = null;
        }
        
        public async Task SingUp(UserDom user)
        {
            var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(API_KEY));
            var a = await auth.CreateUserWithEmailAndPasswordAsync(user.email, user.password);
            await LogIn(user);
            

        }


        public async Task AddToFavorites(int movie)
        {
            if (token != null)
            {

               await dbclient.PushAsync(token1 + "/", movie);

            }
        }

        public async Task<IList<Result>> AllUserFavorites()
        {

            var result = dbclient.Get(token + "/");
            Console.WriteLine(result);

            /*
            string uri = "https://movies-app-310106.nw.r.appspot.com/api/movies/favorites/";
            string jsonString = @"{ ""ids"":";
            IList<int> milbei = new List<int> { 68646, 71562, 68202, 70951 };
            var json = System.Text.Json.JsonSerializer.Serialize(milbei);
            jsonString = jsonString + json + "}";
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var streamTask = client.PostAsync(uri, content);
            string result = await streamTask.Result.Content.ReadAsStringAsync();
            */
            return null;
        }


    }
}
