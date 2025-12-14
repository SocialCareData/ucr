using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace Web.Pages.requirementCategory;

public class IndexModel : PageModel
{
    public required RequirementCategory RequirementCategory { get; set; }

    public void OnGet(int id)
    {
        var g = UcrGraph.Wrap(new Graph());
		FileLoader.Load(g, "./wwwroot/data/all.ttl"); // TODO: Extract

		RequirementCategory = g.RequirementCategories.Single(x => x.Number == id.ToString());
    }
}
