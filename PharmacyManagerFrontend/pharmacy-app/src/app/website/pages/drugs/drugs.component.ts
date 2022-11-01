import {Component, Input, OnInit} from '@angular/core';
import {Drug} from "../../../models/drug.model";
import {DrugsService} from "../../../services/drugs.service";
import {StoreService} from "../../../services/store.service";
import {PurchaseDrugDto} from "../../../models/Dto/purchase-drug-dto.model";

@Component({
  selector: 'app-drugs',
  templateUrl: './drugs.component.html',
  styleUrls: ['./drugs.component.scss']
})
export class DrugsComponent implements OnInit {

  @Input() drugs: Drug[] = []
  myShoppingCart : PurchaseDrugDto[] = []

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

  handleGetAllResponse(data: any){
    this.drugs = data
  }

  handleError(error: any){
    window.alert("Error getting pharmacies")
  }

  onAddToShoppingCart(purchaseDrug: PurchaseDrugDto) {
    this.storeService.addDrug(purchaseDrug)
  }
}
