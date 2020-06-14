import { Component, OnInit, Inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-conection',
  templateUrl: './conection.component.html',
  styleUrls: ['./conection.component.css']
})
export class ConectionComponent implements OnInit {

  baseUrl = '';
  usersSubscription: Subscription;
  users: any[];

  usersSelectedSubscription: Subscription;
  userSelected: any[];

  mail: string;
  pass: string;

  connect: Boolean;


  constructor(private http: HttpClient, private userService: UserService, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private router: Router) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    console.log('onInit');
    this.connect = false;
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
        if (this.userSelected['id']>=0) {
          this.connect = true;
          console.log("connect");
        }
      }
    );
    this.userService.emitSelectedUserSubject();


  }

  Valider() {
    this.userService.getUser(this.mail, this.pass);
  }


}
