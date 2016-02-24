using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using Dimogir.DomainModel;
using System.Linq.Expressions;
using System.Threading;

namespace Dimogir.Services
{
    public interface ILessonService : IRepository<Lesson, int>
    {
        bool Exists(Lesson lesson);

        Lesson GetParentOf(Lesson lesson);

        Lesson[] GetChildrenOf(Lesson lesson);

        int GetDepthOf(Lesson lesson);

        int GetHeightOf(Lesson lesson);

        Lesson[] GetSubtreeOf(Lesson lesson, int maxLevel = -1);

        void MoveSubtree(int rootId, int newParentId);

        void DeleteSubtree(int rootId);

        Lesson[] Find(string categoryKey);

        Lesson[] GetOrderedByTitle(bool desc = false);
    }

    public class LessonService : Repository<Lesson,int>, ILessonService
    {
        public const int MaxTreeDepth = 32;

        public bool Exists(Lesson lesson)
        {
            return Get().Contains(lesson);
        }

        public Lesson GetParentOf(Lesson lesson)
        {
            if(lesson == null)
                throw new ArgumentNullException(nameof(lesson));
            if(!Exists(lesson))
                throw new ArgumentException("The value of lesson is not in the database");

            return Get().ParentOf(lesson);
        }

        public Lesson[] GetChildrenOf(Lesson lesson)
        {
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));
            if (!Exists(lesson))
                throw new ArgumentException("The value of lesson is not in the database");

            return Get().WhereChildrenOf(lesson).ToArray();
        }

        public int GetDepthOf(Lesson lesson)
        {
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));
            if (!Exists(lesson))
                throw new ArgumentException("The value of lesson is not in the database");

            return Get().DepthOf(lesson);
        }

        public int GetHeightOf(Lesson lesson)
        {
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));
            if (!Exists(lesson))
                throw new ArgumentException("The value of lesson is not in the database");

            return Get().HeightOf(lesson);
        }

        public Lesson[] GetSubtreeOf(Lesson lesson, int maxLevel = -1)
        {
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));

            return Get().SubtreeOf(lesson, maxLevel);
        }

        public void MoveSubtree(int rootId, int newParentId)
        {
            Lesson root = Load(rootId);

            if (root == null)
                throw new ArgumentException("A record with Id == rootId is not in the database");

            Lesson newParent = Load(newParentId);

            if (newParent == null)
                throw new ArgumentException("A record with Id == newParentId is not in the database");

            if (GetHeightOf(root) + GetDepthOf(newParent) > MaxTreeDepth)
                throw new InvalidOperationException("Depth of the lesson hierarchy cannot exceed the maximum value of MaxTreeDepth = " + MaxTreeDepth);

            if (root.CategoryId != newParent.CategoryId)
                ChangeCategoryOfSubtree(root, newParent.CategoryId);
        }

        public void DeleteSubtree(int rootId)
        {
            Lesson root = Load(rootId);

            if (root == null)
                throw new ArgumentException("A record with Id == rootId is not in the database");

            Lesson[] subtree = GetSubtreeOf(root);

            Delete(root);

            foreach (var lesson in subtree)
                Delete(lesson);
        }

        private void ChangeCategoryOfSubtree(Lesson root, string categoryId)
        {
            Lesson[] subtree = GetSubtreeOf(root);

            Delete(root);
            root.CategoryId = categoryId;
            Create(root);

            foreach (var lesson in subtree)
            {
                Delete(lesson);
                lesson.CategoryId = categoryId;
                Create(lesson);
            }
        }

        public Lesson[] Find(string categoryKey)
        {
            return Get().WhereCategory(categoryKey).ToArray();
        }

        public Lesson[] GetOrderedByTitle(bool desc /* = false */)
        {
            return desc ? Get().OrderByDescending(lesson => lesson.Title).ToArray() : Get().OrderBy(lesson => lesson.Title).ToArray();
        }

        public LessonService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    public static class LessonUtilities
    {
        public static Lesson ParentOf(this IQueryable<Lesson> lessons, Lesson lesson)
        {
            if (lessons == null)
                throw new ArgumentNullException(nameof(lessons));
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));

            return lesson.ParentId == null ? null
                                           : lessons.Find((int)lesson.ParentId);
        }

        public static IQueryable<Lesson> WhereChildrenOf(this IQueryable<Lesson> lessons, Lesson lesson)
        {
            if (lessons == null)
                throw new ArgumentNullException(nameof(lessons));
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));

            return lessons.Where(les => les.ParentId == lesson.Id);
        }


        public static int DepthOf(this IQueryable<Lesson> lessons, Lesson lesson)
        {
            if (lessons == null)
                throw new ArgumentNullException(nameof(lessons));
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));

            int depth = 0;
            Lesson currentLesson = lesson;

            while (currentLesson.ParentId != null)
            {
                currentLesson = lessons.ParentOf(currentLesson);

                //if(currentLesson == null)
                //    throw new Exception("ПЕСДА");

                depth++;
            }

            return depth;
        }

        public static int HeightOf(this IQueryable<Lesson> lessons, Lesson lesson)
        {
            if (lessons == null)
                throw new ArgumentNullException(nameof(lessons));
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));

            Lesson[] children = lessons.WhereChildrenOf(lesson).ToArray();

            if (children.Length == 0)
                return 0;

            int[] candidates = new int[children.Length];

            for (int i = 0; i < children.Length; i++)
            {
                candidates[i] = lessons.HeightOf(children[i]) + 1;
            }

            return candidates.Max();
        }

        public static Lesson[] SubtreeOf(this IQueryable<Lesson> lessons, Lesson lesson, int maxLevel = -1)
        {
            if (lessons == null)
                throw new ArgumentNullException(nameof(lessons));
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));

            if (maxLevel == 0)
                return default(Lesson[]);

            List<Lesson> ret = new List<Lesson>();
            Lesson[] children = lessons.WhereChildrenOf(lesson).ToArray();
            
            if (children.Length == 0)
                return default(Lesson[]);

            Lesson[][] rest = new Lesson[children.Length][];

            for (int i = 0; i < children.Length; i++)
            {
                rest[i] = SubtreeOf(lessons, children[i], maxLevel == -1 ? -1 : maxLevel - 1);
                ret.Add(children[i]);
                ret.AddRange(rest[i]);
            }

            return ret.ToArray();
        }

        public static IQueryable<Lesson> WhereCategory(this IQueryable<Lesson> lessons, string categoryKey)
        {
            if (lessons == null)
                throw new ArgumentNullException(nameof(lessons));
            if (categoryKey == null)
                throw new ArgumentNullException(nameof(categoryKey));

            return lessons.Where(lesson => lesson.CategoryId == categoryKey);
        }
    }
}
