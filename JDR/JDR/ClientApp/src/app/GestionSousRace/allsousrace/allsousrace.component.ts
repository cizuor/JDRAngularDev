import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { SousRaceService } from '../../services/sousrace.service';

@Component({
  selector: 'app-allsousrace',
  templateUrl: './allsousrace.component.html',
  styleUrls: ['./allsousrace.component.css']
})
export class AllsousraceComponent implements OnInit {

  baseUrl = '';
  sousRaceSubscription: Subscription;
  sousRaces: any[];


  constructor(private http: HttpClient, private sousRaceService: SousRaceService, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }


  ngOnInit() {

    this.sousRaceSubscription = this.sousRaceService.sousRaceSubject.subscribe(
      (s: any[]) => {
        this.sousRaces = s.slice();
        console.log(this.sousRaces);
      }
    );
    this.sousRaceService.emitSousRaceSubject();
    this.sousRaceService.getAllSousRace();
  }


}
