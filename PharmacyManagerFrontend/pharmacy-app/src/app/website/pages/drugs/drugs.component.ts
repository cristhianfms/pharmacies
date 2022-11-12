import {Component, Input, OnInit} from '@angular/core';
import {Drug} from "../../../models/drug.model";
import {DrugsService} from "../../../services/drugs.service";
import {StoreService} from "../../../services/store.service";
import {PurchaseItemDto} from "../../../models/Dto/purchase-item-dto.model";
import { DrugQueryDto } from 'src/app/models/Dto/drug-query.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-drugs',
  templateUrl: './drugs.component.html',
  styleUrls: ['./drugs.component.scss']
})
export class DrugsComponent implements OnInit {

  filterDrugName: string | null = null
  filterHasStock: boolean | null = null

  drugQuery: DrugQueryDto = {
    drugName: null,
    hasStock: null
  }

  selectedDrugDetail: Drug | null = null

  @Input() drugs: Drug[] = []
  myShoppingCart : PurchaseItemDto[] = []

  constructor(private drugsService: DrugsService, private storeService: StoreService, private modalService: NgbModal) {
    this.myShoppingCart = this.storeService.getShoppingCart();
  }

  ngOnInit(): void {
    this.drugsService.getAllDrugs().subscribe({
          next: this.handleGetAllResponse.bind(this)
        }
    )
  }

  updateDrugQuery(event: Event) {
    event.preventDefault();
    this.drugQuery = {
      drugName: this.filterDrugName,
      hasStock: this.filterHasStock
    }
  }

  handleGetAllResponse(data: any){
    this.drugs = data
  }

  onAddToShoppingCart(purchaseDrug: PurchaseItemDto) {
    this.storeService.addDrug(purchaseDrug)
  }

  open(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' })
  }

  handleOnDrugDetail(drug: Drug) {
    this.selectedDrugDetail = drug
  }

  handleCloseDrugDetail() {
    this.selectedDrugDetail = null
  }
}
