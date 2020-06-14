import { Component, OnInit, Inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { RaceService } from '../../services/race.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-allrace',
  templateUrl: './allrace.component.html',
  styleUrls: ['./allrace.component.css']
})
export class AllraceComponent implements OnInit {


  baseUrl = '';
  raceSubscription: Subscription;
  races: any[];

  constructor(private http: HttpClient, private raceService: RaceService, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {

    this.raceSubscription = this.raceService.raceSubject.subscribe(
      (s: any[]) => {
        this.races = s.slice();
        console.log(this.races);
      }
    );
    this.raceService.emitRaceSubject();
    this.raceService.getAllRaces();
  }

}
