import { Component, OnInit } from '@angular/core';
import { Form, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { Drug } from 'src/app/models/drug.model';
import { SolicitudeItemDto } from 'src/app/models/Dto/solicitude-item-dto.model';
import { CreateSolicitudeDto, Solicitude, SolicitudeItem } from 'src/app/models/solicitude.model';
import { DrugsService } from 'src/app/services/drugs.service';
import { SolicitudesService } from 'src/app/services/solicitudes.service';

@Component({
  selector: 'app-solicitudes-create',
  templateUrl: './solicitudes-create.component.html',
  styleUrls: ['./solicitudes-create.component.scss']
})
export class SolicitudesCreateComponent implements OnInit {
  pressed: boolean = false;
  drugs : Drug[] =  [];
  myItems : SolicitudeItemDto [] = [];
  mySolicitude = new BehaviorSubject<SolicitudeItemDto[]>([])
  mySolicitude$ = this.mySolicitude.asObservable();
  form!:FormGroup;

  
  newSolicitude : CreateSolicitudeDto = {
    solicitudeItems: [],
  }

  createdSolicitude: Solicitude | null = null;
  
  
  createStatus: 'loading' | 'success' | 'error' | null = null
  errorMessage: string = ''
  
  constructor(private router: Router,  private drugService: DrugsService, private solicitudeService: SolicitudesService) {
    this.myItems = this.myItems
   }
  
  ngOnInit(): void {
    this.drugService.getAllDrugs().subscribe({
      next: this.handleGetAllResponse.bind(this),
      error: this.handleError.bind(this)
    })    
    const group: any = {};
    this.drugs.forEach(drug=> {
      group[drug.drugCode] = new FormControl('')
    })
    this.form =  new FormGroup(group);
  }

  addItem(drug: Drug){
    var inputs = this.form.getRawValue();
    var quantity = inputs[drug.drugCode];
   
    if (quantity > 0){
     let item: SolicitudeItemDto = {
        drugCode: drug.drugCode,
        drugQuantity: quantity
      }      
      this.myItems.push(item);
    }
    else{
      alert('No se puede agregar, cant<0');
    }
    if (this.myItems.length>0){
      this.form.valid;
    }
    this.mySolicitude.next(this.myItems);
  }

  onSubmitCreateSolicitude(){
    this.pressed = true;
    this.createStatus = 'loading';
   const solicitude: CreateSolicitudeDto ={
      solicitudeItems: [...this.myItems]
    }
  
    this.solicitudeService.create(solicitude)
    .subscribe({
      next: this.handleOkResponse.bind(this),
      error: this.handleErrorResponse.bind(this)
      
    })
  }

  handleOkResponse(data: any){
    this.createdSolicitude = data;
    this.createStatus = 'success';
    this.router.navigate(['/employee/solicitudes']);
    
  }
  
  handleErrorResponse(error:any){
    this.createStatus = 'error';
    this.errorMessage = error.error.message;
  }

  handleGetAllResponse(data: any){
    this.drugs = data;
    const group: any = {};
    this.drugs.forEach(drug=> {
      group[drug.drugCode] = new FormControl('null')
    })
    this.form =  new FormGroup(group);
  }

  handleError(error: any){
    window.alert("There is not drugs in this pharmacy")
  }
}
