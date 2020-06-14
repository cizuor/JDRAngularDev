import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { StatService } from '../../services/stat.service';


// ng g c chenil --module app
@Component({
  selector: 'app-allstat',
  templateUrl: './allstat.component.html',
  styleUrls: ['./allstat.component.css']
})
export class AllstatComponent implements OnInit {
  baseUrl = '';
  statSubscription: Subscription;
  stats: any[];



  constructor(private http: HttpClient, private statService: StatService, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.statSubscription = this.statService.statSubject.subscribe(
      (s: any[]) => {
        this.stats = s.slice();
        console.log(this.stats);
      }
    );
    this.statService.emitStatSubject();
    this.onFetch();
  }

  onFetch() {
    this.statService.getAllStat();
  }


}
