using ExpeditionHack.Models.ViewModel;
using Extreme.DataAnalysis;
using Extreme.Statistics;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpeditionHack.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            LanguageVm eng = new LanguageVm
            {
                Home = "Home",
                Graph = "Graphs",
                Chart = "Charts",
                About = "About",
                Contact = "Contact",
                MapSrc = "https://ryoeun0.carto.com/builder/3237491e-11e9-11e7-89c3-0e05a8b3e3d7/embed"
            };

            //    public string Home { get; set; }
            //public string Graph { get; set; }
            //public string Chart { get; set; }
            //public string About { get; set; }
            //public string Contact { get; set; }
            //public string MapSrc { get; set; }

            return View(eng);
        }

        public ActionResult Francais()
        {
            LanguageVm fr = new LanguageVm
            {
                Home = "Acceuil",
                Graph = "Graphiques",
                Chart = "Diagrammes",
                About = "À propos",
                Contact = "Contact",
                MapSrc = "https://ryoeun0.carto.com/builder/4ce9f9cc-245d-4d16-95b4-f9ec3a0f9522/embed"
            };

            return View("Index", fr);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Prediction()
        {
            LanguageVm eng = new LanguageVm
            {
                Home = "Home",
                Graph = "Graphs",
                Chart = "Charts",
                About = "About",
                Contact = "Contact",
                MapSrc = "https://ryoeun0.carto.com/builder/3237491e-11e9-11e7-89c3-0e05a8b3e3d7/embed"
            };

            return View(eng);
        }

        public ActionResult PredictionCalculate(CropYieldRequest mvcModel)
        {
            // Multiple linear regression can be performed using 
            // the LinearRegressionModel class.
            //
            // 

            // This QuickStart sample uses data test scores of 200 high school
            // students, including science, math, and reading.

            // First, read the data from a file into a data frame. 
            var data = DataFrame.ReadCsv(@"C:\SF.Code\Extreme\Extreme\test3.csv");

            //// Now create the regression model. Parameters are the data frame,
            //// the name of the dependent variable, and a string array containing 
            //// the names of the independent variables.
            //var model = new LinearRegressionModel(data, "science", new string[] {"math", "female", "socst", "read"});

            // Alternatively, we can use a formula to describe the variables
            // in the model. The dependent variable goes on the left, the
            // independent variables on the right of the ~:
            //var model2 = new LinearRegressionModel(data,
            //      "science ~ math + female + socst + read");
            var model = new LinearRegressionModel(data, "Yield ~ Temperature + NDVI + Rainfall");

            // We can set model options now, such as whether to exclude 
            // the constant term:
            // model.NoIntercept = false;

            // The Compute method performs the actual regression analysis.
            model.Compute();

            // The Parameters collection contains information about the regression 
            // parameters.
            //Console.WriteLine("Variable\t           Value         Std.Error       t-stat  p-Value");
            double[] ValuesToSend = new double[10];
            int count = 0;
            foreach (Parameter parameter in model.Parameters)
            {
                // Parameter objects have the following properties:
                Console.WriteLine("{0,-20}\t {1,10:F2}\t {2,10:F6} {3,8:F2}\t {4,7:F5}",
                    // Name, usually the name of the variable:
                    parameter.Name,
                    // Estimated value of the parameter:
                    parameter.Value,
                    // Standard error:
                    parameter.StandardError,
                    // The value of the t statistic for the hypothesis that the parameter
                    // is zero.
                    parameter.Statistic,
                    // Probability corresponding to the t statistic.
                    parameter.PValue);
                ValuesToSend[count] = parameter.Value; count++;
            }
            //Console.WriteLine();

            // In addition to these properties, Parameter objects have 
            // a GetConfidenceInterval method that returns 
            // a confidence interval at a specified confidence level.
            // Notice that individual parameters can be accessed 
            // using their numeric index. Parameter 0 is the intercept, 
            // if it was included.
            //Interval confidenceInterval = model.Parameters[0].GetConfidenceInterval(0.95);
            //Console.WriteLine(" for intercept: {0:F4} - {1:F4}",
            //    confidenceInterval.LowerBound, confidenceInterval.UpperBound);

            // Parameters can also be accessed by name:
            //confidenceInterval = model.Parameters.Get("NDVI").GetConfidenceInterval(0.95);
            //Console.WriteLine("95% confidence interval for 'NDVI': {0:F4} - {1:F4}",
            //    confidenceInterval.LowerBound, confidenceInterval.UpperBound);
            //Console.WriteLine();

            // There is also a wealth of information about the analysis available
            // through various properties of the LinearRegressionModel object:
            //Console.WriteLine("Residual standard error: {0:F3}", model.StandardError);
            //Console.WriteLine("R-Squared:\t          {0:F4}", model.RSquared);
            //Console.WriteLine("Adjusted R-Squared:\t  {0:F4}", model.AdjustedRSquared);
            //Console.WriteLine("F-statistic:             {0:F4}", model.FStatistic);
            //Console.WriteLine("Corresponding p-value:   {0:F5}", model.PValue);
            //Console.WriteLine();

            // Much of this data can be summarized in the form of an ANOVA table:
            //Console.WriteLine(model.AnovaTable.ToString());

            // All this information can be printed using the Summarize method.
            // You will also see summaries using the library in C# interactive.
            //Console.WriteLine(model.Summarize());

            LanguageVm eng = new LanguageVm
            {
                Home = "Home",
                Graph = "Graphs",
                Chart = "Charts",
                About = "About",
                Contact = "Contact",
                MapSrc = "https://ryoeun0.carto.com/builder/3237491e-11e9-11e7-89c3-0e05a8b3e3d7/embed"
            };


            ViewBag.CropYield = (ValuesToSend[0] +
                ValuesToSend[1] * mvcModel.Temperature +
                ValuesToSend[2] * mvcModel.NDVI +
                ValuesToSend[3] * mvcModel.Rainfall).ToString("f2");
            ViewBag.RSquared = model.RSquared.ToString("f4");

            return View("Prediction", eng);
        }
    }
}
