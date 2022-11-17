import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Drug} from "../../../models/drug.model";

@Component({
  selector: 'app-drug-detail',
  templateUrl: './drug-detail.component.html',
  styleUrls: ['./drug-detail.component.scss']
})
export class DrugDetailComponent implements OnInit {

  @Input() drug: Drug | null = null;
  @Output() close = new EventEmitter<void>();

  constructor() { }

  ngOnInit(): void {
  }

  onClose() {
    this.close.emit();
  }
}
