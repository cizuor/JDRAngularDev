import { Component, OnInit, Input, Inject } from '@angular/core';
import { StatService } from '../../services/stat.service';
import { HttpClient } from '@angular/common/http';


// ng g c chenil --module app
@Component({
  selector: 'app-statinfo',
  templateUrl: './statinfo.component.html',
  styleUrls: ['./statinfo.component.css']
})
export class StatinfoComponent implements OnInit {

  @Input() nom: string;
  @Input() definition: string;
  @Input() id: number;
  @Input() type: string;
  @Input() stats: string;
  baseUrl = '';

  constructor(private http: HttpClient, private statService: StatService, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
  }

}
