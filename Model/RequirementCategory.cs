using VDS.RDF;
using VDS.RDF.Wrapping;

namespace Model;

public class RequirementCategory : GraphWrapperNode
{
    protected RequirementCategory(INode node, IGraph graph) : base(node, graph) { }

    public static RequirementCategory Wrap(INode node, IGraph graph) => new(node, graph);

    public static RequirementCategory Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

	public string Id { get => (this as IUriNode).Uri.ToString(); }
	public string Number { get => Id.Split("/").Last(); }
	public string Title { get => this.Singular(Vocabulary.CategoryTitle, ValueMappings.As<string>); }
	public string Description { get => this.Singular(Vocabulary.CategoryDescription, ValueMappings.As<string>); }

	public ISet<Requirement> Requirements { get => this.Subjects(Vocabulary.Category, Requirement.Wrap, Requirement.Wrap); }
}
