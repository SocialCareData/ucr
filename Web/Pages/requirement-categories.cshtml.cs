using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace Web.Pages;

public class RequirementCategoriesModel : PageModel
{
    public IEnumerable<RequirementCategory> RequirementCategories { get; } =
        UcrGraph.Instance.RequirementCategories;
}
