import { Component, OnInit, Inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { RaceService } from '../../services/race.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StatService } from '../../services/stat.service';

@Component({
  selector: 'app-editrace',
  templateUrl: './editrace.component.html',
  styleUrls: ['./editrace.component.css']
})
export class EditraceComponent implements OnInit {

  baseUrl = '';
  raceSubscription: Subscription;
  race: any[];

  statSubscription: Subscription;
  stats: any[];


  statraceid = 0;
  statdeeid = 0;



  private id: number;
  private num: number;

  constructor(private http: HttpClient, private statService: StatService, private raceService: RaceService, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private router: Router) {
    this.baseUrl = baseUrl;
  }


  ngOnInit() {
    this.id = this.route.snapshot.params['id'];


    this.statSubscription = this.statService.statLightSubject.subscribe(
      (s: any[]) => {
        this.stats = s;
        console.log(this.stats);
      }
    );
    this.statService.emitStatLightSubject();
    this.statService.getAllStatLight();

    this.raceSubscription = this.raceService.raceSelectSubject.subscribe(
      (s: any[]) => {
        this.race = s;
      }
    );
    this.raceService.emitSelectedRaceSubject();
    this.raceService.getRace(this.id);
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

    this.raceService.MajRace(this.race['id'], this.race['nom'], this.race['definition'])
    this.router.navigate(['/allRace']);
  }


}
