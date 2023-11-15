using BestMovies.Helpers;
using BestMovies.Interfaces;
using BestMovies.Models;
using BestMovies.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;
//controllers control how your data is presented it is like a folder type address where the route you type in decides
//which controller is called. route -> controller -> model

namespace BestMovies.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository _clubRepository;

        public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository)
        {
            _logger = logger;
            _clubRepository = clubRepository;

        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeViewModel = new HomeViewModel();
            //trycatch because its asyncronous because we are trying to retrieve data from 
            //a website or another network and alot of errors may occur during this process.
            //its just safer
            try
            {
                string url = "https://ipinfo.io?token=a61adf362f9f06"; //token recieved from ipinfo.io it will give you access to the endpoint
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info); //taking json and turning it into an object
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country); //globalization object 
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.State = ipInfo.Region;
                if (homeViewModel.City != null)// if we get anything back
                {
                    homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);
                }
                else
                {
                    homeViewModel.Clubs = null;
                }
                return View(homeViewModel);

            }
            catch (Exception ex)
            {
                homeViewModel.Clubs = null;
            }
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}