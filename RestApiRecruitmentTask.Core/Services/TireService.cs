using RestApiRecruitmentTask.Core.Models;

namespace RestApiRecruitmentTask.Core.Services
{
    public interface ITireService
    {
        IEnumerable<Tire> GetAll();
        Tire GetById(int id);
        void Add(Tire tire);
        void Update(Tire tire);
        void Delete(int id);
        void DeleteByProducer(int producerId);
    }

    public class TireService : ITireService
    {
        private readonly List<Tire> _tires = new();

        public IEnumerable<Tire> GetAll() =>
            _tires;

        public Tire GetById(int id) =>
            _tires.FirstOrDefault(t => t.Id == id);

        public void Add(Tire tire)
        {
            tire.Id = _tires.Count > 0 ? _tires.Max(t => t.Id) + 1 : 1;

            _tires.Add(tire);
        }

        public void Update(Tire tire)
        {
            var existingTire = GetById(tire.Id);
            if(existingTire != null)
            {
                existingTire.Size = tire.Size;
                existingTire.TreadName = tire.TreadName;
                existingTire.ProducerId = tire.ProducerId;
            }
        }

        public void Delete(int id) => 
            _tires.RemoveAll(t => t.Id == id);

        public void DeleteByProducer(int producerId) => 
            _tires.RemoveAll(t => t.ProducerId == producerId);
    }
}
