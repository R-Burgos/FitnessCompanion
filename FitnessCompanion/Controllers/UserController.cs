using Microsoft.AspNetCore.Mvc;
using FitnessCompanion.Models;

namespace FitnessCompanion.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Results(UserModel userModel) 
        {
            API_RepoModel apiGet = new API_RepoModel();
            apiGet.GetApiTDEE(userModel);
            apiGet.GetApiBMR(userModel);
            userModel.TDEE = Math.Round(userModel.TDEE, 0, MidpointRounding.AwayFromZero);
            userModel.BMR = Math.Round(userModel.BMR, 0, MidpointRounding.AwayFromZero);
            userModel.GoalCalories(userModel);
            userModel.GetMacros(userModel);
            apiGet.GetApiRecipes(userModel);
            apiGet.GetApiRecipeSourceUrl(userModel);

            return View("Results", userModel);
        }
    }
}