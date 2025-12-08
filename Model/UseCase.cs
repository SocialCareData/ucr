using VDS.RDF;
using VDS.RDF.Wrapping;

namespace Model;

public class UseCase : GraphWrapperNode
{
    protected UseCase(INode node, IGraph graph) : base(node, graph) { }

    public static UseCase Wrap(INode node, IGraph graph) => new(node, graph);

    public static UseCase Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

	public string Id { get => (this as IUriNode).Uri.ToString(); }
	public string Number { get => Id.Split("/").Last(); }
	public string Title { get => this.Singular(Vocabulary.UseCaseTitle, ValueMappings.As<string>); }
	public string Description { get => this.Singular(Vocabulary.UseCaseDescription, ValueMappings.As<string>); }

	public ISet<Link> Links { get => this.Subjects(Vocabulary.Target, Link.Wrap, Link.Wrap); }
}
