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



public interface IDataLayer
{
    Task<Root> MovieByName(string name);
    Task<Root> Trending(string place);
    Task<Root> ProcessPlace(string place);
    Task<Root> MovieById(int id);
    //==========User interaction======
     Task LogIn(UserDom user);
     Task SingUp(UserDom user);
     void LogOut();
}



namespace Cloud.Models.Router
{
    public class DataLayer : IDataLayer
    {
        private static string API_KEY = "AIzaSyDfnAUyhfSYWIppLrahRhuc-ZtcxTG2lQo";
        public static DataLayer instance = null;
        private static readonly object padlock = new object();

        private static FireAuthorization token;
        private static string projectId;
        private FireSharp.Config.FirebaseConfig config;
        FireSharp.FirebaseClient dbclient;

        //==== Firebase configuration ===
        IFirebaseConfig configs = new FireSharp.Config.FirebaseConfig()
        {
            AuthSecret = "0KhwicKoxPo43XLJaP5Ye8yLzY13kagROlUbSm6n",
            BasePath = "https://cloudfire-99e69-default-rtdb.firebaseio.com"
        };


        //================
        HttpClient client = new HttpClient();
        public DataLayer()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            dbclient = new FireSharp.FirebaseClient(config);
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
        public async Task<Root> MovieById(int id)
        {
            var uri = "https://cloud-computing-sep6.ew.r.appspot.com/movies" + id;
            var streamTask = await client.GetStringAsync(uri);
            Root recordDetails = JsonSerializer.Deserialize(streamTask.ToString(), typeof(Root)) as Root;
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
        public async Task<Root> Trending(string place)
        {
            var uri = "https://cloud-computing-sep6.ew.r.appspot.com/movies/trending" + place;
            var streamTask = await client.GetStringAsync(uri);
            Root recordDetails = JsonSerializer.Deserialize(streamTask.ToString(), typeof(Root)) as Root;
            return recordDetails;

        }
        public Task<Root> ProcessPlace(string place)
        {
            switch (place)
            {
                case "Bot 50 Movies": return Trending("bot");
                //case "Most 50 Voted Movies": return MostVoted("most");
                case "Least 50 Voted Movies": return Trending("less");
                default: return Trending("top");
            }
        }
 
        public async Task LogIn(UserDom user)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(user);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            string uri = " ";
            var streamTask = client.PostAsync(uri, content);
            string result = await streamTask.Result.Content.ReadAsStringAsync();
            var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(API_KEY));
            FireAuthorization token = JsonSerializer.Deserialize(streamTask.ToString(), typeof(FireAuthorization)) as FireAuthorization;
            Console.WriteLine(result);

        }
        
        public void LogOut()
        {
            token = null;
        }
        
        public async Task SingUp(UserDom user)
        {
            var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(API_KEY));
            var a = await auth.CreateUserWithEmailAndPasswordAsync(user.email, user.password);
            _ = LogIn(user);

        }
        

    }
}
