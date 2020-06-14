import { Component, OnInit, Inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-inscription',
  templateUrl: './inscription.component.html',
  styleUrls: ['./inscription.component.css']
})
export class InscriptionComponent implements OnInit {


  baseUrl = '';
  usersSubscription: Subscription;
  users: any[];

  usersSelectedSubscription: Subscription;
  userSelected: any[];



  constructor(private http: HttpClient, private userService: UserService, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private router: Router) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {

    this.usersSubscription = this.userService.userSubject.subscribe(
      (s: any[]) => {
        this.users = s;
        console.log(this.users);
      }
    );
    this.userService.emitUserSubject();
    this.userService.getAllUser();


    this.usersSelectedSubscription = this.userService.userSelectSubject.subscribe(
      (s: any[]) => {
        this.userSelected = s;
        console.log(this.userSelected);
      }
    );
    this.userService.emitSelectedUserSubject();
    this.userService.getNewUser();



  }

  Valider() {
    var ok: boolean;
    ok = true;
    for (let i = 0; i < this.users.length; i++) {
      if (this.users[i]['email'] == this.users['email']) {
        ok = false;
      }
    }
    console.log(this.userSelected)
    if (ok) {
      this.userService.addUser()
    }
  }

}
