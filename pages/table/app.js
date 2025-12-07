import "https://unpkg.com/n3/browser/n3.min.js"
import { Graph } from "../browser/Graph.js"

const g = await loadData()

onLoad()

async function loadData() {
	const response = await fetch("../data.ttl")
	const text = await response.text()
	const store = new N3.Store
	store.addQuads(new N3.Parser().parse(text))
	return new Graph(store)
}

async function onLoad() {
	const table = document.querySelector("table")

	const colgroup = table.appendChild(document.createElement("colgroup"))
	colgroup.appendChild(document.createElement("col"))
	colgroup.appendChild(document.createElement("col"))
	for (const useCase of g.useCases) {
		table.appendChild(document.createElement("col")).classList.add("useCaseColumn")
	}

	const useCasesRow = table.tHead.insertRow()

	const cornerCell = useCasesRow.insertCell()
	cornerCell.colSpan = 2
	cornerCell.rowSpan = 2

	const useCasesHeaderCell = useCasesRow.insertCell()
	useCasesHeaderCell.colSpan = g.useCases.length
	useCasesHeaderCell.innerText = "use cases"
	useCasesHeaderCell.classList.add("useCases")

	const useCaseRow = table.tHead.insertRow()

	for (const useCase of g.useCases) {
		const useCaseHeaderCell = useCaseRow.insertCell()
		const useCaseA = useCaseHeaderCell.appendChild(document.createElement("a"))
		useCaseA.href = `../browser#${useCase.id}`
		useCaseA.innerText = `${useCase.number} - ${useCase.title}`
	}

	let isFirstRequirementRow = true
	for (const requirement of g.requirements) {
		const requirementRow = table.tBodies[0].insertRow()

		if (isFirstRequirementRow) {
			const requirementsHeaderCell = requirementRow.insertCell()
			requirementsHeaderCell.rowSpan = g.requirements.length
			requirementsHeaderCell.classList.add("requirements")
			const qwe = document.createElement("span")
			qwe.innerText = "requirements"
			requirementsHeaderCell.appendChild(qwe)

			isFirstRequirementRow = false
		}

		const requirementHeaderCell = requirementRow.insertCell()
		const requirementA = requirementHeaderCell.appendChild(document.createElement("a"))
		requirementA.href = `../browser#${requirement.id}`
		requirementA.innerText = `${requirement.number} - ${requirement.title}`

		for (const useCase of g.useCases) {
			const linkCell = requirementRow.insertCell()
			if (g.links.some(l => l.requirement.number === requirement.number && l.target.number === useCase.number)) {
				linkCell.innerText = "✔"
			}
		}
	}
}
