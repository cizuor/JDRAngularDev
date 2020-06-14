import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-raceinfo',
  templateUrl: './raceinfo.component.html',
  styleUrls: ['./raceinfo.component.css']
})
export class RaceinfoComponent implements OnInit {

  @Input() nom: string;
  @Input() definition: string;
  @Input() id: number;


  constructor() { }

  ngOnInit() {
  }

}
