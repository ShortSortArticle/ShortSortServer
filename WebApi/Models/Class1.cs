using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Per
    {
        public string UserId { get; set; }
        public string OpSend { get; set; }
    }

    public class NewArtFromReact
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class ArtToSearch
    {
        public string Name { get; set; }
        public List<int> CatsId { get; set; }
    }
    public class TwoNumbers
    {
        public int Key { get; set; }
        public int Value { get; set; }
    }

    public class NewCatFromReact
    {
        public string Kod { get; set; }
        public string Name { get; set; }
        public List<NewArtFromReact> Arts { get; set; }
    }
    public class AddArtsToCatFromReact
    {
        public int Id { get; set; }
        public List<NewArtFromReact> Arts { get; set; }
    }
}