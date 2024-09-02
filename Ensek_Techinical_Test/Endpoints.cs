using Ensek_Technical_Test.Application.DTO;
using Ensek_Technical_Test.Application.Services;

namespace Ensek_Techinical_Test
{
    public static class Endpoints
    {
        public static void RegisterEndpoints(this WebApplication app)
        {
            app.MapGet("/", async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync("wwwroot/index.html");
            });
            app.MapPost("/meter-reading-uploads", UploadMeterReading);
        }

        private static async Task<IResult> UploadMeterReading(HttpContext httpContext, MeterReadingService service)
        {
            var form = await httpContext.Request.ReadFormAsync();
            var file = form.Files["file"];
            if (file == null || file.Length == 0)
            {
                return Results.BadRequest("No file was uploaded.");
            }

            var meterReadings = new List<MeterReadingDto>();
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                string line;
                stream.ReadLine();//read off header
                while ((line = stream.ReadLine()) != null)
                {
                    var columns = line.Split(',');

                    if (int.TryParse(columns[0], out var accountId) &&
                        DateTime.TryParse(columns[1], out var meterReadingDateTime) &&
                        !string.IsNullOrWhiteSpace(columns[2]))
                    {
                        meterReadings.Add(new MeterReadingDto
                        {
                            AccountId = accountId,
                            MeterReadingDateTime = meterReadingDateTime,
                            MeterReadValue = columns[2]
                        });
                    }
                }
            }

            var (successful, failed) = await service.ProcessMeterReadingsAsync(meterReadings);
            return Results.Ok(new { Successful = successful, Failed = failed });
        }
    }
}
