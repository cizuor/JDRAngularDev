import { Inject } from "@angular/core";
import { Subject, race } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Race } from "../models/race.model";


export class RaceService {

  raceSubject = new Subject<any[]>();
  private allrace = [];
  baseUrl: string


  raceSelectSubject = new Subject<any[]>();
  private raceSelect = [];

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }


  emitRaceSubject() {
    this.raceSubject.next(this.allrace.slice());
  }

  emitSelectedRaceSubject() {
    this.raceSelectSubject.next(this.raceSelect);
  }

  getAllRaces() {
    this.httpClient.get<any[]>('api/race/').subscribe(result => {
      console.log(result);
      this.allrace = result;
      this.emitRaceSubject();
    }, error => console.error(error))
  }

  getRace(id: number) {
    console.log('getRace : '+id)
    this.httpClient.get<any[]>('api/race/'+id).subscribe(result => {
      console.log(result);
      this.raceSelect = result;
      this.emitSelectedRaceSubject();
    }, error => console.error(error))
  }

  getNewRace() {
    this.httpClient.get<any[]>('api/race/GenerateNewRace/').subscribe(result => {
      console.log(result);
      this.raceSelect = result;
      this.emitSelectedRaceSubject();
    }, error => console.error(error))
  }


  addRace(name: string, definition: string) {
    var newRace = new Race(0, name, definition)

    newRace.Stat = this.raceSelect['stat'];
    newRace.StatDee = this.raceSelect['statDee'];

    this.httpClient.post(this.baseUrl + 'api/race/', newRace).subscribe();
  }


  MajRace(id: number, name: string, definition: string) {
    var newRace = new Race(id, name, definition)

    newRace.Stat = this.raceSelect['stat'];
    newRace.StatDee = this.raceSelect['statDee'];

    this.httpClient.post(this.baseUrl + 'api/race/UpdateRace/', newRace).subscribe();
  }


}
