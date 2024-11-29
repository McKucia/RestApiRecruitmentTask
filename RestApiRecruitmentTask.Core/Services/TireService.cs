using Microsoft.EntityFrameworkCore;
using RestApiRecruitmentTask.Core.Models;

namespace RestApiRecruitmentTask.Core.Services
{
    public interface ITireService
    {
        IEnumerable<Tire> GetAll();
        Tire? GetById(int id);
        void Add(Tire tire);
        bool Update(int id, Tire tire);
        bool Delete(int id);
        void DeleteByProducer(int producerId);
    }

    public class TireService : ITireService
    {
        private readonly RestApiRecruitmentTaskDbContext _dbContext;

        public TireService(RestApiRecruitmentTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Tire> GetAll()
        {
            return _dbContext
                .Tires
                .ToList();
        }

        public Tire? GetById(int id)
        {
            return _dbContext
                .Tires
                .FirstOrDefault(t => t.Id == id);
        }

        public void Add(Tire tire)
        {
            _dbContext.Tires.Add(tire);
            _dbContext.SaveChanges();
        }

        public bool Update(int id, Tire tire)
        {
            var existingTire = GetById(id);

            if (existingTire != null)
            {
                existingTire.Size = tire.Size;
                existingTire.TreadName = tire.TreadName;

                _dbContext.Entry(existingTire).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return true;
            }
            else
                return false;
        }

        public bool Delete(int id)
        {
            var tire = GetById(id);

            if (tire != null)
            {
                _dbContext
                    .Tires
                    .Remove(tire);

                _dbContext.SaveChanges();

                return true;
            }
            else
                return false;
        }

        public void DeleteByProducer(int producerId)
        {
            var relatedTires = _dbContext
                .Tires
                .Where(t => t.ProducerId == producerId)
                .ToList();

            if (relatedTires is not null && relatedTires.Any())
            {
                _dbContext.Remove(relatedTires);
                _dbContext.SaveChanges();
            }
        }
    }
}
