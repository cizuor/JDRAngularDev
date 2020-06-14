import { Component, OnInit, Inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { StatService } from '../../services/stat.service';
import { HttpClient } from '@angular/common/http';
import { ClasseService } from '../../services/classe.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-classe',
  templateUrl: './edit-classe.component.html',
  styleUrls: ['./edit-classe.component.css']
})
export class EditClasseComponent implements OnInit {

  baseUrl = '';
  classeSubscription: Subscription;
  classe: any[];

  statSubscription: Subscription;
  stats: any[];

  statclasseid = 0;

  private id: number;

  constructor(private http: HttpClient, private statService: StatService, private classeService: ClasseService, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute, private router: Router) {
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

    this.classeSubscription = this.classeService.classeSelectSubject.subscribe(
      (s: any[]) => {
        this.classe = s;
      }
    );
    this.classeService.emitSelectedClasseSubject();
    this.classeService.getClasse(this.id);

    console.log(this.classe);

  }



  addStat() {

    var nomstat = '';
    for (let i = 0; i < this.stats.length; i++) {
      if (this.stats[i]['id'] == this.statclasseid) {
        nomstat = this.stats[i]['nom'];
      }
    }
    this.classe['stat'].push({
      statId: this.statclasseid,
      nomStat: nomstat,
      id: this.classe['id'],
      valeur: 2
    });
  }



  Modif() {

    this.classeService.majClasse();
    this.router.navigate(['/allClasse']);
  }



}
