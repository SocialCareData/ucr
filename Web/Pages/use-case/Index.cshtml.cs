using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace WebApplication1.Pages.usecase;

public class IndexModel : PageModel
{
    public UseCase UseCase { get; private set; }

    public void OnGet(int id)
    {
        var g = UcrGraph.Wrap(new Graph());
		FileLoader.Load(g, "./wwwroot/data/all.ttl"); // TODO: Extract

		UseCase = g.UseCases.Single(x => x.Number == id.ToString());
    }
}
