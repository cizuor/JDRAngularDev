import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StatService, stats } from '../../services/stat.service';
import { RaceService } from '../../services/race.service';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-newrace',
  templateUrl: './newrace.component.html',
  styleUrls: ['./newrace.component.css']
})
export class NewraceComponent implements OnInit {

  baseUrl = '';
  raceSubscription: Subscription;
  race: any[];

  statSubscription: Subscription;
  stats: any[];

  statraceid = 0;
  statdeeid = 0;


  private stattype = stats

  private id: number;
  private num: number;

  constructor(private http: HttpClient, private statService: StatService, private raceService: RaceService, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private router: Router) {
    this.baseUrl = baseUrl;
  }


  ngOnInit() {


    this.statSubscription = this.statService.statSubject.subscribe(
      (s: any[]) => {
        this.stats = s;
        console.log(this.stats);
      }
    );
    this.statService.emitStatSubject();
    this.statService.getAllStat();

    this.raceSubscription = this.raceService.raceSelectSubject.subscribe(
      (s: any[]) => {
        this.race = s;
      }
    );
    this.raceService.emitSelectedRaceSubject();
    this.raceService.getNewRace();

    console.log(this.race);
  }



  statInit() {

  }



  addStat() {

    var nomstat = '';
    for (let i = 0; i < this.stats.length; i++) {
      if (this.stats[i]['id'] == this.statraceid) {
        nomstat = this.stats[i]['nom'];
      }
    }
    this.race['stat'].push({
      statId: this.statraceid,
      nomStat: nomstat,
      id: this.race['id'],
      valeur: 20
    });
  }

  addDee() {
    var nomstat = '';
    for (let i = 0; i < this.stats.length; i++) {
      if (this.stats[i]['id'] == this.statdeeid) {
        nomstat = this.stats[i]['nom'];
      }
    }

    this.race['statDee'].push({
      statId: this.statdeeid,
      nomStat: nomstat,
      raceId: this.race['id'],
      nbDee: 2,
      tailleDee: 6
    });
  }

  Modif() {

    this.raceService.addRace(this.race['nom'], this.race['definition'])
    this.router.navigate(['/allRace']);
  }
}
