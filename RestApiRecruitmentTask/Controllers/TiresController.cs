using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApiRecruitmentTask.Api.ViewModels;
using RestApiRecruitmentTask.Core.Models;
using RestApiRecruitmentTask.Core.Services;

namespace RestApiRecruitmentTask.Api.Controllers
{
    [Route("api/tires")]
    [ApiController]
    public class TiresController : ControllerBase
    {
        private readonly ITireService _tireService;
        private readonly IProducerService _producerService;
        private readonly IMapper _mapper;

        public TiresController(ITireService tireService, IProducerService producerService, IMapper mapper)
        {
            _tireService = tireService;
            _producerService = producerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all tires.
        /// </summary>
        /// <response code="200">Returns the list of all tires.</response>
        /// <response code="404">If no tire is found.</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            var tires = _tireService.GetAll();

            if (tires == null || !tires.Any())
                return NotFound("No tires found.");

            var tiresViewModel = _mapper.Map<IEnumerable<TireViewModel>>(tires);
            return Ok(tiresViewModel);
        }

        /// <summary>
        /// Gets a tire by its ID.
        /// </summary>
        /// <param name="id">The ID of the tire.</param>
        /// <response code="200">Returns the tire details.</response>
        /// <response code="404">If the tire is not found.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tire = _tireService.GetById(id);

            if (tire == null) return NotFound();

            var tireViewModel = _mapper.Map<TireViewModel>(tire);

            return Ok(tireViewModel);
        }

        /// <summary>
        /// Adds a new tire.
        /// </summary>
        /// <param name="tireViewModel">The tire object to add.</param>
        /// <response code="201">Returns the newly created tire.</response>
        /// <response code="400">If the tire model is invalid.</response>
        [HttpPost]
        public IActionResult Create(TireViewModel tireViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producer = _producerService.GetById(tireViewModel.ProducerId);

            if (producer == null)
                return NotFound($"Cannot add tire - producer with ID {tireViewModel.ProducerId} not found.");

            var tire = _mapper.Map<Tire>(tireViewModel);
            _tireService.Add(tire);
            var createdTire = _mapper.Map<TireViewModel>(tire);
            return CreatedAtAction(nameof(GetById), new { id = tire.Id }, createdTire);
        }

        /// <summary>
        /// Updates an existing tire.
        /// </summary>
        /// <param name="id">The ID of the tire to update.</param>
        /// <param name="tireViewModel">The tire object with updated data.</param>
        /// <response code="200">Returns the updated tire.</response>
        /// <response code="404">If the tire is not found.</response>
        [HttpPut("{id}")]
        public IActionResult Update(int id, TireViewModel tireViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tire = _mapper.Map<Tire>(tireViewModel);
            var isChanged = _tireService.Update(id, tire);

            if (!isChanged) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a tire by its ID.
        /// </summary>
        /// <param name="id">The ID of the tire to delete.</param>
        /// <response code="204">If the tire was deleted successfully.</response>
        /// <response code="404">If the tire is not found.</response>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _tireService.Delete(id);

            if (!isDeleted) return NotFound();

            return NoContent();
        }
    }
}
