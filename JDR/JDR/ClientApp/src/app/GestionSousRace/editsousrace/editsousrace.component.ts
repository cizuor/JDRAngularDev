import { Component, OnInit, Inject } from '@angular/core';
import { StatService } from '../../services/stat.service';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { SousRaceService } from '../../services/sousrace.service';
import { RaceService } from '../../services/race.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-editsousrace',
  templateUrl: './editsousrace.component.html',
  styleUrls: ['./editsousrace.component.css']
})
export class EditsousraceComponent implements OnInit {

  baseUrl = '';
  sousRaceSubscription: Subscription;
  sousRace: any[];

  statSubscription: Subscription;
  stats: any[];


  raceSubscription: Subscription;
  races: any[];

  statSousRaceid = 0;


  private id: number;


  constructor(private http: HttpClient, private statService: StatService, private sousRaceService: SousRaceService, private raceService: RaceService, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private router: Router) {
    this.baseUrl = baseUrl;
  }


  ngOnInit() {

    this.id = this.route.snapshot.params['id'];

    this.statSubscription = this.statService.statSubject.subscribe(
      (s: any[]) => {
        this.stats = s;
        console.log(this.stats);
      }
    );
    this.statService.emitStatSubject();
    this.statService.getAllStat();

    this.raceSubscription = this.raceService.raceSubject.subscribe(
      (s: any[]) => {
        this.races = s;
        console.log(this.races);
      }
    );
    this.raceService.emitRaceSubject();
    this.raceService.getAllRaces();


    this.sousRaceSubscription = this.sousRaceService.sousRaceSelectSubject.subscribe(
      (s: any[]) => {
        this.sousRace = s;
      }
    );
    this.sousRaceService.emitSousRaceSelectedSubject();
    this.sousRaceService.getSousRace(this.id);


    console.log(this.sousRace);

  }



  addStat() {

    var nomstat = '';
    for (let i = 0; i < this.stats.length; i++) {
      if (this.stats[i]['id'] == this.statSousRaceid) {
        nomstat = this.stats[i]['nom'];
      }
    }
    this.sousRace['stat'].push({
      statId: this.statSousRaceid,
      nomStat: nomstat,
      id: this.sousRace['id'],
      valeur: 2
    });
  }

  Modif() {

    this.sousRaceService.majSousRace();
    this.router.navigate(['/allSousRace']);

  }
}
