using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Jobs;
namespace ApiHangFire.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DataController : ControllerBase
{

    private readonly IConfiguration _config;

    public DataController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    [Route("test")]
    public String TestJob()
    {
        Job jobs = new Job();

        var externalUrls = _config.GetSection("Urls");

        var getExternalUrl = externalUrls.GetSection("GetDataUrl").Value;
        var setExternalUrl = externalUrls.GetSection("SetDataUrl").Value;

        var authToken = _config.GetSection("AuthToken").Value;

        //Recurring Job - execute many times
        RecurringJob.AddOrUpdate(() =>

            //call job inside dll
            jobs.JobTest(getExternalUrl, setExternalUrl, authToken).Wait()

        , Cron.Hourly);

        return "## Job finalized! ##";
    }
}
