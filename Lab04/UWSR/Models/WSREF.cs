using System.Collections.Generic;

namespace UWSR.Models
{
    public class WSREF
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Minus { get; set; }
        public int Plus { get; set; }

        public List<WSREFCOMMENT> Comments { get; set; } = new List<WSREFCOMMENT>();
    }
}
