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
       
        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public IEnumerable<Doctor> GetActiveDoctors()
        {
            return _doctors.Where(d => d.IsActive).ToList();
        }

        public Doctor GetDoctorById(Guid id)
        {
            return _doctors.FirstOrDefault(d => d.Id == id);
        }

        public void DeleteDoctor(Guid id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.Id == id);
            if (doctor != null)
            {
                doctor.IsActive = false;
            }
        }

        public Speciality? GetSpecialityByName(string name)
        {
            return _specialities.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public bool LicenseExists(string licenseNumber)
        {
            return _doctors.Any(d => d.LicenseNumber.Equals(licenseNumber, StringComparison.OrdinalIgnoreCase));
        }
    }
}