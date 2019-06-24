namespace FoxOffice.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FoxOffice.Events;
    using Khala.EventSourcing;

    public class Movie : EventSourced, IMementoOriginator
    {
        private readonly List<Screening> _screenings;

        public Movie(Guid movieId, string title)
            : this(movieId)
        {
            RaiseEvent(new MovieCreated { Title = title });
        }

        private Movie(Guid movieId)
            : base(movieId)
        {
            _screenings = new List<Screening>();
        }

        private Movie(Guid movieId, MovieMemento memento, IEnumerable<IDomainEvent> pastEvents)
            : base(movieId, memento)
        {
            Title = memento.Title;
            _screenings = memento.Screenings?.ToList();
            HandlePastEvents(pastEvents);
        }

        public string Title { get; private set; }

        public IEnumerable<Screening> Screenings => _screenings;

        private void Handle(MovieCreated domainEvent)
        {
            Title = domainEvent.Title;
        }

        public static Movie Factory(
            Guid movieId, IEnumerable<IDomainEvent> pastEvents)
        {
            var movie = new Movie(movieId);
            movie.HandlePastEvents(pastEvents);
            return movie;
        }

        public static Movie FactoryWithMemento(
            Guid movieId, IMemento memento, IEnumerable<IDomainEvent> pastEvents)
        {
            return new Movie(movieId, (MovieMemento)memento, pastEvents);
        }

        public void AddScreening(
            Guid screeningId,
            Guid theaterId,
            int seatRowCount,
            int seatColumnCount,
            DateTime screeningTime,
            decimal defaultFee,
            decimal childrenFee)
        {
            RaiseEvent(new ScreeningAdded
            {
                ScreeningId = screeningId,
                TheaterId = theaterId,
                SeatRowCount = seatRowCount,
                SeatColumnCount = seatColumnCount,
                ScreeningTime = screeningTime,
                DefaultFee = defaultFee,
                ChildrenFee = childrenFee,
            });
        }

        private void Handle(ScreeningAdded domainEvent)
        {
            _screenings.Add(Screening.Create(
                domainEvent.ScreeningId,
                domainEvent.TheaterId,
                domainEvent.SeatRowCount,
                domainEvent.SeatColumnCount,
                domainEvent.ScreeningTime,
                domainEvent.DefaultFee,
                domainEvent.ChildrenFee));
        }

        public IMemento SaveToMemento()
        {
            return new MovieMemento
            {
                Title = Title,
                Screenings = Screenings,
                Version = Version,
            };
        }
    }
}
