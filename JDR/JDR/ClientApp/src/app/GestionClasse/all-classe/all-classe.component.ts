import { Component, OnInit, Inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ClasseService } from '../../services/classe.service';

@Component({
  selector: 'app-all-classe',
  templateUrl: './all-classe.component.html',
  styleUrls: ['./all-classe.component.css']
})
export class AllClasseComponent implements OnInit {

  baseUrl = '';
  classeSubscription: Subscription;
  classes: any[];


  constructor(private http: HttpClient, private classeService: ClasseService, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }


  ngOnInit() {

    this.classeSubscription = this.classeService.classeSubject.subscribe(
      (s: any[]) => {
        this.classes = s.slice();
        console.log(this.classes);
      }
    );
    this.classeService.emitClasseSubject();
    this.classeService.getAllClasse();
  }


}
