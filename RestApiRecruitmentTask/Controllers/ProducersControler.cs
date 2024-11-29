using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApiRecruitmentTask.Api.ViewModels;
using RestApiRecruitmentTask.Core.Models;
using RestApiRecruitmentTask.Core.Services;

namespace RestApiRecruitmentTask.Api.Controllers
{
    [Route("api/producers")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IProducerService _producerService;
        private readonly IMapper _mapper;

        public ProducersController(IProducerService producerService, IMapper mapper)
        {
            _producerService = producerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all producers.
        /// </summary>
        /// <response code="200">Returns the list of all producers.</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            var producers = _producerService.GetAll();
            var producersViewModel = _mapper.Map<IEnumerable<ProducerViewModel>>(producers);

            return Ok(producersViewModel);
        }

        /// <summary>
        /// Gets a producer by its ID.
        /// </summary>
        /// <param name="id">The ID of the producer.</param>
        /// <response code="200">Returns the producer details.</response>
        /// <response code="404">If the producer is not found.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var producer = _producerService.GetById(id);

            if (producer == null) return NotFound();

            var producetViewModel = _mapper.Map<ProducerViewModel>(producer);

            return Ok(producetViewModel);
        }

        /// <summary>
        /// Adds a new producer.
        /// </summary>
        /// <param name="producerViewModel">The producer object to add.</param>
        /// <response code="201">Returns the newly created producer.</response>
        /// <response code="400">If the producer model is invalid.</response>
        [HttpPost]
        public IActionResult Create(ProducerViewModel producerViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producer = _mapper.Map<Producer>(producerViewModel);
            _producerService.Add(producer);

            var createdProducer = _mapper.Map<ProducerViewModel>(producer);

            return CreatedAtAction(nameof(GetById), new { id = producer.Id }, createdProducer);
        }

        /// <summary>
        /// Updates an existing producer.
        /// </summary>
        /// <param name="id">The ID of the producer to update.</param>
        /// <param name="producerViewModel">The producer object with updated data.</param>
        /// <response code="200">Returns the updated producer.</response>
        /// <response code="404">If the producer is not found.</response>
        [HttpPut("{id}")]
        public IActionResult Update(int id, ProducerViewModel producerViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producer = _mapper.Map<Producer>(producerViewModel);
            var isChanged =  _producerService.Update(id, producer);

            if (!isChanged) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a producer by its ID.
        /// </summary>
        /// <param name="id">The ID of the producer to delete.</param>
        /// <response code="204">If the producer was deleted successfully.</response>
        /// <response code="404">If the producer is not found.</response>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _producerService.Delete(id);

            if(!isDeleted) return NotFound();

            return NoContent();
        }
    }
}
