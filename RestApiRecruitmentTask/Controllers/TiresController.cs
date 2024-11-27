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
        private readonly IMapper _mapper;

        public TiresController(ITireService tireService, IMapper mapper)
        {
            _tireService = tireService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tires = _tireService.GetAll();
            var tiresViewModel = _mapper.Map<IEnumerable<TireViewModel>>(tires);

            return Ok(tiresViewModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tire = _tireService.GetById(id);

            if (tire == null) return NotFound();

            var tireViewModel = _mapper.Map<TireViewModel>(tire);

            return Ok(tireViewModel);
        }

        [HttpPost]
        public IActionResult Create(TireViewModel tireViewModel)
        {
            try
            {
                var tire = _mapper.Map<Tire>(tireViewModel);
                _tireService.Add(tire);
                return CreatedAtAction(nameof(GetById), new { id = tire.Id }, tireViewModel);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TireViewModel tireViewModel)
        {
            var tire = _tireService.GetById(id);

            if (tire == null) return NotFound();

            _mapper.Map(tireViewModel, tire);
            _tireService.Update(tire);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tire = _tireService.GetById(id);

            if (tire == null) return NotFound();

            _tireService.Delete(id);
            return NoContent();
        }
    }
}
