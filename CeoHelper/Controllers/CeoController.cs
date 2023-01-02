using CeoHelper.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CeoHelper.Controllers
{
    [Authorize]
    public class CeoController : Controller
    {
        private readonly ICeoService _ceoService;

        public CeoController(ICeoService ceoService)
        {
            _ceoService = ceoService;
        }
         public async Task<IActionResult> Index()
        {
            //OpenAIAPI api = new OpenAIAPI(_configuration.GetValue<string>("OPENAI_KEY"), Engine.Davinci);
            // var result = await api.Completions.CreateCompletionAsync(new CompletionRequest("Write article about blockchain", temperature: 0, max_tokens:200));
            //ViewBag.Result = await _ceoService.ExecuteOpenAiRequest("write small article about bitcoin");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromQuery]string text)
        {
            //OpenAIAPI api = new OpenAIAPI(_configuration.GetValue<string>("OPENAI_KEY"), Engine.Davinci);
            // var result = await api.Completions.CreateCompletionAsync(new CompletionRequest("Write article about blockchain", temperature: 0, max_tokens:200));
            var result = await _ceoService.ExecuteOpenAiRequest(text, 50);
            return Json(result);
        }
    }
}
