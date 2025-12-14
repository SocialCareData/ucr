using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace WebApplication1.Pages.usecases;

public class IndexModel : PageModel
{
    public required IEnumerable<UseCase> UseCases { get; set; }

    public void OnGet()
    {
        var g = UcrGraph.Wrap(new Graph());
		FileLoader.Load(g, "./wwwroot/data/all.ttl"); // TODO: Extract

		UseCases = g.UseCases;
    }
}
