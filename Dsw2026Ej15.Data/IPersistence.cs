using Dsw2026Ej15.Domain;
using System;
using System.Collections.Generic;

namespace Dsw2026Ej15.Data
{
    public interface IPersistence
    {
        void LoadSpecialities();

        void AddDoctor(Doctor doctor);
        IEnumerable<Doctor> GetActiveDoctors();
        Doctor GetDoctorById(Guid id);
        void DeleteDoctor(Guid id);

        Speciality GetSpecialityByName(string name);
    }
}