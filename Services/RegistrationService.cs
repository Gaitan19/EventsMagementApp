﻿using EventsManagementApp.Models;
using EventsManagementApp.Repositories;
using EventsManagementsApp.DTOs;
using System;
using System.Collections.Generic;

namespace EventsManagementApp.Service
{
    public class RegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationService(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public IEnumerable<Registration> GetAllRegistrations() => _registrationRepository.GetAll();
        public Registration GetRegistrationById(Guid id) => _registrationRepository.GetById(id);
        public void CreateRegistration(RegistrationDTO registrationDto)
        {
            var registration = new Registration
            {
                ParticipantId = registrationDto.ParticipantId,
                EventId = registrationDto.EventId,
                RegistrationDate = registrationDto.RegistrationDate
            };

            _registrationRepository.Add(registration);
            _registrationRepository.SaveChanges();
        }
        public void UpdateRegistration(Guid id,RegistrationDTO registrationDto)
        { 
            var registration = _registrationRepository.GetById(id);
            if (registration != null)
            {
                registration.ParticipantId = registrationDto.ParticipantId;
                registration.EventId = registrationDto.EventId;
                registration.RegistrationDate = registrationDto.RegistrationDate;

                _registrationRepository.Update(registration);
                _registrationRepository.SaveChanges();
            }
           
        }
        public void DeleteRegistration(Guid id) { _registrationRepository.Delete(id); _registrationRepository.SaveChanges(); }
    }
}
