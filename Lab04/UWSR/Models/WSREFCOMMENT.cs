using System;

namespace UWSR.Models
{
    public class WSREFCOMMENT
    {
        public int Id { get; set; }
        public int WSREFId { get; set; }
        public string SessionId { get; set; } = string.Empty;
        public DateTime Stamp { get; set; }
        public string ComText { get; set; } = string.Empty;

        public WSREF WSREF { get; set; }
    }
}
