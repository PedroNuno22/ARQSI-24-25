using Microsoft.AspNetCore.Mvc;  // Imports ASP.NET Core MVC classes for handling HTTP requests and responses
using System.Collections.Generic;  //Imports IEnumerable and List<T>
using PatientManagement.Domain.Entities;  // Imports the Patient entity class from the Domain layer
using PatientManagement.Domain.Interfaces;  // Imports the IPatientRepository interface

namespace PatientManagement.Controllers
{
    // Marks this class as an API controller and allows it to handle HTTP requests
    [ApiController]
    // Defines the route for the controller. The route will be "api/patient" as [controller] will be replaced with the controller's name.
    [Route("api/[controller]")]
    public class PatientController : ControllerBase  // Inherits from ControllerBase, which provides basic API functionality
    {
        // A private field to hold the reference to the patient repository
        private readonly IPatientRepository _repository;

        // Constructor that uses Dependency Injection (DI) to inject an implementation of IPatientRepository
        public PatientController(IPatientRepository repository)
        {
            _repository = repository;  // Assigns the injected repository to the private field
        }

        // GET: api/patient
        // This method handles GET requests to retrieve all patients.
        // It returns an HTTP 200 (OK) status code with the list of patients from the repository.
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetAll()
        {
            return Ok(_repository.GetAll());  // Calls the GetAll method of the repository and returns the result in an HTTP 200 response
        }

        // GET: api/patient/{id}
        // This method handles GET requests to retrieve a patient by ID.
        // It returns an HTTP 200 (OK) with the patient data if found, or an HTTP 404 (NotFound) if not found.
        [HttpGet("{id}")]
        public ActionResult<Patient> GetById(int id)
        {
            var patient = _repository.GetById(id);  // Calls the repository to get a patient by their ID
            if (patient == null)
            {
                return NotFound();  // If no patient is found, return a 404 status code
            }
            return Ok(patient);  // If found, return the patient data with an HTTP 200 status code
        }

        // POST: api/patient
        // This method handles POST requests to create a new patient.
        // It returns an HTTP 201 (Created) status with the newly created patient.
        [HttpPost]
        public ActionResult<Patient> Create([FromBody] Patient patient)
        {
            var createdPatient = _repository.Create(patient);  // Calls the repository to create a new patient
            // Returns a 201 Created response with the route to get the created patient and the patient data
            return CreatedAtAction(nameof(GetById), new { id = createdPatient.Id }, createdPatient);
        }

    }
}
