import { Subject } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Inject } from "@angular/core";
import { User } from "../models/user.model";

export class UserService {
  baseUrl: string

  userSubject = new Subject<any[]>();
  private alluser = [];


  userSelectSubject = new Subject<any[]>();
  private userSelect = [];

  conectSubject = new Subject<boolean>();
  private log = false;


  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }


  emitUserSubject() {
    this.userSubject.next(this.alluser.slice());
  }

  emitSelectedUserSubject() {
    this.userSelectSubject.next(this.userSelect);
  }

  emitLogSubject() {
    this.conectSubject.next(this.log);
  }


  getAllUser() {
    this.httpClient.get<any[]>('api/user/').subscribe(result => {
      console.log(result);
      this.alluser = result;
      this.emitUserSubject();
    }, error => console.error(error))
  }

  getUser(mail: string, pass: string) {
    this.httpClient.get<any[]>('api/user/' + mail+'/'+pass).subscribe(result => {
      console.log(result);
      this.userSelect = result;
      if (result['id'] != -1) {
        this.log = true;
        console.log(' compte logé');
        this.emitLogSubject();
      }
      this.emitSelectedUserSubject();

    }, error => console.error(error))
  }

  getNewUser() {
    this.userSelect.push({
      id: -2,
      pseudo: "pseudo",
      email: "email",
      password: ""
    });
  }

  addUser()
  {
    console.log(this.userSelect);
      var newUser = new User(0, this.userSelect['pseudo'], this.userSelect['email'], this.userSelect['password'])
      this.httpClient.post(this.baseUrl + 'api/user/', newUser).subscribe();
  }

}
