import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-classeinfo',
  templateUrl: './classeinfo.component.html',
  styleUrls: ['./classeinfo.component.css']
})
export class ClasseinfoComponent implements OnInit {


  @Input() nom: string;
  @Input() definition: string;
  @Input() id: number;


  constructor() { }

  ngOnInit() {
  }

}
