using Microsoft.AspNetCore.Mvc;

namespace CIMSimulate.Controllers
{
          /// <summary>
          /// 模擬測試
          /// </summary>
          public class TestBoardController : Controller
          {
                    public IActionResult Index()
                    {
                              return View();
                    }
          }
}
