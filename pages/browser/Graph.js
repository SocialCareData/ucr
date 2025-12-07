import { Vocabulary } from "./Vocabulary.js"
import { Requirement } from "./Requirement.js"
import { UseCase } from "./UseCase.js"
import { UseCaseLink } from "./UseCaseLink.js"

export class Graph {
	constructor(graph) {
		this.graph = graph
	}

	get requirements() {
		return [...this.graph.match(undefined, Vocabulary.title)]
			.map(x => x.subject)
			.map(x => new Requirement(x, this.graph))
	}

	get requirementsDescending() {
		return this.requirements
			.sort((a, b) => b.useCases.length - a.useCases.length)
	}

	get useCases() {
		return [...this.graph.match(undefined, Vocabulary.useCaseTitle)]
			.map(x => x.subject)
			.map(x => new UseCase(x, this.graph))
	}

	get useCasesDescending() {
		return this.useCases
			.sort((a, b) => b.requirements.length - a.requirements.length)
	}

	get links() {
		return [...this.graph.match(undefined, Vocabulary.useCase)]
			.map(x => x.object)
			.map(x => new UseCaseLink(x, this.graph))
	}
}
