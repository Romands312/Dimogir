namespace Dimogir.DomainModel
{
    public enum ExerciseType
    {
        Test,
        FillText
    }

    public class Exercise
    {
        public string Id { get; set; }

        public int Type { get; set; }

        public string Answer { get; set; }

        public string Description { get; set; }
    }
}
