import { Subject } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Inject } from "@angular/core";
import { Perso } from "../models/perso.model";

export class PersoService {

  persoSubject = new Subject<any[]>();
  private allPerso = [];
  baseUrl: string;


  persoSelectSubject = new Subject<any[]>();
  private persoSelect = [];


  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }


  emitPersoSubject() {
    this.persoSubject.next(this.allPerso.slice());
  }

  emitSelectedPersoSubject() {
    this.persoSelectSubject.next(this.persoSelect);
  }


  getAllPerso(idUser: number) {
    this.httpClient.get<any[]>('api/perso/'+idUser).subscribe(result => {
      console.log(result);
      this.allPerso = result;
      this.emitPersoSubject();
    }, error => console.error(error))
  }

  getPerso(idUser: number, idPerso: number) {
    this.httpClient.get<any[]>('api/perso/' + idUser + '/' + idPerso).subscribe(result => {
      console.log(result);
      this.persoSelect = result;
      this.emitSelectedPersoSubject();
    }, error => console.error(error))
  }

  addPerso() {
    var newPerso = new Perso(0, this.persoSelect['idUser'], this.persoSelect['nom'], this.persoSelect['prenom'], this.persoSelect['definition'], this.persoSelect['vivant'], this.persoSelect['idRace'], this.persoSelect['nomRace'], this.persoSelect['idSousRace'], this.persoSelect['nomSousRace'],
      this.persoSelect['idClasse'], this.persoSelect['nomClasse'], this.persoSelect['lvl'], this.persoSelect['stats'], this.persoSelect['posX'], this.persoSelect['posY'], this.persoSelect['buff']);
    this.httpClient.post(this.baseUrl + 'api/perso/', newPerso).subscribe();

  }
  majPerso() {
    var newPerso = new Perso(0, this.persoSelect['idUser'], this.persoSelect['nom'], this.persoSelect['prenom'], this.persoSelect['definition'], this.persoSelect['vivant'], this.persoSelect['idRace'], this.persoSelect['nomRace'], this.persoSelect['idSousRace'], this.persoSelect['nomSousRace'],
      this.persoSelect['idClasse'], this.persoSelect['nomClasse'], this.persoSelect['lvl'], this.persoSelect['stats'], this.persoSelect['posX'], this.persoSelect['posY'], this.persoSelect['buff']);
    this.httpClient.post(this.baseUrl + 'api/perso/UpdatePerso/', newPerso).subscribe();
  }



  getNewPerso() {
    this.httpClient.get<any[]>('api/perso/GenerateNewPerso/').subscribe(result => {
      console.log(result);
      this.persoSelect = result;
      this.emitSelectedPersoSubject();
    }, error => console.error(error))
  }

}
