using CounterApp1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Speech.Synthesis;

namespace CounterApp1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFaceDetectionService _faceDetectionService;

        public HomeController(ILogger<HomeController> logger, IFaceDetectionService faceDetectionService)
        {
            _logger = logger;
            _faceDetectionService = faceDetectionService;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Speak(int number, int counter)
        {
            try
            {
                var message = $"{number} counter {counter}";

                using (var synth = new SpeechSynthesizer())
                {
                    synth.SetOutputToDefaultAudioDevice();
                    synth.Speak(message);
                }

                ViewData["VoiceMessage"] = $"Voice message generated: {message}";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        public IActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadAndDetectFace(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                using (Stream imageStream = image.OpenReadStream())
                {
                    var containsFace = _faceDetectionService.ContainsFace(imageStream);

                    if (containsFace)
                    {
                        ViewBag.Message = "Face detected!";
                    }
                    else
                    {
                        ViewBag.Message = "No face detected. Maybe it's a room?";
                    }
                }
            }

            return View("UploadImage");
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadAndDetectFace(ImageUploadModel model)
        //{
        //    if (model.Image != null && model.Image.Length > 0)
        //    {
        //        using (Stream imageStream = model.Image.OpenReadStream())
        //        {
        //            var faces = await _faceDetectionService.DetectFacesAsync(imageStream);

        //            if (faces.Any())
        //            {
        //                ViewBag.Message = "Face detected!";
        //            }
        //            else
        //            {
        //                ViewBag.Message = "No face detected. Maybe it's a room?";
        //            }
        //        }
        //    }

        //    return View("Index");
        //}
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