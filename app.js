import "https://unpkg.com/n3/browser/n3.min.js"
import { Graph } from "./Graph.js"

let g
window.addEventListener("load", onLoad)

async function onLoad() {
	const response = await fetch("./data.ttl")
	const text = await response.text()
	const parser = new N3.Parser
	const store = new N3.Store
	const quads = parser.parse(text)
	store.addQuads(quads)
	g = new Graph(store)

	for (const requirement of g.requirements) {
		const li = document.getElementById("requirements").appendChild(document.createElement("li"))
		const a = li.appendChild(document.createElement("a"))
		const descriptionDiv = li.appendChild(document.createElement("div"))
		const commentDiv = li.appendChild(document.createElement("div"))

		a.innerText = `${requirement.number} - ${requirement.title} (${requirement.useCases.length})`
		a.id = `requirement-${requirement.number}`
		a.href = `#requirement-${requirement.number}`
		li.dataset.id = requirement.id
		commentDiv.className = "comment"
		descriptionDiv.innerText = requirement.description
		descriptionDiv.className = "description"
	}

	for (const useCase of g.useCases) {
		const li = document.getElementById("useCases").appendChild(document.createElement("li"))
		const a = li.appendChild(document.createElement("a"))
		const descriptionDiv = li.appendChild(document.createElement("div"))
		const commentDiv = li.appendChild(document.createElement("div"))

		a.innerText = `${useCase.number} - ${useCase.title} (${useCase.requirements.length})`
		a.id = `usecase-${useCase.number}`
		a.href = `#usecase-${useCase.number}`
		li.dataset.id = useCase.id
		commentDiv.className = "comment"
		descriptionDiv.innerText = useCase.description
		descriptionDiv.className = "description"
	}


	document.addEventListener("keydown", e => {
		if (e.key === "Escape") {
			clear()
		}
	})

	window.addEventListener("hashchange", e => {
		onHashChange(new URL(e.newURL).hash.split("#")[1])
	})

	if (location.hash) {
		onHashChange(location.hash.split("#")[1])
	}
}

function clear() {
	for (const element of document.querySelectorAll(".clicked")) {
		element.classList.remove("clicked")
	}
	for (const element of document.querySelectorAll(".filtered")) {
		element.classList.remove("filtered")
	}
	for (const element of document.querySelectorAll(".comment")) {
		element.innerText = ""
	}
}

function onRequirementClick() {
	clear()
	this.classList.add("clicked")
	for (const useCaseElement of document.querySelectorAll("#useCases li")) {
		for (const link of g.requirements
			.filter(x => x.id === this.dataset.id)
			.flatMap(x => x.useCases)
			.filter(x => x.target.id === useCaseElement.dataset.id)) {

			useCaseElement.classList.add("filtered")
			useCaseElement.querySelector(".comment").innerText = link.comment
		}
	}
}

function onUseCaseClick() {
	clear()
	this.classList.add("clicked")
	for (const requirementElement of document.querySelectorAll("#requirements li")) {
		for (const link of g.useCases
			.filter(x => x.id === this.dataset.id)
			.flatMap(x => x.requirements)
			.filter(x => x.requirement.id === requirementElement.dataset.id)) {

			requirementElement.classList.add("filtered")
			requirementElement.querySelector(".comment").innerText = link.comment
		}
	}
}

function onHashChange(hash) {
	const target = document.getElementById(hash).parentElement

	if (hash.startsWith("requirement")) {
		onRequirementClick.call(target)
	} else {
		onUseCaseClick.call(target)
	}

	target.scrollIntoView()
}
