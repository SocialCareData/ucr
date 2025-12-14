using VDS.RDF;
using VDS.RDF.Wrapping;

namespace Model;

public class Requirement : GraphWrapperNode
{
    protected Requirement(INode node, IGraph graph) : base(node, graph) { }

	public static Requirement Wrap(INode node, IGraph graph) => new(node, graph);

	public static Requirement Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

	public string Id { get => (this as IUriNode).Uri.ToString(); }
	public string Number { get => Id.Split("/").Last(); }
    public string Title { get => this.Singular(Vocabulary.Title, ValueMappings.As<string>); }
    public string Description { get => this.Singular(Vocabulary.Description, ValueMappings.As<string>); }

    public ISet<Link> Links { get => this.Objects(Vocabulary.UseCase, Link.Wrap, Link.Wrap); }
    public ISet<RequirementCategory> Categories { get => this.Objects(Vocabulary.Category, RequirementCategory.Wrap, RequirementCategory.Wrap); }
}
