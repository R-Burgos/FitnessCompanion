namespace FitnessCompanion.Models
{
    public class RecipeModel
    {
        public RecipeModel()
        {

        }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ImageType { get; set; }
        public string Calories { get; set; }
        public string Protein { get; set; }
        public string Fat { get; set; }
        public string Carbs { get; set; }
        public string SourceUrl { get; set; }
    }
}
