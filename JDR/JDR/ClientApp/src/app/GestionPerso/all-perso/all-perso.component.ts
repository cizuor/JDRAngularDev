import { Component, OnInit, Inject } from '@angular/core';
import { PersoService } from '../../services/perso.service';
import { UserService } from '../../services/user.service';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-all-perso',
  templateUrl: './all-perso.component.html',
  styleUrls: ['./all-perso.component.css']
})
export class AllPersoComponent implements OnInit {

  baseUrl = '';
  usersSelectedSubscription: Subscription;
  userSelected: any[];

  persoSubscription: Subscription;
  persos: any[];

  connectSubscription: Subscription;
  connect: Boolean;


  constructor(private http: HttpClient, private persoService: PersoService, private userService: UserService, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.usersSelectedSubscription = this.userService.userSelectSubject.subscribe(
      (s: any[]) => {
        this.userSelected = s;
        console.log(this.userSelected);
        this.persoService.getAllPerso(this.userSelected['id']);
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

    this.persoSubscription = this.persoService.persoSubject.subscribe(
      (p: any[]) => {
        this.persos = p;
        console.log(this.persos);
      }
    );
    this.persoService.emitPersoSubject();
  }



}
