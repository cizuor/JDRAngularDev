import { Subject } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Inject } from "@angular/core";
import { Classe } from "../models/classe.model";

export class ClasseService {
  baseUrl: string

  classeSubject = new Subject<any[]>();
  private allclasse = [];


  classeSelectSubject = new Subject<any[]>();
  private classeSelect = [];

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }


  emitClasseSubject() {
    this.classeSubject.next(this.allclasse.slice());
  }

  emitSelectedClasseSubject() {
    this.classeSelectSubject.next(this.classeSelect);
  }


  getAllClasse() {
    this.httpClient.get<any[]>('api/classe/').subscribe(result => {
      console.log(result);
      this.allclasse = result;
      this.emitClasseSubject();
    }, error => console.error(error))
  }

  getClasse(id: number) {
    this.httpClient.get<any[]>('api/classe/' + id).subscribe(result => {
      console.log(result);
      this.classeSelect = result;
      this.emitSelectedClasseSubject();
    }, error => console.error(error))
  }

  getNewClasse() {
    this.httpClient.get<any[]>('api/classe/GenerateNewClasse/').subscribe(result => {
      console.log(result);
      this.classeSelect = result;
      this.emitSelectedClasseSubject();
    }, error => console.error(error))
  }

  addClasse(name: string, definition: string) {
    var newClasse = new Classe(0, name, definition)

    newClasse.Stat = this.classeSelect['stat'];

    this.httpClient.post(this.baseUrl + 'api/classe/', newClasse).subscribe();
  }

  majClasse() {
    var newClasse = new Classe(this.classeSelect['id'], this.classeSelect['nom'], this.classeSelect['definition'])
    newClasse.Stat = this.classeSelect['stat'];
    this.httpClient.post(this.baseUrl + 'api/classe/UpdateClasse/', newClasse).subscribe();
  }


}
