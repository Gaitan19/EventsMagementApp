using EventsManagementApp.Context;
using EventsManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventsManagementApp.Repositories
{
    public interface IRegistrationRepository
    {
        IEnumerable<Registration> GetAll();
        Registration GetById(Guid id);
        void Add(Registration registration);
        void Update(Registration registration);
        void Delete(Guid id);
        void SaveChanges();
    }
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly DBContext _context;

        public RegistrationRepository(DBContext context)
        {
            _context = context;
        }

        public IEnumerable<Registration> GetAll() => _context.Registrations.ToList();
        public Registration GetById(Guid id) => _context.Registrations
            .Select(r => new Registration
        {

            Id = r.Id,
            EventId = r.EventId,
            ParticipantId = r.ParticipantId,
            RegistrationDate = r.RegistrationDate,
            Event = r.Event,
            Participant = r.Participant 
            }).FirstOrDefault(r => r.Id == id);

        public void Add(Registration registration) => _context.Registrations.Add(registration);
        public void Update(Registration registration) => _context.Registrations.Update(registration);
        public void Delete(Guid id) => _context.Registrations.Remove(GetById(id));
        public void SaveChanges() => _context.SaveChanges();
    }
}
