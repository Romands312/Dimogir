namespace Dimogir.DomainModel
{
    public enum ExerciseType
    {
        Test,
        FillText
    }

    public class Exercise : Entity<int>
    {
        public int Type { get; set; }
 
        public string Answer { get; set; }

        public string Description { get; set; }

        public int LessonId { get; set; }
    }
}
