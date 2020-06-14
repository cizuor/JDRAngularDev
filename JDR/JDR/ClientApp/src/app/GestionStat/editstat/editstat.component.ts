import { Component, OnInit, Inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { StatService, stats, typestats } from '../../services/stat.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-editstat',
  templateUrl: './editstat.component.html',
  styleUrls: ['./editstat.component.css']
})
export class EditstatComponent implements OnInit {



  private stattype = stats
  public statOption = [];
  private typestats = typestats
  public typestatsOption = [];

  baseUrl = '';

  statUtilSubscription: Subscription;
  statUtil: any[];

  statSubscription: Subscription;
  liststats: any[];


  private id = 0;
  private num = 0;


  constructor(private http: HttpClient, private statService: StatService, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.statService.clearStatUtil();// netoi les stat utils du service pour ne pas en avoir d'une ancienne page
    this.id = this.route.snapshot.params['id'];
    this.statUtilSubscription = this.statService.statUtilSubject.subscribe(
      (stats: any[]) => {
        this.statUtil = stats.slice();
      });

    this.statService.emitStatUtilSubject();

    this.statSubscription = this.statService.statSubject.subscribe(
      (s: any[]) => {
        this.liststats = s.slice();
        console.log(this.liststats);
      }
    );
    this.statService.emitStatSubject();
    this.statService.getAllStat();


    console.log('onInit');
    console.log(this.id);

    this.statOption = Object.keys(this.stattype);
    this.typestatsOption = Object.keys(this.typestats);

    for (var i = 0; i < this.liststats.length; i++) {
      if (this.liststats[i]['id'] == this.id) {
        this.num = i;
      }
    }
    this.statService.getStatUtilFromStat(this.id);



  }

  addStatUtils() {
    this.statService.addStatUtil();
  }

  Modif() {

    this.statService.MajStat(this.liststats[this.num]['id'],this.liststats[this.num]['nom'], this.liststats[this.num]['definition'], this.liststats[this.num]['type'], this.liststats[this.num]['stats'])
  }


}
