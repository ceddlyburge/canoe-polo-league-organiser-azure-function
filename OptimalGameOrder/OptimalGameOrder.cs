using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CanoePoloLeagueOrganiser;
using System.Collections.Generic;

namespace OptimalGameOrder
{
    public static class OptimalGameOrder
    {
        [FunctionName("OptimalGameOrder")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var body = await new StreamReader(req.Body).ReadToEndAsync();

            var games = JsonConvert.DeserializeObject<List<Game>>(body);

            //var games = new List<Game> {
            //    new Game("Clapham", "Surrey"),
            //    new Game("Clapham", "ULU"),
            //    new Game("Clapham", "Meridian"),
            //    new Game("Blackwater", "Clapham"),
            //    new Game("ULU", "Blackwater"),
            //    new Game("Surrey", "Castle"),
            //    new Game("ULU", "Meridian"),
            //    new Game("Letchworth", "ULU"),
            //    new Game("Castle", "Blackwater"),
            //    new Game("Surrey", "Letchworth"),
            //    new Game("Meridian", "Castle"),
            //    new Game("Blackwater", "Letchworth"),
            //    new Game("Meridian", "Surrey"),
            //    new Game("Castle", "Letchworth")
            // };

            var gameOrder = new CanoePoloLeagueOrganiser.OptimalGameOrder(new TenSecondPragmatiser()).OptimiseGameOrder(games);

            return new OkObjectResult(gameOrder);
        }
    }
}
