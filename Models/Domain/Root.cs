using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud.Models.Domain
{
    public class Root
    {
        public Root(int page, List<Result> results, int total_pages, int total_results, List<Cast> cast)
        {
            this.page = page;
            this.results = results;
            this.total_pages = total_pages;
            this.total_results = total_results;
            this.cast = cast;
        }

        public int page { get; set; }
        public List<Result> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
        public List<Cast> cast { get; set; }
        
        public override bool Equals(object? obj)
        {
            return obj is Root root &&
                   page == root.page &&
                   EqualityComparer<List<Result>>.Default.Equals(results, root.results) &&
                   total_pages == root.total_pages &&
                   total_results == root.total_results;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(page, results, total_pages, total_results, cast);
        }
    }
}
