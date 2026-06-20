using Dsw2026Ej15.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors;
        private readonly List<Speciality> _specialities;

        private static PersistenceInMemory _instance;
        private static readonly object _lock = new object();

        private PersistenceInMemory()
        {
            _doctors = new List<Doctor>();
            _specialities = new List<Speciality>();

            LoadSpecialities();
        }

        public static PersistenceInMemory Instance
        {
            get
            {
                lock (_lock) 
                {
                    if (_instance == null)
                    {
                        _instance = new PersistenceInMemory();
                    }
                    return _instance;
                }
            }
        }

        public void LoadSpecialities()
        {
            throw new NotImplementedException();
        }

        public void AddDoctor(Doctor doctor) { throw new NotImplementedException(); }
        public IEnumerable<Doctor> GetActiveDoctors() { throw new NotImplementedException(); }
        public Doctor GetDoctorById(Guid id) { throw new NotImplementedException(); }
        public void DeleteDoctor(Guid id) { throw new NotImplementedException(); }
        public Speciality GetSpecialityByName(string name) { throw new NotImplementedException(); }
    }
}