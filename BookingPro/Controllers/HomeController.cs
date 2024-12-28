using System;
using System.Web.Mvc;
using BookingPro.Data;
using Dapper;

namespace BookingPro.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbContext dbContext;

        public HomeController()
        {
            dbContext = new DbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestConnection()
        {
            string message;

            try
            {
                using (var connection = dbContext.CreateConnection())
                {
                    connection.Open();
                    var query = "SELECT 1";
                    var result = connection.ExecuteScalar(query);
                    message = result != null ? "Connection successful!" : "Connection failed!";
                }
            }
            catch (Exception ex)
            {
                message = $"Connection failed: {ex.Message}";
            }

            ViewBag.Message = message;
            return View();
        }
    }
}
