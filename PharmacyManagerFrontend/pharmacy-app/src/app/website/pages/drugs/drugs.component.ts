import {Component, Input, OnInit} from '@angular/core';
import {Drug} from "../../../models/drug.model";
import {DrugsService} from "../../../services/drugs.service";
import {StoreService} from "../../../services/store.service";
import {PurchaseItemDto} from "../../../models/Dto/purchase-item-dto.model";
import { DrugQueryDto } from 'src/app/models/Dto/drug-query.model';

@Component({
  selector: 'app-drugs',
  templateUrl: './drugs.component.html',
  styleUrls: ['./drugs.component.scss']
})
export class DrugsComponent implements OnInit {

  filterDrugName: string | null = null
  filterHasStock: boolean = false

  drugQuery: DrugQueryDto = {
    drugName: null,
    hasStock: false
  }

  @Input() drugs: Drug[] = []
  myShoppingCart : PurchaseItemDto[] = []

  constructor(private drugsService: DrugsService, private storeService: StoreService,) {
    this.myShoppingCart = this.storeService.getShoppingCart();
  }

  ngOnInit(): void {
    this.drugsService.getAllDrugs().subscribe({
          next: this.handleGetAllResponse.bind(this),
          error: this.handleError.bind(this)
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

  handleError(error: any){
    window.alert("Error getting pharmacies")
  }

  onAddToShoppingCart(purchaseDrug: PurchaseItemDto) {
    this.storeService.addDrug(purchaseDrug)
  }
}
