using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json.Linq;
using RestSharp;
using Method = RestSharp.Method;

namespace FitnessCompanion.Models
{
    public class API_RepoModel
    {
        //Conversion const
        const double lbToKg = 2.205; // Divide by
        const double inToCm = 2.54; // Mult by

        public API_RepoModel()
        {

        }
        public void GetApiBMR(UserModel user)
        {
            var key = File.ReadAllText("appsettings.json");
            var APIKey = JObject.Parse(key).GetValue("X-RapidAPI-Key").ToString();
            var APIHost = JObject.Parse(key).GetValue("X-RapidAPI-Host").ToString();

            //BMR GET
            var weight = user.Weight / lbToKg;
            var height = user.Height * inToCm;
            string urlBMR = $"https://mega-fitness-calculator1.p.rapidapi.com/bmr?weight={weight}" +
                $"&height={height}&age={user.Age}&gender={user.Gender}";
            
            var client = new RestClient(urlBMR);
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-RapidAPI-Key", APIKey);
            request.AddHeader("X-RapidAPI-Host", APIHost);
            IRestResponse response = client.Execute(request);
            user.BMR = double.Parse(JObject.Parse(response.Content)["info"]["bmr"].ToString());
        }
        public void GetApiTDEE(UserModel user)
        {
            var key = File.ReadAllText("appsettings.json");
            var APIKey = JObject.Parse(key).GetValue("X-RapidAPI-Key").ToString();
            var APIHost = JObject.Parse(key).GetValue("X-RapidAPI-Host").ToString();

            //TDEE GET
            var weight = user.Weight / lbToKg;
            var height = user.Height * inToCm;
            string urlTDEE = $"https://mega-fitness-calculator1.p.rapidapi.com/tdee?weight={weight}" +
                $"&height={height}&activitylevel={user.ActivityLevel}&age={user.Age}&gender={user.Gender}";

            var client = new RestClient(urlTDEE);
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-RapidAPI-Key", APIKey);
            request.AddHeader("X-RapidAPI-Host", APIHost);
            IRestResponse response = client.Execute(request);
            user.TDEE = double.Parse(JObject.Parse(response.Content)["info"]["tdee"].ToString());
        }
        public void GetApiRecipes(UserModel user)
        {
            var key = File.ReadAllText("appsettings.json");
            var APIKey = JObject.Parse(key).GetValue("X-RapidAPI-Key").ToString();
            var APIHost = JObject.Parse(key).GetValue("X-RapidAPI-Host1").ToString();

            //Recipes GET
            var minP = user.UserProtein / user.MealCount;
            var minF = user.UserFat / user.MealCount;
            var minC = user.UserCarbohydrate / user.MealCount;
            string urlRecipe = $"https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/" +
                $"recipes/findByNutrients?limitLicense=false&minProtein={minP}&minCarbs={minC}" +
                $"&maxFat={minF + 5}&maxProtein={minP + 15}&maxCarbs={minC + 30}&number={user.MealCount}" +
                $"&minFat={minF}";

            var client = new RestClient(urlRecipe);
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-RapidAPI-Key", APIKey);
            request.AddHeader("X-RapidAPI-Host1", APIHost);
            IRestResponse response = client.Execute(request);

            //Adding sample recipes to user
            for (int i = 0; i < user.MealCount; i++)
            {
                var recipeObj = new RecipeModel();

                recipeObj.Id = JArray.Parse(response.Content)[i]["id"].ToString();
                recipeObj.Title = JArray.Parse(response.Content)[i]["title"].ToString();
                recipeObj.Image = JArray.Parse(response.Content)[i]["image"].ToString();
                recipeObj.ImageType = JArray.Parse(response.Content)[i]["imageType"].ToString();
                recipeObj.Calories = JArray.Parse(response.Content)[i]["calories"].ToString();
                recipeObj.Protein = JArray.Parse(response.Content)[i]["protein"].ToString();
                recipeObj.Fat = JArray.Parse(response.Content)[i]["fat"].ToString();
                recipeObj.Carbs = JArray.Parse(response.Content)[i]["carbs"].ToString();

                user.SampleRecipes.Add(recipeObj);
            }
        }
        public void GetApiRecipeSourceUrl(UserModel user)
        {
            var key = File.ReadAllText("appsettings.json");
            var APIKey = JObject.Parse(key).GetValue("X-RapidAPI-Key").ToString();
            var APIHost = JObject.Parse(key).GetValue("X-RapidAPI-Host1").ToString();

            for (int i = 0; i < user.MealCount; i++)
            {
                foreach (RecipeModel recipe in user.SampleRecipes)
                {
                    //RecioeUrl GET
                    string urlSourceRecipe = $"https://spoonacular-recipe-food-nutrition" +
                        $"-v1.p.rapidapi.com/recipes/{user.SampleRecipes[i].Id}/information";

                    var client = new RestClient(urlSourceRecipe);
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("X-RapidAPI-Key", APIKey);
                    request.AddHeader("X-RapidAPI-Host1", APIHost);
                    IRestResponse response = client.Execute(request);

                    user.SampleRecipes[i].SourceUrl = JObject.Parse(response.Content)["sourceUrl"].ToString();
                }
            }


        }
    }
}
