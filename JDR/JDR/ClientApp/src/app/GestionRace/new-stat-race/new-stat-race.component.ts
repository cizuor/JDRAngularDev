import { Component, OnInit, Input, Inject } from '@angular/core';


@Component({
  selector: 'app-new-stat-race',
  templateUrl: './new-stat-race.component.html',
  styleUrls: ['./new-stat-race.component.css']
})
export class NewStatRaceComponent implements OnInit {

  //@Input() race: any[];
  @Input() raceStat: any[];
  /*
  raceSubscription: Subscription;
  race: any[];*/

  /*statSubscription: Subscription;
  stats: any[];
  */



  constructor() {
  }


  ngOnInit() {
    /*this.raceSubscription = this.raceService.raceSelectSubject.subscribe(
      (s: any[]) => {
        this.race = s;
        console.log(this.race['nom'])
        console.log(this.race['stat'])
      }
    );
    this.raceService.emitSelectedRaceSubject();*/
    //this.raceService.getRace(this.idRace);

   /* this.statSubscription = this.statService.statSubject.subscribe(
      (s: any[]) => {
        this.stats = s.slice();
      }
    );
    this.statService.emitStatSubject();

*/
  }

}
