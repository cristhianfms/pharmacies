import {DrugExporterProp} from "./drug-exporter-prop.model";

export interface DrugExporter {
    name: string,
    props: DrugExporterProp[]
}
