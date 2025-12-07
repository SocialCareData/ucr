import { Vocabulary } from "./Vocabulary.js"
import { UseCaseLink } from "./UseCaseLink.js"

export class UseCase {
	constructor(node, graph) {
		this.node = node
		this.graph = graph
	}

	get id() {
		return this.node.id
	}

	get number() {
		return this.id.split("/").pop()
	}

	get title() {
		return [...this.graph.match(this.node, Vocabulary.useCaseTitle)]
			.map(x => x.object)
			.map(x => x.value)
		[0]
	}

	get description() {
		return [...this.graph.match(this.node, Vocabulary.useCaseDescription)]
			.map(x => x.object)
			.map(x => x.value)
		[0]
	}

	get requirements() {
		return [...this.graph.match(undefined, Vocabulary.target, this.node)]
			.map(x => x.subject)
			.map(x => new UseCaseLink(x, this.graph))
	}
}
