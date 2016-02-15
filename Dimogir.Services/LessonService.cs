using System.Linq;
using Dimogir.DomainModel;

namespace Dimogir.Services
{
    public interface ILessonService : IRepository<Lesson, int>
    {
        Lesson[] Find(string categoryKey);

        Lesson[] GetOrderedByTitle(bool desc = false);
    }

    public class LessonService : Repository<Lesson,int>, ILessonService
    {

        public Lesson[] Find(string categoryKey)
        {
            return Get().Where(lesson => lesson.CategoryId == categoryKey).ToArray();
        }

        public Lesson[] GetOrderedByTitle(bool desc /* = false */)
        {
            return desc ? Get().OrderByDescending(lesson => lesson.Title).ToArray() : Get().OrderBy(lesson => lesson.Title).ToArray();
        }

        public LessonService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
