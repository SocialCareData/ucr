using VDS.RDF;
using VDS.RDF.Wrapping;

namespace Model;

public class CoreStandard : GraphWrapperNode
{
    protected CoreStandard(INode node, IGraph graph) : base(node, graph) { }

    public static CoreStandard Wrap(INode node, IGraph graph) => new(node, graph);

    public static CoreStandard Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

	public string Id { get => (this as IUriNode).Uri.ToString(); }
	public string Number { get => Id.Split("/").Last(); }
	public string Title { get => this.Singular(Vocabulary.CoreStandardTitle, ValueMappings.As<string>); }
	public string Description { get => this.Singular(Vocabulary.CoreStandardDescription, ValueMappings.As<string>); }

	public ISet<Requirement> Requirements { get => this.Subjects(Vocabulary.CoreStandard, Requirement.Wrap, Requirement.Wrap); }
}
