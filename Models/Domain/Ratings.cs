namespace Cloud.Models.Domain
{
    public class Ratings
    {
        public Ratings(int id, string rating, int vote)
        {
            this.id = id;
            this.rating = rating;
            this.vote = vote;
        }

        public int id { get;  set; }
        public string rating { get;  set; }
        public int vote { get;  set; }
        public override bool Equals(object obj)
        {
            return obj is Ratings rating &&
                   id == rating.id &&
                   rating.Equals(rating.rating) &&
                   vote == rating.vote;
        }

    }
}