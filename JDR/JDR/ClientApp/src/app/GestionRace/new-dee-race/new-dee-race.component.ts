import { Component, OnInit, Input, Inject } from '@angular/core';

@Component({
  selector: 'app-new-dee-race',
  templateUrl: './new-dee-race.component.html',
  styleUrls: ['./new-dee-race.component.css']
})
export class NewDeeRaceComponent implements OnInit {

  @Input() raceStat: any[];


  constructor() {
  }


  ngOnInit() {
  }


}

