import { Component, OnInit } from '@angular/core';
import {DrugExporter} from "../../../models/drug-exporter.model";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {DrugsExporterService} from "../../../services/drugs-exporter.service";

@Component({
  selector: 'app-export-drug',
  templateUrl: './export-drug.component.html',
  styleUrls: ['./export-drug.component.scss']
})
export class ExportDrugComponent implements OnInit {

  exporters: DrugExporter[] = []

  selectedExporter: DrugExporter | null = null
  form!: FormGroup;
  exportStatus: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''

  constructor(private drugExporterService: DrugsExporterService) { }

  ngOnInit(): void {
    this.drugExporterService.getAllExporters().subscribe({
          next: this.handleGetAllResponse.bind(this)
        }
    )
  }

  handleGetAllResponse(data: any){
    this.exporters = data
  }

  onChange(drugExporter: DrugExporter) {
    const group: any = {};
    this.selectedExporter = drugExporter
    this.selectedExporter?.props.forEach(prop => {
      group[prop.key] = new FormControl(null, Validators.required)
    })
    this.form = new FormGroup(group)
  }

  onSubmit() {
    this.selectedExporter!.props.forEach( prop =>
        prop.value = this.form.getRawValue()[prop.key]
    )

    this.drugExporterService.export(this.selectedExporter!).subscribe({
          next: this.handleExport.bind(this),
          error: this.handleExportError.bind(this)
        }
    )
  }

  isValid(key: string) {
    return this.form.controls[key].valid
  }

  handleExport(data: any){
    this.exportStatus = 'success'
    this.form.reset()
    setTimeout(() => this.exportStatus = null,3000)
  }

  handleExportError(error: any){
    this.exportStatus = 'error';
    this.errorMessage = error.error.message
    setTimeout(() => this.exportStatus = null,3000)

  }
}
