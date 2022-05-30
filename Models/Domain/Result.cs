using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud.Models.Domain
{
    public class Result
    {
        public Result(bool adult, string backdrop_path, List<int> genre_ids, int id, string original_language, string original_title, string overview, double popularity, string poster_path, string release_date, string title, bool video, double vote_average, int vote_count)
        {
            this.adult = adult;
            this.backdrop_path = backdrop_path;
            this.genre_ids = genre_ids;
            this.id = id;
            this.original_language = original_language;
            this.original_title = original_title;
            this.overview = overview;
            this.popularity = popularity;
            this.poster_path = poster_path;
            this.release_date = release_date;
            this.title = title;
            this.video = video;
            this.vote_average = vote_average;
            this.vote_count = vote_count;
        }

        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Result result &&
                   adult == result.adult &&
                   backdrop_path == result.backdrop_path &&
                   EqualityComparer<List<int>>.Default.Equals(genre_ids, result.genre_ids) &&
                   id == result.id &&
                   original_language == result.original_language &&
                   original_title == result.original_title &&
                   overview == result.overview &&
                   popularity == result.popularity &&
                   poster_path == result.poster_path &&
                   release_date == result.release_date &&
                   title == result.title &&
                   video == result.video &&
                   vote_average == result.vote_average &&
                   vote_count == result.vote_count;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(adult);
            hash.Add(backdrop_path);
            hash.Add(genre_ids);
            hash.Add(id);
            hash.Add(original_language);
            hash.Add(original_title);
            hash.Add(overview);
            hash.Add(popularity);
            hash.Add(poster_path);
            hash.Add(release_date);
            hash.Add(title);
            hash.Add(video);
            hash.Add(vote_average);
            hash.Add(vote_count);
            return hash.ToHashCode();
        }
    }
}


