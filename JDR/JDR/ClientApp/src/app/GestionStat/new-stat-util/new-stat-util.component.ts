import { Component, OnInit, Input, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { StatService } from '../../services/stat.service';

@Component({
  selector: 'app-new-stat-util',
  templateUrl: './new-stat-util.component.html',
  styleUrls: ['./new-stat-util.component.css']
})
export class NewStatUtilComponent implements OnInit {


  @Input() num: number;
  id = 0;
  valeur = 50;

  statSubscription: Subscription;
  stats: any[];
  baseUrl = '';


  statUtilSubscription: Subscription;
  statUtil: any[];



  constructor(private http: HttpClient, private statService: StatService, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.statSubscription = this.statService.statSubject.subscribe(
      (s: any[]) => {
        this.stats = s.slice();
      }
    );
    this.statService.emitStatSubject();
    this.statService.getAllStat();

    this.statUtilSubscription = this.statService.statUtilSubject.subscribe(
      (stats: any[]) => {
        this.statUtil = stats.slice();
        console.log("stat util" + this.num)
        console.log("valeur" + this.statUtil[this.num]['valeur'])
      });
    this.statService.emitStatUtilSubject();
    console.log(this.statUtil);


  }


}
