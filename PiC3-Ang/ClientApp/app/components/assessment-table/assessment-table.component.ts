import { Component, OnInit, Input } from '@angular/core';
import { Http } from "@angular/http/http";
import { ClientAssessmentService } from "../services/client.assessment.service";

@Component({
  selector: 'app-assessment-table',
  templateUrl: './assessment-table.component.html',
  styleUrls: ['./assessment-table.component.css']
})
export class AssessmentTableComponent implements OnInit {
    @Input() status: string;
    clientAssessments: any[];
    filteredStatus = '';
    isDesc: boolean = false;
    column: string = 'CategoryName';
    direction: number;
    isLoading: boolean = true;
    constructor(private clientAssessmentService: ClientAssessmentService) {
        
    }

    ngOnInit() {


        this.clientAssessmentService.getClientAssessments(this.status).subscribe(clientAssessments => {
            this.clientAssessments = clientAssessments
            this.isLoading = false;
        });
        
        console.log("status", this.status);
    }
    sort(property:string) {
        this.isDesc = !this.isDesc; //change the direction    
        this.column = property;
        this.direction = this.isDesc ? 1 : -1;
    };


}
