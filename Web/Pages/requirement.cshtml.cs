using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace Web.Pages;

public class RequirementModel : PageModel
{
    public required Requirement Requirement { get; set; }

    public void OnGet(int id) =>
        Requirement = UcrGraph.Instance.Requirements.Single(x => x.Number == id.ToString());
}
