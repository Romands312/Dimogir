using System;
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
        private const int MAX_DEPTH = 32;

        public Lesson GetParent(int parentId)
        {
            return Get().SingleOrDefault(les => les.Id == parentId);
        }

        public Lesson[] GetChildren(Lesson lesson)
        {
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));

            return Get().Where(les => les.ParentId == lesson.Id).ToArray();
        }

        public int GetDepth(Lesson lesson)
        {
            int depth = 0;
            Lesson currentLesson = lesson;

            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));
            if (!Get().Contains(lesson))
                throw new ArgumentException("The value of lesson is not in the database");

            while (true)
            {
                if (currentLesson.ParentId == null)
                    break;

                currentLesson = GetParent(currentLesson.ParentId ?? 0);

                //if(currentLesson == null)
                //    throw new Exception("ПЕСДА В БАЗЕ ДАННЫХ");

                depth++;
            }

            return depth;
        }

        private Lesson GetFarthestDescendant(Lesson lesson, out int depth, int curDepth = 0)
        {
            Lesson[] children = GetChildren(lesson);
            
            if(children.Length == 0)
            {
                depth = curDepth;
                return lesson;
            }

            Lesson[] lastLessons = new Lesson[children.Length];
            int[] depths = new int[children.Length];

            for (int i = 0; i < children.Length; i++)
            {
                lastLessons[i] = GetFarthestDescendant(children[i], out depths[i], curDepth + 1);
            }

            int maxDepthIndex = Utilities.MaxWithIndex(depths, x => x).Item2;

            depth = depths[maxDepthIndex];
            return lastLessons[maxDepthIndex];
        }

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
