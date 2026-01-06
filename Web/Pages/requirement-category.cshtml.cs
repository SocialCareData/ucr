using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace Web.Pages;

public class RequirementCategoryModel : PageModel
{
    public required RequirementCategory RequirementCategory { get; set; }

    public void OnGet(int id) =>
        RequirementCategory = UcrGraph.Instance.RequirementCategories.Single(x => x.Number == id.ToString());
}
