using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace Web.Pages;

public class CoreStandardModel : PageModel
{
    public required CoreStandard CoreStandard { get; set; }

    public void OnGet(int id) =>
		CoreStandard = UcrGraph.Instance.CoreStandards.Single(x => x.Number == id.ToString());
}
