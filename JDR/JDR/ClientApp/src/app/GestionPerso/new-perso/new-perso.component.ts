import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PersoService } from '../../services/perso.service';
import { UserService } from '../../services/user.service';
import { Subscription } from 'rxjs';
import { RaceService } from '../../services/race.service';
import { SousRaceService } from '../../services/sousrace.service';
import { ClasseService } from '../../services/classe.service';

@Component({
  selector: 'app-new-perso',
  templateUrl: './new-perso.component.html',
  styleUrls: ['./new-perso.component.css']
})
export class NewPersoComponent implements OnInit {




  baseUrl = '';
  usersSelectedSubscription: Subscription;
  userSelected: any[];

  persoSubscription: Subscription;
  perso: any[];

  connectSubscription: Subscription;
  connect: Boolean;

  raceSubscription: Subscription;
  races: any[];

  sousRaceSubscription: Subscription;
  sousRaces: any[];

  classeSubscription: Subscription;
  classes: any[];



  constructor(private http: HttpClient, private persoService: PersoService, private userService: UserService, private raceService: RaceService, private sousRaceService: SousRaceService, private classeService: ClasseService, @Inject('BASE_URL') baseUrl: string) { }

  ngOnInit() {
    this.usersSelectedSubscription = this.userService.userSelectSubject.subscribe(
      (s: any[]) => {
        this.userSelected = s;
        console.log(this.userSelected);
      }
    );
    this.userService.emitSelectedUserSubject();

    this.connectSubscription = this.userService.conectSubject.subscribe(
      (s: boolean) => {
        this.connect = s;
        console.log(this.connect);
      }
    );
    this.userService.emitLogSubject();

    this.raceSubscription = this.raceService.raceSubject.subscribe(
      (s: any[]) => {
        this.races = s.slice();
        console.log('races');
        console.log(this.races);
      }
    );

    this.raceService.emitRaceSubject();
    this.raceService.getAllRaces();

    this.sousRaceSubscription = this.sousRaceService.sousRaceSubject.subscribe(
      (s: any[]) => {
        this.sousRaces = s.slice();
        console.log('sous race');
        console.log(this.sousRaces);
      }
    );
    this.sousRaceService.emitSousRaceSubject();
    this.sousRaceService.getAllSousRace();

    this.persoSubscription = this.persoService.persoSelectSubject.subscribe(
      (p: any[]) => {
        this.perso = p;
        console.log('perso');
        console.log(this.perso);
      }
    )
    this.persoService.emitSelectedPersoSubject();
    this.persoService.getNewPerso();

    this.classeSubscription = this.classeService.classeSubject.subscribe(

      (c: any[]) => {
        this.classes = c.slice();
        console.log('classe');
        console.log(this.classes);
      })
    this.classeService.emitClasseSubject();
    this.classeService.getAllClasse()


  }



  getSousRace() {
    return this.sousRaces.filter((item) => item['idRace'] == this.perso['idRace'])
  }

}
