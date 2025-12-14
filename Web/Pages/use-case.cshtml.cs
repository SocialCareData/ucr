using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace Web.Pages;

public class UseCaseModel : PageModel
{
    public required UseCase UseCase { get; set; }

    public void OnGet(int id)
    {
        var g = UcrGraph.Wrap(new Graph());
		FileLoader.Load(g, "./wwwroot/data/all.ttl"); // TODO: Extract

		UseCase = g.UseCases.Single(x => x.Number == id.ToString());
    }
}
