namespace Cloud.Models.Domain
{
    public class Director
    {
        public Director(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        private int id { get; set; }
        public string name { get; set; }
        public override bool Equals(object obj)
        {
            return obj is Director director &&
                   id == director.id &&
                   name == director.name;
        }
    }
}