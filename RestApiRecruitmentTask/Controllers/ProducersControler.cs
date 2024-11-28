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

        [HttpGet]
        public IActionResult GetAll()
        {
            var producers = _producerService.GetAll();
            var producersViewModel = _mapper.Map<IEnumerable<ProducerViewModel>>(producers);

            return Ok(producersViewModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var producer = _producerService.GetById(id);

            if (producer == null) return NotFound();

            var producetViewModel = _mapper.Map<ProducerViewModel>(producer);

            return Ok(producetViewModel);
        }

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

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProducerViewModel producerViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producer = _producerService.GetById(id);

            if (producer == null) return NotFound();

            _mapper.Map(producerViewModel, producer);
            _producerService.Update(producer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var producer = _producerService.GetById(id);

            if (producer == null) return NotFound();

            _producerService.Delete(id);
            return NoContent();
        }
    }
}
