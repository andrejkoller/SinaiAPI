using SinaiAPI.Data;
using SinaiAPI.Models;

namespace SinaiAPI.Services
{
    public class FAQService(SinaiDbContext context)
    {
        public IQueryable<FAQ> GetGuides()
        {
            return context.FAQs;
        }

        public FAQ? GetGuide(int id)
        {
            return context.FAQs.SingleOrDefault(x => x.Id == id);
        }

        public void PostGuide(FAQ guide)
        {
            if (guide != null)
            {
                var guideModel = new FAQ
                {
                    Id = guide.Id,
                    Title = guide.Title,
                    Description = guide.Description,
                    Language = guide.Language,
                };

                context.FAQs.Add(guideModel);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(guide));
            }
        }

        public bool DeleteGuide(int id)
        {
            var guide = context.FAQs.SingleOrDefault(x => x.Id == id);

            if (guide != null)
            {
                context.FAQs.Remove(guide);
                context.SaveChanges();

                return true;
            }

            throw new KeyNotFoundException(nameof(guide));
        }

        public void UpdateGuide(int id, FAQ updatedGuide)
        {
            var guide = context.FAQs.SingleOrDefault(x => x.Id == id)
                ?? throw new KeyNotFoundException($"FAQ with ID {id} not found.");

            guide.Title = updatedGuide.Title;
            guide.Description = updatedGuide.Description;
            guide.Language = updatedGuide.Language;

            context.SaveChanges();
        }
    }
}
