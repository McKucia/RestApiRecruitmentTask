using Microsoft.EntityFrameworkCore;
using RestApiRecruitmentTask.Core.Models;

namespace RestApiRecruitmentTask.Core.Services
{
    public interface IProducerService
    {
        IEnumerable<Producer> GetAll();
        Producer? GetById(int id);
        void Add(Producer producer);
        bool Update(int id, Producer producer);
        bool Delete(int id);
    }

    public class ProducerService : IProducerService
    {
        private readonly RestApiRecruitmentTaskDbContext _dbContext;
        private readonly ITireService _tireService;

        public ProducerService(ITireService tireService, RestApiRecruitmentTaskDbContext dbContext)
        {
            _dbContext = dbContext;
            _tireService = tireService;
        }

        public IEnumerable<Producer> GetAll()
        {
            return _dbContext
                .Producers
                .Include(p => p.Tires)
                .ToList();
        }

        public Producer? GetById(int id)
        {
            return _dbContext
                .Producers
                .FirstOrDefault(p => p.Id == id);
        }

        public void Add(Producer producer)
        {
            _dbContext.Producers.Add(producer);
            _dbContext.SaveChanges();
        }

        public bool Update(int id, Producer producer)
        {
            var existingProducer = GetById(id);

            if (existingProducer != null)
            {
                existingProducer.Name = producer.Name;
                existingProducer.Class = producer.Class;

                _dbContext.Entry(existingProducer).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return true;
            }
            else
                return false;
        }

        public bool Delete(int id)
        {
            var producer = GetById(id);

            if (producer != null)
            {
                _tireService.DeleteByProducer(producer.Id);

                _dbContext
                    .Producers
                    .Remove(producer);

                _dbContext.SaveChanges();

                return true;
            }
            else
                return false;
        }
    }
}
