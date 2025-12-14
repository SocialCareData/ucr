using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace Web.Pages;

public class RequirementCategoriesModel : PageModel
{
    public required IEnumerable<RequirementCategory> RequirementCategories { get; set; }

    public void OnGet()
    {
        var g = UcrGraph.Wrap(new Graph());
		FileLoader.Load(g, "./wwwroot/data/all.ttl"); // TODO: Extract

		RequirementCategories = g.RequirementCategories;
    }
}
