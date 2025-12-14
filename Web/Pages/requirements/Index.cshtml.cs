using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace WebApplication1.Pages.requirements;

public class IndexModel : PageModel
{
    public IEnumerable<Requirement> Requirements { get; private set; }

    public void OnGet()
    {
        var g = UcrGraph.Wrap(new Graph());
		FileLoader.Load(g, "./wwwroot/data/all.ttl"); // TODO: Extract

		Requirements = g.Requirements;
    }
}
