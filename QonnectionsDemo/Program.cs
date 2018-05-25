using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qlik.Engine;
using Qlik.Engine.Communication;
using Qlik.Sense.Client.Visualizations;

namespace QonnectionsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri("http://localhost:4848");
            var location = Location.FromUri(uri);
            location.AsDirectConnectionToPersonalEdition();
            location.IsVersionCheckActive = false;

            var appId = location.AppWithNameOrDefault("QonnectionsDemo");
            using (var hub = location.Hub(Session.WithApp(appId)))
            using (new DebugConsole())
            {
                Console.WriteLine(hub.ProductVersion());

                var app = hub.OpenApp(appId.AppId);
                app.Evaluate("Sum(Sales)");

                var barChart = app.GetObject<Barchart>("ajZM");
                barChart.GetHyperCubeData("/qHyperCubeDef", new[] { new NxPage { Width = 3, Height = 20 } });

                barChart.SelectHyperCubeCells("/qHyperCubeDef", new[] { 0 }, new[] { 0 });
            }
        }
    }
}
