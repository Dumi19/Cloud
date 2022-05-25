using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud.Models.Domain
{
    public class Movies
    {
        public Movies() { }

        public Movies (int id, string title, Director director, Ratings rating, List<Casting> cast, string moviePrint)
        {
            this.id = id;
            this.title = title;
            this.director = director;
            this.rating = rating;
            this.cast = cast;
            this.moviePrint = moviePrint;
        }
        public int id { get; set; }
        public string title { get; set; }

        public Director director { get; set; }

        public Ratings rating { get; set; }

        public List<Casting> cast { get; set; }
        public string moviePrint { get; set; }
        public string plot { get; set; }
        public string GetPoster()
        {
            if (moviePrint.Equals("N/A"))
            {
                return "https://cdn.discordapp.com/attachments/813364585800400989/849661531079835678/Untitled.png";
            }
            return moviePrint;
        }

        public string getDescription()
        {
            if (plot.Equals("N/A"))
            {
                return "No plot available";
            }
            return plot;
        }

        public override bool Equals(object obj)
        {
            return obj is Movies movie &&
                   id == movie.id &&
                   title == movie.title &&
                   EqualityComparer<Director>.Default.Equals(director, movie.director) &&
                   EqualityComparer<Ratings>.Default.Equals(rating, movie.rating) &&
                   EqualityComparer<List<Casting>>.Default.Equals(cast, movie.cast);
        }
    }


}

