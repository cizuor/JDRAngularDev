import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-perso-info',
  templateUrl: './perso-info.component.html',
  styleUrls: ['./perso-info.component.css']
})
export class PersoInfoComponent implements OnInit {


  @Input() nom: string;
  @Input() prenom: string;
  @Input() race: string;
  @Input() classe: string;
  @Input() sousrace: string;
  @Input() definition: string;
  @Input() id: number;


  constructor() { }

  ngOnInit() {
  }

}
