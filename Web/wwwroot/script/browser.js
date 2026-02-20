import "https://unpkg.com/n3/browser/n3.min.js"
import "./RequirementList.js"
import "./UseCaseList.js"
import { Graph } from "./Graph.js"

await Promise.all([
	loadData(),
	new Promise(resolve => addEventListener("usecases-loaded", resolve)),
	new Promise(resolve => addEventListener("requirements-loaded", resolve)),
])

onLoad()

async function loadData() {
	const response = await fetch("../data/all.ttl") // TODO: Extract
	const text = await response.text()
	const store = new N3.Store
	store.addQuads(new N3.Parser().parse(text))
	const g = new Graph(store)

	document.dispatchEvent(new CustomEvent("data-ready", { detail: { data: g } }))
}

async function onLoad() {
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
	let first = true
	for (const useCaseElement of document.querySelectorAll("#useCases > ul > li")) {
		for (const link of this.requirement.useCases
			.filter(x => x.target.id === useCaseElement.useCase.id)) {

			useCaseElement.classList.add("filtered")
			useCaseElement.querySelector(".comment").innerText = link.comment

			if (first) {
				first = false
				useCaseElement.scrollIntoView()
			}
		}
	}
}

function onUseCaseClick() {
	clear()
	this.classList.add("clicked")
	let first = true
	for (const requirementElement of document.querySelectorAll("#requirements > ul > li")) {
		for (const link of this.useCase.requirements
			.filter(x => x.requirement.id === requirementElement.requirement.id)) {

			requirementElement.classList.add("filtered")
			requirementElement.querySelector(".comment").innerText = link.comment

			if (first) {
				first = false
				requirementElement.scrollIntoView()
			}
		}
	}
}

function onHashChange(hash) {
	if (hash == undefined) {
		clear()
	} else {
		const target = document.getElementById(hash).parentElement

		if (hash.includes("requirement")) {
			onRequirementClick.call(target)
		} else {
			onUseCaseClick.call(target)
		}

		target.scrollIntoView()
	}
}
