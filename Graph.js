import { Vocabulary } from "./Vocabulary.js"
import { Requirement } from "./Requirement.js"
import { UseCase } from "./UseCase.js"

export class Graph {
	constructor(graph) {
		this.graph = graph
	}

	get requirements() {
		return [...this.graph.match(undefined, Vocabulary.title)]
			.map(x => x.subject)
			.map(x => new Requirement(x, this.graph))
			.sort((a, b) => b.useCases.length - a.useCases.length)
	}

	get useCases() {
		return [...this.graph.match(undefined, Vocabulary.useCaseTitle)]
			.map(x => x.subject)
			.map(x => new UseCase(x, this.graph))
			.sort((a, b) => b.requirements.length - a.requirements.length)
	}
}
