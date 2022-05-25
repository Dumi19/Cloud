namespace Cloud.Models.Domain
{
    public class Casting
    {
        public Casting(int cast_id, string cast_name)
        {
            this.cast_id = cast_id;
            this.cast_name = cast_name;
        }

        public int cast_id { get;  set; }
        public string cast_name { get;  set; }
        public float avg_movie_rating { get; set; }
        public string getActorName()
        {
            if (cast_name == null || cast_name.Equals("Not in database"))
            {
                return "";
            }
            return cast_name;
        }
        public override bool Equals(object obj)
        {
            return obj is Casting actor &&
                   cast_id == actor.cast_id &&
                   cast_name == actor.cast_name;
        }
    }
}