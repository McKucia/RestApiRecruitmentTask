using RestApiRecruitmentTask.Core.Models;

namespace RestApiRecruitmentTask.Core.Services
{
    public interface IProducerService
    {
        IEnumerable<Producer> GetAll();
        Producer GetById(int id);
        void Add(Producer producer);
        void Update(Producer producer);
        void Delete(int id);
    }

    public class ProducerService : IProducerService
    {
        private readonly List<Producer> _producers = new();
        private readonly ITireService _tireService;

        public ProducerService(ITireService tireService)
        {
            _tireService = tireService;
        }

        public IEnumerable<Producer> GetAll() => 
            _producers;

        public Producer GetById(int id) =>
            _producers.FirstOrDefault(p => p.Id == id);

        public void Add(Producer producer)
        {
            if (_producers.Any(t => t.Id == producer.Id))
                throw new InvalidOperationException($"Tire with Id {producer.Id} already exists.");

            _producers.Add(producer);
        }

        public void Update(Producer producer)
        {
            var existingProducer = GetById(producer.Id);
            if (existingProducer != null)
            {
                existingProducer.Name = producer.Name;
                existingProducer.Class = producer.Class;
            }
        }

        public void Delete(int id)
        {
            var producer = GetById(id);
            if (producer != null)
            {
                _tireService.DeleteByProducer(producer.Id);
                _producers.Remove(producer);
            }
        }
    }
}
