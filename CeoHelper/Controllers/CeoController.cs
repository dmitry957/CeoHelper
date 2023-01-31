using CeoHelper.Services.Services.Interfaces;
using CeoHelper.Shared.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CeoHelper.Controllers
{
    [Authorize]
    public class CeoController : Controller
    {
        private readonly ICeoService _ceoService;
        private readonly IRequestService _requestService;
        private readonly IUserService _userService;

        public CeoController(ICeoService ceoService,
            IRequestService requestService,
            IUserService userService)
        {
            _ceoService = ceoService;
            _requestService = requestService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            //OpenAIAPI api = new OpenAIAPI(_configuration.GetValue<string>("OPENAI_KEY"), Engine.Davinci);
            // var result = await api.Completions.CreateCompletionAsync(new CompletionRequest("Write article about blockchain", temperature: 0, max_tokens:200));
            //ViewBag.Result = await _ceoService.ExecuteOpenAiRequest("write small article about bitcoin");
            ViewBag.Tokens = await _userService.GetCurrentUserAvailableTokens();   
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] SearchRequestModel model)
        {
            //OpenAIAPI api = new OpenAIAPI(_configuration.GetValue<string>("OPENAI_KEY"), Engine.Davinci);
            // var result = await api.Completions.CreateCompletionAsync(new CompletionRequest("Write article about blockchain", temperature: 0, max_tokens:200));
            SearchResultModel result = await _ceoService.ExecuteOpenAiRequest(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Like([FromQuery] long id)
        {
            await _requestService.Like(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Dislike([FromQuery] long id)
        {
            await _requestService.Dislike(id);
            return Ok();
        }
    }
}
