using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpPost]
        public IActionResult CreateDoctor([FromBody] Doctor doctor)
        {
            if (string.IsNullOrWhiteSpace(doctor.Name) || string.IsNullOrWhiteSpace(doctor.LicenseNumber))
                throw new Exception("ValidationException: El nombre y la matrícula son obligatorios."); // Reemplazar con la clase ValidationException de tu compañero

            if (_persistence.LicenseExists(doctor.LicenseNumber))
                throw new Exception("ValidationException: Ya existe un médico con esa matrícula.");

            var speciality = _persistence.GetSpecialityByName(doctor.Speciality.Name);
            if (speciality == null)
                throw new Exception("ValidationException: La especialidad ingresada no existe.");

            doctor.Id = Guid.NewGuid();
            doctor.IsActive = true;
            doctor.Speciality = speciality;

            _persistence.AddDoctor(doctor);

            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
        }

        [HttpGet]
        public IActionResult GetActiveDoctors()
        {
            var activeDoctors = _persistence.GetActiveDoctors();
            return Ok(activeDoctors);
        }


        [HttpGet("{id}")]
        public IActionResult GetDoctorById(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);
            if (doctor == null || !doctor.IsActive)
                return NotFound();

            // Devolvemos solo los datos que pide el TP
            return Ok(new
            {
                Name = doctor.Name,
                LicenseNumber = doctor.LicenseNumber,
                SpecialityName = doctor.Speciality.Name
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);
            if (doctor == null || !doctor.IsActive)
                return NotFound();

            _persistence.DeleteDoctor(id);
            return NoContent();
        }
    }
}