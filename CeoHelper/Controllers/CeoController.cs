using CeoHelper.Services.Services.Interfaces;
using CeoHelper.Shared.Models.Ceo;
using CeoHelper.Shared.Models.Request;
using CeoHelper.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CeoHelper.Controllers
{
    [Authorize, AuthenticatedConstraint]
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

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            //OpenAIAPI api = new OpenAIAPI(_configuration.GetValue<string>("OPENAI_KEY"), Engine.Davinci);
            // var result = await api.Completions.CreateCompletionAsync(new CompletionRequest("Write article about blockchain", temperature: 0, max_tokens:200));
            //ViewBag.Result = await _ceoService.ExecuteOpenAiRequest("write small article about bitcoin");
            ViewBag.Tokens = await _userService.GetCurrentUserAvailableTokens();
            IndexViewModel model = new();
            string cookieName = "NotFirstTime";
            if (Request.Cookies.Keys.Contains(cookieName))
            {
                model.IsFirstTime = false;
                return View(model);
            }
            else
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                cookieOptions.Path = "/";
                Response.Cookies.Append(cookieName, HttpContext.User.Identity.Name, cookieOptions);
                model.IsFirstTime = true;
                return View(model);
            }
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
