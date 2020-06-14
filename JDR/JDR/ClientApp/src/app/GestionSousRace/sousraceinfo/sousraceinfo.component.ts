import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-sousraceinfo',
  templateUrl: './sousraceinfo.component.html',
  styleUrls: ['./sousraceinfo.component.css']
})
export class SousraceinfoComponent implements OnInit {

  @Input() nom: string;
  @Input() nomRace: string;
  @Input() definition: string;
  @Input() id: number;


  constructor() { }

  ngOnInit() {
  }

}
