import { Subject } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Inject } from "@angular/core";
import { SousRace } from "../models/sousrace.model";

export class SousRaceService {

  baseUrl: string;

  sousRaceSubject = new Subject<any[]>();
  private allSousRace = [];


  sousRaceSelectSubject = new Subject<any[]>();
  private sousRaceSelect = [];


  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }


  emitSousRaceSubject() {
    this.sousRaceSubject.next(this.allSousRace.slice());
  }

  emitSousRaceSelectedSubject() {
    this.sousRaceSelectSubject.next(this.sousRaceSelect);
  }

  getAllSousRace() {
    this.httpClient.get<any[]>('api/sousRace/').subscribe(result => {
      this.allSousRace = result;
      this.emitSousRaceSubject();
    }, error => console.error(error));
  }

  getSousRace(id: number) {
    this.httpClient.get<any[]>('api/sousRace/' + id).subscribe(result => {
      console.log(result);
      this.sousRaceSelect = result;
      this.emitSousRaceSelectedSubject();
    }, error => console.error(error))

  }


  getNewSousRace() {
    this.httpClient.get<any[]>('api/sousRace/GenerateNewSousRace/').subscribe(result => {
      console.log(result);
      this.sousRaceSelect = result;
      this.emitSousRaceSelectedSubject();
    }, error => console.error(error))
  }

  addSousRace() {
    var newSousRace = new SousRace(0, this.sousRaceSelect['nom'], this.sousRaceSelect['definition'], "", this.sousRaceSelect['idRace'])

    newSousRace.Stat = this.sousRaceSelect['stat'];

    this.httpClient.post(this.baseUrl + 'api/sousRace/', newSousRace).subscribe();
  }


  majSousRace() {
    var newSousRace = new SousRace(this.sousRaceSelect['id'], this.sousRaceSelect['nom'], this.sousRaceSelect['definition'], "", this.sousRaceSelect['idRace'])
    newSousRace.Stat = this.sousRaceSelect['stat'];

    this.httpClient.post(this.baseUrl + 'api/sousRace/UpdateSousRace/', newSousRace).subscribe();
  }




}
