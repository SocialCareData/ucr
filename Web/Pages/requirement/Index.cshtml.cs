using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace WebApplication1.Pages.requirement;

public class IndexModel : PageModel
{
    public Requirement Requirement { get; private set; }

    public void OnGet(int id)
    {
        var g = UcrGraph.Wrap(new Graph());
		FileLoader.Load(g, "./wwwroot/data/all.ttl"); // TODO: Extract

		Requirement = g.Requirements.Single(x => x.Number == id.ToString());
    }
}
