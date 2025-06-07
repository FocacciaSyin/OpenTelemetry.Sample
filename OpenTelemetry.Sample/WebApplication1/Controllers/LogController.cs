using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class LogController(ILogger<LogController> logger) : ControllerBase
{
    [HttpGet(Name = "DeafaultOK")]
    public IActionResult DeafaultOK(int id, int pageSize, CancellationToken ct)
    {
        logger.LogInformation("== DeafaultOK Started ==");

        //這些都是不好的方法
        logger.LogInformation($" Input id: {id}");
        logger.LogInformation(" Input id: " + id);
        logger.LogInformation(" Input id: {0}", id);

        logger.LogInformation(string.Format(" Input id: {0}", id));
        //注意
        //建議使用 Template 方式撰寫 Log
        //來源：https://www.youtube.com/watch?v=d1ODcHi5AI4
        logger.LogInformation(" Input id: {@id}", id);
        logger.LogInformation(" Input id: {pageSize:int}", pageSize);

        logger.LogInformation("== DeafaultOK End ==");


        return Ok("回傳結果");
    }
}
