using EventsManagementApp.Context;
using EventsManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventsManagementApp.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();
        Event GetById(Guid id);
        void Add(Event eventEntity);
        void Update(Event eventEntity);
        void Delete(Guid id);
        void SaveChanges();
    }
    public class EventRepository : IEventRepository
    {
        private readonly DBContext _context;

        public EventRepository(DBContext context)
        {
            _context = context;
        }

        //public IEnumerable<Event> GetAll() =>
        //    _context.Events
        //        .Include(e => e.Organizer)
        //        .Include(e => e.Registrations)
        //            .ThenInclude(r => r.Participant) // <<--- este es el include anidado
        //        .Include(e => e.Sponsors)
        //        .ToList();

        public IEnumerable<Event> GetAll() =>
                _context.Events
                .Select(e => new Event
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Date = e.Date,
                    Location = e.Location,
                    MaxCapacity = e.MaxCapacity,
                    OrganizerId = e.OrganizerId,
                    Organizer = new Organizer {
                        Id = e.Organizer.Id,
                        Name = e.Organizer.Name,
                        Email = e.Organizer.Email,
                        Phone = e.Organizer.Phone
                    },
                    Registrations = e.Registrations,
                    Sponsors = e.Sponsors
                }).ToList();
        public Event GetById(Guid id) => _context.Events
             .Select(e => new Event
             {
                 Id = e.Id,
                 Name = e.Name,
                 Description = e.Description,
                 Date = e.Date,
                 Location = e.Location,
                 MaxCapacity = e.MaxCapacity,
                 OrganizerId = e.OrganizerId,
                 Organizer = e.Organizer,
                 Registrations = (ICollection<Registration>)e.Registrations.Select(r => new Registration
                 { 
                   Id = r.Id,
                   ParticipantId = r.ParticipantId,
                   Participant = r.Participant,
                   EventId = r.EventId

                 }),
                 Sponsors = e.Sponsors
             })
            .FirstOrDefault(e => e.Id == id);

        public void Add(Event eventEntity) => _context.Events.Add(eventEntity);
        public void Update(Event eventEntity) => _context.Events.Update(eventEntity);
        public void Delete(Guid id) => _context.Events.Remove(GetById(id));
        public void SaveChanges() => _context.SaveChanges();
    }
}
