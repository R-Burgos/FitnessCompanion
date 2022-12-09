using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FitnessCompanion.Models
{
    public class UserModel
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        [DisplayName ("Activity")]
        public string ActivityLevel { get; set; }
        public string Gender { get; set; }
        [Range (1, 100)]
        public int Age { get; set; }
        [DisplayName("Diet Modifier")]
        public int CalorieTargetModifier { get; set; }  // 1,2,3,4,5
        [DisplayName("Macro Distribution")]
        public int CarbLevel { get; set; } // 1,2,3
        [Range (1, 8)]
        public int MealCount { get; set; }
        public double TDEE { get; set; }
        public double BMR { get; set; }
        public void GoalCalories(UserModel user)
        {
            switch (user.CalorieTargetModifier)
            {
                case 1:
                    user.DietCalories = Math.Round(user.TDEE - 500, 0, MidpointRounding.AwayFromZero);
                    break;
                case 2:
                    user.DietCalories = Math.Round(user.TDEE - 250, 0, MidpointRounding.AwayFromZero);
                    break;
                case 3:
                    user.DietCalories = Math.Round(user.TDEE, 0);
                    break;
                case 4:
                    user.DietCalories = Math.Round(user.TDEE + 250, 0, MidpointRounding.AwayFromZero);
                    break;
                case 5:
                    user.DietCalories = Math.Round(user.TDEE + 500, 0, MidpointRounding.AwayFromZero);
                    break;
            }
        }
        [DisplayName("Target Calories")]
        public double DietCalories { get; set; }
        public void GetMacros(UserModel user)
        {
            switch (user.CarbLevel)
            {
                case 1: // P30/F35/C35
                    user.UserProteinCalories = Math.Round(user.DietCalories * 0.3, 0, MidpointRounding.AwayFromZero);
                    user.UserProtein = Math.Round(user.UserProteinCalories / 4.0, 0, MidpointRounding.AwayFromZero);
                    user.UserFatCalories = Math.Round(user.DietCalories * 0.35, 0, MidpointRounding.AwayFromZero);
                    user.UserFat = Math.Round(user.UserFatCalories / 9.0, 0, MidpointRounding.AwayFromZero);
                    user.UserCarbohydrateCalories = Math.Round(user.DietCalories * 0.35, 0, MidpointRounding.AwayFromZero);
                    user.UserCarbohydrate = Math.Round(user.UserCarbohydrateCalories / 4.0, 0, MidpointRounding.AwayFromZero);
                    break;
                case 2: // P40/F40/C20
                    user.UserProteinCalories = Math.Round(user.DietCalories * 0.4, 0, MidpointRounding.AwayFromZero);
                    user.UserProtein = Math.Round(user.UserProteinCalories / 4.0, 0, MidpointRounding.AwayFromZero);
                    user.UserFatCalories = Math.Round(user.DietCalories * 0.4, 0, MidpointRounding.AwayFromZero);
                    user.UserFat = Math.Round(user.UserFatCalories / 9.0, 0, MidpointRounding.AwayFromZero);
                    user.UserCarbohydrateCalories = Math.Round(user.DietCalories * 0.2, 0, MidpointRounding.AwayFromZero);
                    user.UserCarbohydrate = Math.Round(user.UserCarbohydrateCalories / 4.0, 0, MidpointRounding.AwayFromZero);
                    break;
                case 3: // P30/F20/C50
                    user.UserProteinCalories = Math.Round(user.DietCalories * 0.3, 0, MidpointRounding.AwayFromZero);
                    user.UserProtein = Math.Round(user.UserProteinCalories / 4.0, 0, MidpointRounding.AwayFromZero);
                    user.UserFatCalories = Math.Round(user.DietCalories * 0.2, 0, MidpointRounding.AwayFromZero);
                    user.UserFat = Math.Round(user.UserFatCalories / 9.0, 0, MidpointRounding.AwayFromZero);
                    user.UserCarbohydrateCalories = Math.Round(user.DietCalories * 0.5, 0, MidpointRounding.AwayFromZero);
                    user.UserCarbohydrate = Math.Round(user.UserCarbohydrateCalories / 4.0, 0, MidpointRounding.AwayFromZero);
                    break;
            }
        }
        public double UserProtein { get; set; }
        public double UserProteinCalories { get; set; }
        public double UserFat { get; set; }
        public double UserFatCalories { get; set; }
        public double UserCarbohydrate { get; set; }
        public double UserCarbohydrateCalories { get; set; }
        public List<RecipeModel> SampleRecipes { get; set; } = new List<RecipeModel>();
    }
}
