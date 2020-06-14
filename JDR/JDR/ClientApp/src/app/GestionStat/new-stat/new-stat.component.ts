import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { StatService, stats, typestats } from '../../services/stat.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-new-stat',
  templateUrl: './new-stat.component.html',
  styleUrls: ['./new-stat.component.css']
})
export class NewStatComponent implements OnInit {

  private statselect = "0";
  private stattype = stats
  public statOption = [];
  private statNom = 'Nom';
  private statDef = 'Definition';
  private typeStatSelect = "1";
  private typestats = typestats
  public typestatsOption = [];



  statUtilSubscription: Subscription;
  statUtil: any[];


  baseUrl = '';
  constructor(private http: HttpClient, private statService: StatService, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {

    this.statService.clearStatUtil();
    this.statUtilSubscription = this.statService.statUtilSubject.subscribe(
      (stats: any[]) => {
        this.statUtil = stats.slice();
      });

    this.statService.emitStatUtilSubject();

    console.log('onInit');
    console.log(stats);
    console.log(typestats);
    this.statOption = Object.keys(this.stattype);
    this.typestatsOption = Object.keys(this.typestats);
    //this.numbers = Array(100).fill().map((x, i) => i); // [0,1,2,3,4]
  }



  addStatUtils() {
    this.statService.addStatUtil();
  }



  affiche() {
    console.log('affiche');
    //console.log(this.statselect + ' type ' + typeof this.statselect);
    //console.log(this.typeStatSelect + ' type ' + typeof this.typeStatSelect);
    console.log(this.statUtil);



    this.statService.addStat(this.statNom, this.statDef, this.typeStatSelect, this.statselect)
  }



}
