using SinaiAPI.Models;

namespace SinaiAPI.Services
{
    public class GuideService
    {
        private readonly SinaiDbContext _context;

        public GuideService(SinaiDbContext context) {
            _context = context;
        }

        public IQueryable<Guide> GetGuides()
        {
            return _context.Guides;
        }

        public Guide? GetGuide(int id)
        {
            return _context.Guides.SingleOrDefault(x => x.Id == id);
        }

        public void PostGuide(Guide guide)
        {
            if (guide == null)
            {
                throw new ArgumentNullException(nameof(guide));
            }

            var guideModel = new Guide { 
                Id = guide.Id,
                Title = guide.Title,
                Description = guide.Description,
                Language = guide.Language,
            };

            _context.Guides.Add(guideModel);
            _context.SaveChanges();
        }

        public bool DeleteGuide(int id)
        {
            var guide = _context.Guides.SingleOrDefault(x => x.Id == id);

            if (guide == null)
            { 
                throw new KeyNotFoundException(nameof(guide));
            }

            _context.Guides.Remove(guide);
            _context.SaveChanges();

            return true;
        }

        public void UpdateGuide(int id, Guide updatedGuide)
        {
            var guide = _context.Guides.SingleOrDefault(x => x.Id == id);

            if (guide == null)
            {
                throw new KeyNotFoundException($"Guide with ID {id} not found.");
            }

            guide.Title = updatedGuide.Title;
            guide.Description = updatedGuide.Description;
            guide.Language = updatedGuide.Language;

            _context.SaveChanges();
        }
    }
}
