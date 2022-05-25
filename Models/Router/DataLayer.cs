using Cloud.Models.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FirebaseAdmin.Auth;
public interface IDataLayer
{
    Task<IList<Movies>> AllItems();
    Task<IList<Movies>> Trending(string place);
    Task<IList<Movies>> Favourite(string place);
    Task<IList<Movies>> MovieByName(string name);
    //Task<IList<Movies>> UserFavorites();
    Task<Movies> MovieById(int id);
    Task<Casting> ActorByName(string name);
    Task LogIn(UserDom user);
}



namespace Cloud.Models.Router
{
    public class DataLayer : IDataLayer
    {
        private static string API_KEY = "";
        public static DataLayer instance = null;
        private static readonly object padlock = new object();

        private static FireAuthorization token;
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

        public async Task<IList<Movies>> AllItems()
        {
            string uri = " ";;
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<List<Movies>>(stream);
            return movies;
        }
        public async Task<IList<Movies>> Trending(string place)
        {
            string uri = " " + place;
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            List<Movies> movies = JsonConvert.DeserializeObject<List<Movies>>(stream);
            return movies;
        }
        public async Task<IList<Movies>> Favourite(string place)
        {
            string uri = " " + place;
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            List<Movies> movies = JsonConvert.DeserializeObject<List<Movies>>(stream);
            return movies;
        }
        /*
        public async Task<IList<Movies>> UserFavorites()
        {

            var result = dbclient.Get(token.localId + "/");
            Console.WriteLine(result);
            return null;
        }
        */

        public async Task<IList<Movies>> MovieByName(string name)
        {
            string jsonString = @"{""name"": """ + name + @"""}";
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var uri = " ";
            var streamTask = client.PostAsync(uri, content);
            string result = await streamTask.Result.Content.ReadAsStringAsync();
            IList<Movies> moviez = JsonConvert.DeserializeObject<IList<Movies>>(result);
            return moviez;
        }

        public async Task<Movies> MovieById(int id)
        {
            string uri = "" + id;
            var streamTask = client.GetAsync(uri);
            var stream = await streamTask.Result.Content.ReadAsStringAsync();
            Movies movies = JsonConvert.DeserializeObject<Movies>(stream);
            uri = "" + movies.title;
            streamTask = client.GetAsync(uri);
            stream = await streamTask.Result.Content.ReadAsStringAsync();
            movies.moviePrint = stream;
            uri = "" + movies.title;
            streamTask = client.GetAsync(uri);
            stream = await streamTask.Result.Content.ReadAsStringAsync();
            movies.plot = stream;
            for (int i = 0; i < movies.cast.Count; i++)
            {
                if (!movies.cast[i].getActorName().Equals(""))
                {
                    Casting newstar = ActorByName(movies.cast[i].cast_name).Result;
                    movies.cast[i].avg_movie_rating = newstar.avg_movie_rating;
                }
            }

            return movies;
        }
        public async Task<Casting> ActorByName(string name)
        {
            string jsonString = @"{""name"": """ + name + @"""}";
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            string uri = "";
            var streamTask = client.PostAsync(uri, content);
            string result = await streamTask.Result.Content.ReadAsStringAsync();
            IList<Casting> stars = JsonConvert.DeserializeObject<IList<Casting>>(result);
            return stars[0];
        }
        public async Task LogIn(UserDom user)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(user);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            string uri = " ";
            var streamTask = client.PostAsync(uri, content);
            string result = await streamTask.Result.Content.ReadAsStringAsync();
            var auth = new FirebaseAuth(new FirebaseConfig(API_KEY));
            token = JsonConvert.DeserializeObject<FireAuthorization>(result);
            Console.WriteLine(result);

        }
    }
}
