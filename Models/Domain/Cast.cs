using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud.Models.Domain
{
    public class Cast
    {
        public Cast(bool adult, int gender, int id, string known_for_department, string name, string original_name, double popularity, string profile_path, int cast_id, string character, string credit_id, int order)
        {
            this.adult = adult;
            this.gender = gender;
            this.id = id;
            this.known_for_department = known_for_department;
            this.name = name;
            this.original_name = original_name;
            this.popularity = popularity;
            this.profile_path = profile_path;
            this.cast_id = cast_id;
            this.character = character;
            this.credit_id = credit_id;
            this.order = order;
        }

        public bool adult { get; set; }
        public int gender { get; set; }
        public int id { get; set; }
        public string known_for_department { get; set; }
        public string name { get; set; }
        public string original_name { get; set; }
        public double popularity { get; set; }
        public string profile_path { get; set; }
        public int cast_id { get; set; }
        public string character { get; set; }
        public string credit_id { get; set; }
        public int order { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Cast cast &&
                   adult == cast.adult &&
                   gender == cast.gender &&
                   id == cast.id &&
                   known_for_department == cast.known_for_department &&
                   name == cast.name &&
                   original_name == cast.original_name &&
                   popularity == cast.popularity &&
                   profile_path == cast.profile_path &&
                   cast_id == cast.cast_id &&
                   character == cast.character &&
                   credit_id == cast.credit_id &&
                   order == cast.order;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(adult);
            hash.Add(gender);
            hash.Add(id);
            hash.Add(known_for_department);
            hash.Add(name);
            hash.Add(original_name);
            hash.Add(popularity);
            hash.Add(profile_path);
            hash.Add(cast_id);
            hash.Add(character);
            hash.Add(credit_id);
            hash.Add(order);
            return hash.ToHashCode();
        }
    }
}
