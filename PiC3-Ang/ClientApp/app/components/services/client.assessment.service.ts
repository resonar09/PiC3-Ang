

import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import 'rxjs/add/operator/map';

@Injectable()
export class ClientAssessmentService {
    constructor(private http: Http) {

    }
    getClientAssessments(status: string) {
        return this.http.get('/api/assessmentdata/GetAssessmentsByStatus/' + status).map(res => res.json());
    }
}