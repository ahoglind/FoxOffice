namespace FoxOffice.Domain
{
    using System.Collections.Generic;
    using Khala.EventSourcing;

    public class MovieMemento : IMemento
    {
        public int Version { get; set; }

        public IEnumerable<Screening> Screenings { get; set; }

        public string Title { get; set; }
    }
}
