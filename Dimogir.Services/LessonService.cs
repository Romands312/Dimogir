using System.Linq;
using Dimogir.DataAccess;
using Dimogir.DomainModel;

namespace Dimogir.Services
{
    public interface ILessonService : IRepository<Lesson, int>
    {
        Lesson[] Find(string categoryKey);
    }

    public class LessonService : Repository<Lesson,int>, ILessonService
    {

        public Lesson[] Find(string categoryKey)
        {
            return Get().Where(lesson => lesson.CategoryId == categoryKey).ToArray();
        }

        public LessonService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
