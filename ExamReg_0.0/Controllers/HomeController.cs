using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExamReg_0._0.Models;

namespace ExamReg_0._0.Controllers
{
    public class HomeController : Controller
    {

  
        private readonly ILogger<HomeController> _logger;
        //public DbConnectionStrings DBS = new DbConnectionStrings();

        public HomeController(ILogger<HomeController> _logger)

        {

           
            //DBS.SqlServerConnectionString = "Data Source=45.251.114.50,1444;Initial Catalog=DevGroup_dev;User Id=dev;Password=devVn1200;";            _logger = logger;
        }

        public IActionResult Index()
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
