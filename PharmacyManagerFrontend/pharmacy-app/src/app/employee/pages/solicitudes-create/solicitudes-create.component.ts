import {Component, OnInit} from '@angular/core';
import {Form, FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {BehaviorSubject} from 'rxjs';
import {Drug} from 'src/app/models/drug.model';
import {SolicitudeItemDto} from 'src/app/models/Dto/solicitude-item-dto.model';
import {CreateSolicitudeDto, Solicitude, SolicitudeItem} from 'src/app/models/solicitude.model';
import {DrugsService} from 'src/app/services/drugs.service';
import {SolicitudesService} from 'src/app/services/solicitudes.service';

@Component({
    selector: 'app-solicitudes-create',
    templateUrl: './solicitudes-create.component.html',
    styleUrls: ['./solicitudes-create.component.scss']
})
export class SolicitudesCreateComponent implements OnInit {
    drugs: Drug[] = [];
    myItems: SolicitudeItemDto [] = [];
    mySolicitude = new BehaviorSubject<SolicitudeItemDto[]>([])
    mySolicitude$ = this.mySolicitude.asObservable();
    form!: FormGroup;


    newSolicitude: CreateSolicitudeDto = {
        solicitudeItems: [],
    }

    createdSolicitude: Solicitude | null = null;


    status: 'loading' | 'success' | 'error' | null = null
    errorMessage: string = ''

    constructor(private router: Router, private drugService: DrugsService, private solicitudeService: SolicitudesService) {
        this.myItems = this.myItems
    }

    ngOnInit(): void {
        this.drugService.getAllDrugs().subscribe({
            next: this.handleGetAllResponse.bind(this),
            error: this.handleError.bind(this)
        })
        const group: any = {};
        this.drugs.forEach(drug => {
            group[drug.drugCode] = new FormControl('')
        })
        this.form = new FormGroup(group);
    }

    onSubmitCreateSolicitude() {
        this.status = 'loading';
        let items: SolicitudeItem[] = []
        let drugCodes = Object.keys(this.form.getRawValue())
        drugCodes.forEach(code => {
            items.push({
                drugCode: code,
                drugQuantity: +this.form.getRawValue()[code],
            })
        })
        items = items.filter(i => i.drugQuantity > 0);

        if (!this.isFormQuantitiesOK(items)){
            this.status = 'error';
            this.errorMessage = 'Enter at least one solicitude'
            return;
        };

        const solicitude: CreateSolicitudeDto = {
            solicitudeItems: items
        }

        this.solicitudeService.create(solicitude)
            .subscribe({
                next: this.handleOkResponse.bind(this),
                error: this.handleErrorResponse.bind(this)
            })
    }

    handleOkResponse(data: any) {
        this.createdSolicitude = data;
        this.status = 'success';
    }

    handleErrorResponse(error: any) {
        this.status = 'error';
        this.errorMessage = error.error.message;
    }

    handleGetAllResponse(data: any) {
        this.drugs = data;
        const group: any = {};
        this.drugs.forEach(drug => {
            group[drug.drugCode] = new FormControl('null')
        })
        this.form = new FormGroup(group);
    }

    handleError(error: any) {
        window.alert("There is not drugs in this pharmacy")
    }

    private isFormQuantitiesOK(items: SolicitudeItem[]) {
        return items.length > 0 && items.every(i => i.drugQuantity >= 1);
    }

    onChangeInput() {
        this.status = null
        this.errorMessage = ''
    }
}
