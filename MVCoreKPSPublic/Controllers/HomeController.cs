using Microsoft.AspNetCore.Mvc;
using MVCoreKPSPublic.Models;
using MVCoreKPSPublic.Models.Notification;
using System.Diagnostics;

namespace MVCoreKPSPublic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotificationService _notificationService;


        public HomeController(ILogger<HomeController> logger, INotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService; 
        }

        public IActionResult Index()
        {
            ViewModel viewModel = new ViewModel();  
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Index(ViewModel model)
        {
            ServiceKPSPublic service=new ServiceKPSPublic();
            Response response = new Response(); 

            response._parametters.TCKimlikNo=model._parametters.TCKimlikNo;
            response._parametters.Ad = model._parametters.Ad;
            response._parametters.Soyad = model._parametters.Soyad;
            response._parametters.DogumYili = model._parametters.DogumYili;

            var result = service.OnGetService(response._parametters);

           if (result == null)
            {
                _notificationService.WarningNotification(message:"Servis çekilemedi");
            }
            else
            {
                if (result.Result==true)
                {
                    _notificationService.SuccessNotification(message: "TC KİMLİK NO DOĞRU");
                }
                else
                {
                    _notificationService.ErrorNotification(message: "KİMLİK NO YANLIŞ");
                }
            }

            return View(model);
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