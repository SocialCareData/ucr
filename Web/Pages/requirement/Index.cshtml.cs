using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace Web.Pages.requirement;

public class IndexModel : PageModel
{
    public required Requirement Requirement { get; set; }

    public void OnGet(int id)
    {
        var g = UcrGraph.Wrap(new Graph());
		FileLoader.Load(g, "./wwwroot/data/all.ttl"); // TODO: Extract

		Requirement = g.Requirements.Single(x => x.Number == id.ToString());
    }
}
